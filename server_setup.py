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
    <script src="https://cdnjs.cloudflare.com/ajax/libs/three.js/r128/three.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/three@0.128.0/examples/js/controls/OrbitControls.js"></script>
    <style>
        body { 
            font-family: Arial, sans-serif; 
            margin: 0;
            display: flex;
            flex-direction: column;
            height: 100vh;
        }
        #controls { 
            padding: 20px;
            background-color: #f5f5f5;
        }
        .control { 
            margin: 10px 0;
            padding: 15px;
            background-color: white;
            border-radius: 8px;
            box-shadow: 0 2px 4px rgba(0,0,0,0.1);
        }
        #viewer {
            flex-grow: 1;
            width: 100%;
        }
    </style>
</head>
<body>
    <div id="controls">
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
    </div>
    <div id="viewer"></div>

    <script>
        // Inizializzazione Three.js
        const scene = new THREE.Scene();
        scene.background = new THREE.Color(0xf0f0f0);
        
        const viewer = document.getElementById('viewer');
        const camera = new THREE.PerspectiveCamera(75, viewer.clientWidth / viewer.clientHeight, 0.1, 1000);
        const renderer = new THREE.WebGLRenderer({ antialias: true });
        
        renderer.setSize(viewer.clientWidth, viewer.clientHeight);
        viewer.appendChild(renderer.domElement);
        
        // Luci
        const ambientLight = new THREE.AmbientLight(0x404040, 1);
        scene.add(ambientLight);
        
        const directionalLight = new THREE.DirectionalLight(0xffffff, 0.8);
        directionalLight.position.set(1, 1, 1);
        scene.add(directionalLight);
        
        const directionalLight2 = new THREE.DirectionalLight(0xffffff, 0.5);
        directionalLight2.position.set(-1, -1, -1);
        scene.add(directionalLight2);
        
        let currentMesh = null;
        let controls = null;

        // Gestione ridimensionamento
        window.addEventListener('resize', () => {
            const width = viewer.clientWidth;
            const height = viewer.clientHeight;
            camera.aspect = width / height;
            camera.updateProjectionMatrix();
            renderer.setSize(width, height);
        });

        function animate() {
            requestAnimationFrame(animate);
            if (controls) controls.update();
            renderer.render(scene, camera);
        }
        
        function updateGeometry(geometryData) {
            console.log("Dati geometria ricevuti:", {
                numVertici: geometryData.vertices.length,
                numFacce: geometryData.faces.length,
                numColori: geometryData.colors.length
            });

            if (currentMesh) {
                scene.remove(currentMesh);
            }
            
            const geometry = new THREE.BufferGeometry();
            
            // Vertici
            const vertices = new Float32Array(geometryData.vertices.flat());
            geometry.setAttribute('position', new THREE.BufferAttribute(vertices, 3));
            
            // Facce - gestisci sia triangoli che quad
            const indices = [];
            geometryData.faces.forEach(face => {
                if (face.length === 4) {
                    // Converte quad in due triangoli
                    indices.push(face[0], face[1], face[2]);
                    indices.push(face[2], face[3], face[0]);
                } else if (face.length === 3) {
                    indices.push(...face);
                }
            });
            geometry.setIndex(new THREE.BufferAttribute(new Uint16Array(indices), 1));
            
            // Colori
            const colors = new Float32Array(geometryData.colors.flat());
            geometry.setAttribute('color', new THREE.BufferAttribute(colors, 3));
            
            // Calcola le normali se non sono fornite
            if (geometryData.normals) {
                const normals = new Float32Array(geometryData.normals.flat());
                geometry.setAttribute('normal', new THREE.BufferAttribute(normals, 3));
            } else {
                geometry.computeVertexNormals();
            }
            
            const material = new THREE.MeshPhongMaterial({
                vertexColors: true,
                side: THREE.DoubleSide,
                flatShading: false,
                shininess: 30
            });
            
            currentMesh = new THREE.Mesh(geometry, material);
            
            // Centra e scala la geometria
            geometry.computeBoundingSphere();
            const center = geometry.boundingSphere.center;
            const radius = geometry.boundingSphere.radius;
            
            currentMesh.position.set(-center.x, -center.y, -center.z);
            
            // Imposta la camera
            camera.position.set(radius * 2, radius * 2, radius * 2);
            camera.lookAt(0, 0, 0);
            
            // Aggiungi controlli orbitali se non esistono
            if (!controls) {
                controls = new THREE.OrbitControls(camera, renderer.domElement);
                controls.enableDamping = true;
                controls.dampingFactor = 0.05;
            }
            
            scene.add(currentMesh);
        }

        // Avvia il loop di rendering
        animate();
        
        // Polling dei dati
        function pollGeometry() {
            fetch('/get_geometry')
                .then(response => response.json())
                .then(data => {
                    if (data.status !== 'error') {
                        updateGeometry(data);
                    }
                });
        }
        
        setInterval(pollGeometry, 100);

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