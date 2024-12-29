"""Componente per la gestione della geometria 3D"""
import json
import os
import tempfile
import time
import System.Drawing

# Input:
#   vertices: Lista di coordinate dei vertici dalla mesh decostruita
#   faces: Lista di facce dalla mesh decostruita
#   colors: Lista di colori (oggetti Color di Grasshopper)
#   normals: Lista di normali per vertice (opzionale)
#   trigger: Trigger per aggiornamento (opzionale)

JSON_PATH = os.path.join(tempfile.gettempdir(), 'gh_geometry_data.json')

def parse_face_indices(face):
    """Converte una faccia di Grasshopper in lista di indici"""
    try:
        # Se è un oggetto MeshFace di Grasshopper
        if hasattr(face, 'IsTriangle'):
            if face.IsTriangle:
                return [face.A, face.B, face.C]
            return [face.A, face.B, face.C, face.D]
        # Se la faccia è una stringa nel formato "Q(1, 0, 2, 3)"
        elif isinstance(face, str):
            # Estrai solo i numeri
            nums = ''.join(c for c in face if c.isdigit() or c == ',' or c == ' ')
            return [int(n.strip()) for n in nums.split(',')]
        # Se la faccia è già una lista o tupla di indici
        elif isinstance(face, (list, tuple)):
            return list(face)
        else:
            print(f"Tipo di faccia non riconosciuto: {type(face)}")
            return []
    except Exception as e:
        print(f"Errore nel parsing della faccia {face}: {e}")
        return []

def parse_color(color):
    """Converte un colore di Grasshopper in lista normalizzata"""
    try:
        if isinstance(color, System.Drawing.Color):
            return [color.R/255.0, color.G/255.0, color.B/255.0]
        elif isinstance(color, str):
            if ',' in color:
                # Se è una stringa "R,G,B"
                r, g, b = map(int, color.strip().split(','))
                return [r/255.0, g/255.0, b/255.0]
            else:
                print(f"Formato colore non riconosciuto: {color}")
                return [0.7, 0.7, 0.7]
        else:
            print(f"Tipo di colore non riconosciuto: {type(color)}")
            return [0.7, 0.7, 0.7]
    except Exception as e:
        print(f"Errore nella conversione del colore {color}: {e}")
        return [0.7, 0.7, 0.7]

def serialize_geometry(vertices, faces, colors, normals=None):
    """Serializza i dati della mesh decostruita per Three.js"""
    try:
        print("\n=== Debug Input ===")
        print(f"Numero vertici: {len(vertices)}")
        print(f"Numero facce: {len(faces)}")
        print(f"Numero colori: {len(colors)}")
        print(f"Tipo vertici: {type(vertices[0]) if vertices else 'None'}")
        print(f"Tipo facce: {type(faces[0]) if faces else 'None'}")
        print(f"Tipo colori: {type(colors[0]) if colors else 'None'}")
        
        # Converti vertici in lista di tuple e applica una trasformazione per Three.js
        vertices_list = [(v.X, v.Z, -v.Y) for v in vertices]  # Scambia Y e Z e inverte Y
        
        # Converti facce in liste di indici
        faces_list = [parse_face_indices(face) for face in faces]
        # Filtra le facce vuote
        faces_list = [f for f in faces_list if len(f) >= 3]
        
        # Converti colori in valori normalizzati
        colors_list = [parse_color(color) for color in colors]
        
        # Debug dei dati convertiti
        print("\n=== Dati Convertiti ===")
        print("Primi 3 vertici:")
        for i, v in enumerate(vertices_list[:3]):
            print(f"  v{i}: ({v[0]:.2f}, {v[1]:.2f}, {v[2]:.2f})")
        
        print("\nPrime 3 facce:")
        for i, f in enumerate(faces_list[:3]):
            print(f"  f{i}: {f}")
            if f:  # Solo se la faccia è valida
                print("    Vertici:", [vertices_list[idx] for idx in f])
        
        print("\nPrimi 3 colori:")
        for i, c in enumerate(colors_list[:3]):
            print(f"  c{i}: [{c[0]:.3f}, {c[1]:.3f}, {c[2]:.3f}]")
        
        # Prepara i dati per il JSON
        geometry_data = {
            "vertices": vertices_list,
            "faces": faces_list,
            "colors": colors_list,
            "timestamp": time.time()
        }
        
        # Aggiungi le normali se presenti
        if normals:
            normals_list = [(n.X, n.Y, n.Z) for n in normals]
            geometry_data["normals"] = normals_list
        
        # Salva su file
        with open(JSON_PATH, 'w') as f:
            json.dump(geometry_data, f)
        
        print("\n=== Dati Salvati con Successo ===")
        return True
        
    except Exception as e:
        print(f"Errore nella serializzazione della geometria: {e}")
        import traceback
        print(traceback.format_exc())
        return False

# Output
success = serialize_geometry(vertices, faces, colors, normals) 