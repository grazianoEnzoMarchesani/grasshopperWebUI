"""Componente per l'aggiornamento dei valori"""
import os
import tempfile
import Grasshopper as gh

# Input:
#   monitor: istanza del ValueMonitor
#   control_id: ID del controllo da monitorare
#   trigger: input trigger per forzare l'aggiornamento (non usato direttamente)

# Variabili globali per la cache
_last_modified_time = 0
_cached_value = None
_json_path = os.path.join(tempfile.gettempdir(), 'gh_interface_data.json')

def update_value(monitor, control_id):
    global _last_modified_time, _cached_value
    
    if monitor is None:
        return None
    
    try:
        # Verifica se il file esiste
        if not os.path.exists(_json_path):
            return None
            
        # Ottiene il timestamp di ultima modifica del file
        current_modified_time = os.path.getmtime(_json_path)
        
        # Se il file è stato modificato, aggiorna la cache e forza l'aggiornamento
        if current_modified_time > _last_modified_time:
            value = monitor.get_value(control_id)
            _last_modified_time = current_modified_time
            _cached_value = value
            
            # Forza l'aggiornamento del componente
            ghenv.Component.ExpireSolution(True)
            
            return value
            
        # Se il file non è stato modificato, usa la cache
        return _cached_value
        
    except Exception as e:
        print(f"Errore nell'aggiornamento del valore: {e}")
        return _cached_value  # In caso di errore, ritorna l'ultimo valore valido

# Output: valore aggiornato del controllo specificato
value = update_value(monitor, control_id) 