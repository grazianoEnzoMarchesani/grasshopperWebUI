import webbrowser
import time

# Input:
#   openUI: bool - Trigger per aprire il browser (True/False)
#   port: int - Porta del server (default: 5000)

class BrowserOpener:
    _last_open_time = 0
    _min_interval = 5  # Intervallo minimo in secondi tra le aperture del browser
    
    @classmethod
    def open_interface(cls, port=5000):
        """
        Apre l'interfaccia nel browser solo se è passato abbastanza tempo dall'ultima apertura
        Args:
            port (int): Porta su cui è in esecuzione il server
        """
        current_time = time.time()
        
        # Controlla se è passato abbastanza tempo dall'ultima apertura
        if current_time - cls._last_open_time >= cls._min_interval:
            url = f'http://127.0.0.1:{port}'
            webbrowser.open(url)
            cls._last_open_time = current_time

# Utilizzo diretto nel componente
if openUI:  # trigger è l'input del componente
    BrowserOpener.open_interface(port)  # port è l'input del componente