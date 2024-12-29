
from flask import Flask, render_template_string, jsonify, request
import webbrowser
import threading
import socket
import time
import json
import os
import tempfile

# Usa la directory temporanea del sistema
JSON_PATH = os.path.join(tempfile.gettempdir(), 'gh_interface_data.json')

# Inizializza il file JSON se non esiste
def init_json_file():
    default_data = {
        "slider_value": 50,
        "shape": "circle",
        "last_update": time.time()
    }
    if not os.path.exists(JSON_PATH):
        with open(JSON_PATH, 'w') as f:
            json.dump(default_data, f)
    return default_data

# Modifica del contenuto HTML per utilizzare AJAX
html_content = """
<!DOCTYPE html>
<html>
<head>
    <title>Grasshopper Interface</title>
    <style>
        body { 
            font-family: Arial, sans-serif; 
            padding: 20px;
            max-width: 800px;
            margin: 0 auto;
            background-color: #f5f5f5;
        }
        .control { 
            margin: 20px 0;
            padding: 20px;
            background-color: white;
            border-radius: 8px;
            box-shadow: 0 2px 4px rgba(0,0,0,0.1);
        }
        label {
            display: block;
            margin-bottom: 10px;
            font-weight: bold;
        }
        input[type="range"] {
            width: 100%;
            margin: 10px 0;
        }
        select {
            width: 100%;
            padding: 8px;
            border-radius: 4px;
            border: 1px solid #ddd;
        }
    </style>
</head>
<body>
    <div class="control">
        <label>Slider: <span id="slider-value">50</span></label>
        <input type="range" id="slider" min="0" max="100" value="50">
    </div>
    <div class="control">
        <label>Shape:</label>
        <select id="shape-select">
            <option value="circle">Circle</option>
            <option value="rectangle">Rectangle</option>
            <option value="polygon">Polygon</option>
        </select>
    </div>

    <script>
        function updateServer(data) {
            fetch('/update', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(data)
            });
        }

        function pollUpdates() {
            fetch('/get_values')
                .then(response => response.json())
                .then(data => {
                    document.getElementById('slider').value = data.slider_value;
                    document.getElementById('slider-value').textContent = data.slider_value;
                    document.getElementById('shape-select').value = data.shape;
                });
        }

        // Aggiorna i valori ogni 100ms
        setInterval(pollUpdates, 100);

        document.getElementById('slider').addEventListener('input', (e) => {
            const value = e.target.value;
            document.getElementById('slider-value').textContent = value;
            updateServer({
                slider_value: parseFloat(value),
                shape: document.getElementById('shape-select').value
            });
        });

        document.getElementById('shape-select').addEventListener('change', (e) => {
            updateServer({
                slider_value: parseFloat(document.getElementById('slider').value),
                shape: e.target.value
            });
        });
    </script>
</body>
</html>
"""

class WebInterface:
    _instance = None
    
    def __new__(cls):
        if cls._instance is None:
            cls._instance = super(WebInterface, cls).__new__(cls)
        return cls._instance
    
    def __init__(self):
        if not hasattr(self, 'initialized'):
            self.initialized = True
            self.app = Flask(__name__)
            self.server_thread = None
            self.port = self.find_free_port()
            self.setup_routes()
            init_json_file()
            
    def find_free_port(self):
        """Trova una porta libera partendo da 5000"""
        ports = range(5000, 5100)
        for port in ports:
            with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as s:
                try:
                    s.bind(('127.0.0.1', port))
                    return port
                except socket.error:
                    continue
        raise RuntimeError("Nessuna porta disponibile trovata")
            
    def setup_routes(self):
        @self.app.route('/')
        def index():
            return render_template_string(html_content)
        
        @self.app.route('/update', methods=['POST'])
        def update():
            try:
                data = request.json
                data['last_update'] = time.time()
                with open(JSON_PATH, 'w') as f:
                    json.dump(data, f)
                return jsonify({"status": "success"})
            except Exception as e:
                return jsonify({"status": "error", "message": str(e)}), 500
        
        @self.app.route('/get_values')
        def get_values():
            try:
                with open(JSON_PATH, 'r') as f:
                    return jsonify(json.load(f))
            except Exception as e:
                return jsonify({"status": "error", "message": str(e)}), 500
        
        @self.app.route('/favicon.ico')
        def favicon():
            return '', 204  # Restituisce una risposta vuota con status code 204 (No Content)

    def stop_server(self):
        """Ferma il server se è in esecuzione"""
        if self.server_thread and self.server_thread.is_alive():
            print("Stopping existing server...")
            try:
                requests.get(f'http://127.0.0.1:{self.port}/shutdown')
            except:
                pass
            self.server_thread = None
            time.sleep(1)
            
    def start_server(self):
        self.stop_server()
        
        if not self.server_thread:
            self.server_thread = threading.Thread(target=self._run_server)
            self.server_thread.daemon = True
            self.server_thread.start()
            time.sleep(2)
            webbrowser.open(f'http://127.0.0.1:{self.port}')
            
    def _run_server(self):
        try:
            self.app.run(
                host='127.0.0.1',
                port=self.port, 
                debug=False,
                use_reloader=False
            )
        except Exception as e:
            print(f"Errore nell'avvio del server: {e}")

class ServerInstance:
    _instance = None
    _is_running = False
    
    @classmethod
    def get_instance(cls):
        if cls._instance is None:
            cls._instance = WebInterface()
        return cls._instance
    
    @classmethod
    def start_if_not_running(cls):
        if not cls._is_running:
            instance = cls.get_instance()
            instance.start_server()
            cls._is_running = True

# Output: nessuno necessario, il componente serve solo ad avviare il server
ServerInstance.start_if_not_running() 