"""Componente per costruire l'HTML della pagina web"""

# Input:
#   controls_html: lista di stringhe HTML per i controlli
#   include_3d_viewer: booleano per includere il visualizzatore 3D

def build_page_html(controls_html=[], include_3d_viewer=True):
    # Estrai tutti gli stili CSS dai controlli
    css_blocks = []
    html_blocks = []
    js_blocks = []
    
    for control in controls_html:
        # Estrai il CSS (tutto ciò che è tra <style> e </style>)
        css_start = control.find('<style>')
        css_end = control.find('</style>')
        if css_start != -1 and css_end != -1:
            css = control[css_start + 7:css_end].strip()
            if css not in css_blocks:  # Evita duplicati
                css_blocks.append(css)
        
        # Estrai l'HTML (tutto ciò che è tra </style> e <script>)
        html_start = control.find('</style>') + 8
        html_end = control.find('<script>')
        if html_start != -1 and html_end != -1:
            html = control[html_start:html_end].strip()
            html_blocks.append(html)
        
        # Estrai il JavaScript (tutto ciò che è tra <script> e </script>)
        js_start = control.find('<script>') + 8
        js_end = control.find('</script>')
        if js_start != -1 and js_end != -1:
            js = control[js_start:js_end].strip()
            js_blocks.append(js)
    
    # CSS base e dei controlli combinati
    base_css = """
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
    """
    
    # Costruisci il CSS combinato
    combined_css = f"""
    <style>
        {base_css}
        {' '.join(css_blocks)}
    </style>
    """
    
    # Costruisci l'HTML dei controlli
    controls_section = f"""
    <div id="controls">
        {' '.join(html_blocks)}
    </div>
    """
    
    # Gestisci il viewer 3D
    viewer_html = """
    <div id="viewer"></div>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/three.js/r128/three.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/three@0.128.0/examples/js/controls/OrbitControls.js"></script>
    """ if include_3d_viewer else ""
    
    # Combina tutto il JavaScript
    combined_js = f"""
    <script>
        // Variabile globale per mantenere lo stato di tutti i controlli
        let globalControlsState = {{}};

        // Funzione per inizializzare i valori dei controlli
        async function initializeControls() {{
            try {{
                const response = await fetch('/get_initial_values');
                const data = await response.json();
                
                if (data.status === 'success') {{
                    const values = data.values;
                    // Aggiorna lo stato globale
                    globalControlsState = values;
                    
                    // Itera su tutti i controlli
                    Object.keys(values).forEach(key => {{
                        const value = values[key];
                        const controlId = key.replace('_value', '');
                        const element = document.getElementById(controlId);
                        const displayElement = document.getElementById(controlId + '-value');
                        
                        if (element) {{
                            // Imposta il valore in base al tipo di controllo
                            if (element.type === 'range' || element.type === 'number') {{
                                element.value = value;
                                // Aggiorna il display del valore
                                if (displayElement) {{
                                    displayElement.textContent = value;
                                }}
                            }} else if (element.tagName === 'SELECT') {{
                                element.value = value;
                            }} else if (element.type === 'checkbox') {{
                                element.checked = value === true || value === 'true';
                            }}
                        }}
                    }});

                    // Notifica che i controlli sono stati inizializzati
                    document.dispatchEvent(new CustomEvent('controlsInitialized'));
                }}
            }} catch (error) {{
                console.error('Errore nel caricamento dei valori iniziali:', error);
            }}
        }}

        // Funzione aggiornata per l'update del server
        function updateServer(data) {{
            // Aggiorna lo stato globale
            Object.assign(globalControlsState, data.controls);
            
            // Invia lo stato completo al server
            fetch('/update', {{
                method: 'POST',
                headers: {{
                    'Content-Type': 'application/json',
                }},
                body: JSON.stringify({{
                    controls: globalControlsState
                }})
            }});
        }}

        // Inizializza i controlli al caricamento della pagina
        document.addEventListener('DOMContentLoaded', initializeControls);
        
        // Codice dei controlli
        {' '.join(js_blocks)}
    </script>
    """
    
    # Costruisci la pagina completa
    html = f"""
    <!DOCTYPE html>
    <html>
    <head>
        <title>Grasshopper Interface</title>
        {combined_css}
    </head>
    <body>
        {controls_section}
        {viewer_html}
        {combined_js}
    </body>
    </html>
    """
    
    return html

# Output: HTML completo della pagina
page_html = build_page_html(controls_html, include_3d_viewer) 