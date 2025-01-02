from flask import Flask, render_template_string, jsonify, request
import threading
import socket
import time
import json
import os
import tempfile
import requests

# Input:
#   html_content: stringa contenente l'HTML della pagina (da html_builder.py)
#   restart: bool - Se True, riavvia il server (opzionale)

# Usa la directory temporanea del sistema
JSON_PATH = os.path.join(tempfile.gettempdir(), 'gh_interface_data.json')
INSTANCE_PATH = os.path.join(tempfile.gettempdir(), 'gh_server_instance.json')
HTML_CACHE_PATH = os.path.join(tempfile.gettempdir(), 'gh_html_cache.json')

def init_json_file():
    """Inizializza il file JSON se non esiste"""
    try:
        current_data = {"controls": {}, "last_update": time.time()}
        
        if os.path.exists(JSON_PATH):
            try:
                with open(JSON_PATH, 'r') as f:
                    content = f.read().strip()
                    if content:
                        data = json.loads(content)
                        if isinstance(data, dict):
                            current_data = data
            except:
                pass
        
        # Assicurati che la struttura sia corretta
        if 'controls' not in current_data:
            current_data['controls'] = {}
        if 'last_update' not in current_data:
            current_data['last_update'] = time.time()
            
        # Salva il file
        with open(JSON_PATH, 'w') as f:
            json.dump(current_data, f, indent=2)
            
        return current_data
    except Exception as e:
        print(f"Errore nell'inizializzazione del file JSON: {e}")
        return {"controls": {}, "last_update": time.time()}

def get_running_instance():
    """Recupera informazioni sull'istanza corrente del server"""
    try:
        if os.path.exists(INSTANCE_PATH):
            with open(INSTANCE_PATH, 'r') as f:
                data = json.load(f)
                # Verifica se il server è effettivamente in esecuzione
                try:
                    with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as s:
                        s.settimeout(0.1)
                        result = s.connect_ex(('127.0.0.1', data['port']))
                        if result == 0:
                            return data
                except:
                    pass
    except:
        pass
    return None

def save_instance_info(port):
    """Salva le informazioni dell'istanza corrente"""
    try:
        with open(INSTANCE_PATH, 'w') as f:
            json.dump({'port': port, 'timestamp': time.time()}, f)
    except:
        pass

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
            
            # Controlla se esiste già un'istanza attiva
            running_instance = get_running_instance()
            if running_instance:
                self.port = running_instance['port']
                # Prova a fermare il server esistente
                try:
                    requests.get(f'http://127.0.0.1:{self.port}/shutdown', timeout=0.2)
                    time.sleep(0.5)
                except:
                    pass
            else:
                self.port = self.find_free_port()
            
            save_instance_info(self.port)
            self.setup_routes()
            init_json_file()
            self.check_server_status()

    def find_free_port(self):
        """Trova una porta libera"""
        for port in range(5000, 5500):
            try:
                with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as s:
                    s.settimeout(0.1)
                    result = s.connect_ex(('127.0.0.1', port))
                    if result != 0:  # La porta è libera
                        return port
            except:
                continue
        
        # Se non troviamo una porta nel range, lasciamo che il sistema ne assegni una
        with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as s:
            s.bind(('127.0.0.1', 0))
            return s.getsockname()[1]
            
    def setup_routes(self):
        @self.app.route('/')
        def index():
            try:
                # Legge l'HTML dal file cache
                if os.path.exists(HTML_CACHE_PATH):
                    with open(HTML_CACHE_PATH, 'r') as f:
                        data = json.load(f)
                        if data['port'] == self.port:  # Verifica che l'HTML sia per questa istanza
                            return render_template_string(data['html'])
            except Exception as e:
                print(f"Errore nel caricamento dell'HTML dalla cache: {e}")
            
            # Fallback all'HTML originale se c'è un errore
            return render_template_string(html_content)
        
        @self.app.route('/update', methods=['POST'])
        def update():
            try:
                new_data = request.json
                if not isinstance(new_data, dict):
                    return jsonify({"status": "error", "message": "Invalid data format"}), 400

                current_data = {"controls": {}, "last_update": time.time()}
                
                # Leggi i dati esistenti se il file esiste
                if os.path.exists(JSON_PATH):
                    try:
                        with open(JSON_PATH, 'r') as f:
                            content = f.read().strip()
                            if content:
                                current_data = json.loads(content)
                    except json.JSONDecodeError as e:
                        print(f"Errore nella lettura del JSON esistente: {e}")
                        # Continua con i dati di default se c'è un errore
                
                # Assicurati che la struttura sia corretta
                if not isinstance(current_data, dict):
                    current_data = {"controls": {}, "last_update": time.time()}
                if 'controls' not in current_data:
                    current_data['controls'] = {}
                    
                # Aggiorna i controlli
                if 'controls' in new_data:
                    current_data['controls'].update(new_data['controls'])
                current_data['last_update'] = time.time()
                
                # Salva i dati aggiornati
                with open(JSON_PATH, 'w') as f:
                    json.dump(current_data, f, indent=2)  # Usa indent per una formattazione leggibile
                    
                return jsonify({"status": "success"})
            except Exception as e:
                print(f"Errore nell'aggiornamento dei valori: {e}")
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

        @self.app.route('/get_geometry')
        def get_geometry():
            try:
                geometry_path = os.path.join(tempfile.gettempdir(), 'gh_geometry_data.json')
                if os.path.exists(geometry_path):
                    with open(geometry_path, 'r') as f:
                        return jsonify(json.load(f))
                return jsonify({"status": "error", "message": "No geometry data"})
            except Exception as e:
                return jsonify({"status": "error", "message": str(e)})

        @self.app.route('/get_initial_values')
        def get_initial_values():
            try:
                if os.path.exists(JSON_PATH):
                    with open(JSON_PATH, 'r') as f:
                        content = f.read().strip()
                        if content:
                            data = json.loads(content)
                            if isinstance(data, dict) and 'controls' in data:
                                return jsonify({"status": "success", "values": data['controls']})
                return jsonify({"status": "success", "values": {}})
            except Exception as e:
                print(f"Errore nel recupero dei valori iniziali: {e}")
                return jsonify({"status": "error", "message": str(e)}), 500

        @self.app.route('/shutdown', methods=['GET'])
        def shutdown():
            func = request.environ.get('werkzeug.server.shutdown')
            if func is None:
                raise RuntimeError('Non in esecuzione con il server Werkzeug')
            func()
            return 'Server in chiusura...'

    def stop_server(self):
        """Ferma il server se è in esecuzione"""
        if self.server_thread and self.server_thread.is_alive():
            print("\nChiusura server in corso...")
            try:
                requests.get(f'http://127.0.0.1:{self.port}/shutdown', timeout=0.2)
            except:
                pass
            
            # Attendiamo che il thread termini
            self.server_thread.join(timeout=2)
            self.server_thread = None
            
            # Aspettiamo che la porta si liberi
            max_attempts = 20  # Aumentiamo il numero di tentativi
            for _ in range(max_attempts):
                try:
                    with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as s:
                        s.settimeout(0.1)
                        result = s.connect_ex(('127.0.0.1', self.port))
                        if result != 0:  # La porta è libera
                            print(f"Server sulla porta {self.port} chiuso.")
                            return
                except:
                    pass
                time.sleep(0.3)  # Aumentiamo il tempo di attesa tra i tentativi
            
            # Se non riusciamo a liberare la porta, ne troviamo una nuova
            print(f"Impossibile liberare la porta {self.port}, cerco una nuova porta...")
            self.port = self.find_free_port()
            print(f"Nuova porta assegnata: {self.port}")

    def start_server(self):
        """Avvia il server su una porta libera"""
        self.stop_server()
        
        # Verifica ulteriore che la porta sia libera
        try:
            with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as s:
                s.settimeout(0.1)
                result = s.connect_ex(('127.0.0.1', self.port))
                if result == 0:  # La porta è ancora in uso
                    self.port = self.find_free_port()
                    print(f"Porta occupata, usando la nuova porta: {self.port}")
        except:
            pass
        
        if not self.server_thread:
            self.server_thread = threading.Thread(target=self._run_server)
            self.server_thread.daemon = True
            self.server_thread.start()
            time.sleep(1)
            self.check_server_status()

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

    def check_server_status(self):
        """Controlla e mostra lo stato del server"""
        active_ports = []
        for port in range(5000, 5020):
            try:
                with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as s:
                    s.settimeout(0.1)
                    result = s.connect_ex(('127.0.0.1', port))
                    if result == 0:
                        active_ports.append(port)
            except:
                pass
        
        # Prepara il messaggio di stato
        status_msg = f"\n=== STATO SERVER ===\n"
        status_msg += f"Porta attuale: {self.port}\n"
        
        if len(active_ports) > 0:
            other_ports = [p for p in active_ports if p != self.port]
            if other_ports:
                status_msg += f"Altre istanze attive trovate sulle porte: {other_ports}\n"
        
        status_msg += "==================="
        print(status_msg)
        return status_msg

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
        instance = cls.get_instance()
        
        # Forza sempre la chiusura del server esistente
        instance.stop_server()
        cls._is_running = False
        
        # Avvia una nuova istanza
        instance.start_server()
        cls._is_running = True
        
        return instance.port

# Output: porta del server corrente
server_instance = ServerInstance.start_if_not_running()
port = server_instance  # Assegna la porta restituita alla variabile di output 