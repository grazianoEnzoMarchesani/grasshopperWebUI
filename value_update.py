"""Componente per l'aggiornamento continuo dei valori"""

# Input:
#   t: Timer (collegare un componente Timer)
#   m: MonitorInstance (collegare l'output del componente Value Monitor)

def update_values(monitor):
    if monitor is None:
        return 50, 'circle'  # valori di default
    return monitor.check_updates()

# Output: valori aggiornati
a, b = update_values(m)  # Usa 'm' che è l'input dal componente Value Monitor 