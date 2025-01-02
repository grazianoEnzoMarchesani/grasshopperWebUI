import tempfile
import os

temp_dir = tempfile.gettempdir()
print(f"Directory temporanea: {temp_dir}")

# Verifica la presenza dei file specifici
json_interface = os.path.join(temp_dir, 'gh_interface_data.json')
json_geometry = os.path.join(temp_dir, 'gh_geometry_data.json')
html_temp = os.path.join(temp_dir, 'gh_interface.html')

print(f"\nFile dell'interfaccia: {json_interface}")
print(f"Esiste: {os.path.exists(json_interface)}")

print(f"\nFile della geometria: {json_geometry}")
print(f"Esiste: {os.path.exists(json_geometry)}")

print(f"\nFile HTML temporaneo: {html_temp}")
print(f"Esiste: {os.path.exists(html_temp)}")
