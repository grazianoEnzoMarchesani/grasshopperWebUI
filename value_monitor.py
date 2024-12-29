"""Componente per la gestione del monitoraggio dei valori"""
import json
import os
import tempfile
import time

# Input:
#   x: Trigger (opzionale, per forzare l'inizializzazione)

JSON_PATH = os.path.join(tempfile.gettempdir(), 'gh_interface_data.json')

class ValueMonitor:
    def __init__(self):
        self.last_check = 0
        self.last_values = (50, 'circle')
    
    def check_updates(self):
        try:
            if os.path.exists(JSON_PATH):
                mod_time = os.path.getmtime(JSON_PATH)
                if mod_time > self.last_check:
                    with open(JSON_PATH, 'r') as f:
                        data = json.load(f)
                    self.last_check = mod_time
                    self.last_values = (data['slider_value'], data['shape'])
                    return self.last_values
            return self.last_values
        except Exception as e:
            print(f"Errore nel monitoraggio: {e}")
            return self.last_values

class MonitorInstance:
    _instance = None
    
    @classmethod
    def get_instance(cls):
        if cls._instance is None:
            cls._instance = ValueMonitor()
        return cls._instance

# Crea l'istanza del monitor
monitor_instance = MonitorInstance.get_instance()

# Output per Grasshopper
a = monitor_instance  # Questo è l'output che verrà collegato al componente value_update 