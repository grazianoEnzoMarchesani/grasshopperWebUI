"""Componente per monitorare i valori dei controlli"""

import json
import os
import tempfile
import time

JSON_PATH = os.path.join(tempfile.gettempdir(), 'gh_interface_data.json')

class ValueMonitor:
    def __init__(self):
        self.json_path = os.path.join(tempfile.gettempdir(), 'gh_interface_data.json')
        self.last_read_time = 0
        self.cached_values = {}

    def read_values(self):
        """Legge i valori dal file JSON se sono stati aggiornati"""
        try:
            # Verifica se il file esiste
            if not os.path.exists(self.json_path):
                return {}

            # Ottiene il timestamp di ultima modifica del file
            file_modified_time = os.path.getmtime(self.json_path)
            
            # Se il file non è stato modificato dall'ultima lettura, usa la cache
            if file_modified_time <= self.last_read_time:
                return self.cached_values

            # Legge il file JSON
            with open(self.json_path, 'r') as f:
                content = f.read().strip()  # Rimuove spazi bianchi extra
                if not content:  # Se il file è vuoto
                    return {}
                try:
                    data = json.loads(content)
                except json.JSONDecodeError as e:
                    print(f"Errore nel parsing JSON: {e}")
                    print(f"Contenuto problematico: {content[:100]}...")  # Mostra i primi 100 caratteri
                    return self.cached_values  # Usa la cache in caso di errore
                
            # Aggiorna la cache e il timestamp
            if isinstance(data, dict):
                self.cached_values = data.get('controls', {})
                self.last_read_time = file_modified_time
            else:
                print(f"Formato JSON non valido. Atteso dizionario, ricevuto: {type(data)}")
                return self.cached_values

            return self.cached_values

        except Exception as e:
            print(f"Errore nella lettura dei valori: {e}")
            return self.cached_values  # Usa la cache in caso di errore

    def get_value(self, control_id):
        """Ottiene il valore specifico per un controllo"""
        values = self.read_values()
        try:
            value = values.get(f"{control_id}_value")
            # Converti le stringhe numeriche in numeri
            if isinstance(value, str):
                try:
                    if '.' in value:
                        return float(value)
                    else:
                        return int(value)
                except ValueError:
                    return value
            return value
        except Exception as e:
            print(f"Errore nel recupero del valore per {control_id}: {e}")
            return None

class MonitorInstance:
    _instance = None
    
    @classmethod
    def get_instance(cls):
        if cls._instance is None:
            cls._instance = ValueMonitor()
        return cls._instance

# Output: istanza del monitor
monitor = MonitorInstance.get_instance() 