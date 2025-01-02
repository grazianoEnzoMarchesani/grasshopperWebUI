"""Componente per l'aggiornamento dell'HTML del server"""
import tempfile
import json
import os

# Input:
#   html_content: stringa contenente l'HTML aggiornato
#   server_instance: istanza del server da html_builder.py

# Percorso del file temporaneo per l'HTML
HTML_CACHE_PATH = os.path.join(tempfile.gettempdir(), 'gh_html_cache.json')

def update_html(html_content, server_instance):
    """Aggiorna l'HTML del server senza riavviarlo"""
    if not server_instance or not html_content:
        return False
        
    try:
        # Salva l'HTML nel file temporaneo
        cache_data = {
            'html': html_content,
            'port': server_instance
        }
        
        with open(HTML_CACHE_PATH, 'w') as f:
            json.dump(cache_data, f)
            
        return True
        
    except Exception as e:
        print(f"Errore nell'aggiornamento dell'HTML: {e}")
        return False

# Output: True se l'aggiornamento è riuscito, False altrimenti
success = update_html(html_content, server_instance) 