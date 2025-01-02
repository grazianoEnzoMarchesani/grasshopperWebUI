"""Componente per generare uno slider"""

# Input:
#   label: etichetta dello slider (stringa)
#   min_val: valore minimo (numero)
#   max_val: valore massimo (numero)
#   default_val: valore predefinito (numero)
#   step: incremento dello slider (numero)
#   control_id: identificatore univoco dello slider (stringa)
#   custom_css: CSS personalizzato per lo slider (stringa, opzionale)

def create_slider(label, min_val=0, max_val=100, default_val=50, step=1, control_id="slider1", custom_css=""):
    # CSS di default per lo slider (minimo, solo layout base)
    default_css = """
        .slider-container {
            margin: 10px 0;
        }
        .slider-container label {
            display: block;
            margin-bottom: 5px;
        }
        .slider-container input[type="range"] {
            width: 100%;
            -webkit-appearance: none;
            height: 4px;
            background: #ddd;
            border-radius: 0;
            margin: 10px 0;
        }
        .slider-container input[type="range"]::-webkit-slider-thumb {
            -webkit-appearance: none;
            width: 12px;
            height: 12px;
            background: #666;
            border-radius: 2px;
            cursor: pointer;
            border: none;
        }
        .slider-container input[type="range"]::-moz-range-thumb {
            width: 12px;
            height: 12px;
            background: #666;
            border-radius: 2px;
            cursor: pointer;
            border: none;
        }
        .slider-container input[type="range"]:focus {
            outline: none;
        }
    """
    
    # Gestisci il CSS personalizzato
    css_content = default_css
    if custom_css and custom_css.strip():
        css_content = custom_css.strip()
    
    html = f"""
    <style>
        {css_content}
    </style>
    <div class="control slider-container" id="{control_id}-container">
        <label for="{control_id}">{label}: <span id="{control_id}-value">{default_val}</span></label>
        <input type="range" 
               id="{control_id}" 
               min="{min_val}" 
               max="{max_val}" 
               value="{default_val}"
               step="{step}"
               oninput="updateSliderValue_{control_id}(this)">
    </div>
    <script>
        function formatNumber_{control_id}(value) {{
            // Determina il numero di decimali basato sullo step
            const step = {step};
            if (Number.isInteger(step)) {{
                return Math.round(value).toString();
            }} else {{
                const decimals = step.toString().split('.')[1]?.length || 0;
                return value.toFixed(decimals);
            }}
        }}

        function updateSliderValue_{control_id}(slider) {{
            const value = parseFloat(slider.value);
            // Aggiorna il display del valore
            const displayElement = document.getElementById('{control_id}-value');
            if (displayElement) {{
                displayElement.textContent = formatNumber_{control_id}(value);
            }}
            
            // Salva il valore nel localStorage
            localStorage.setItem('{control_id}_value', value);
            
            const data = {{
                controls: {{
                    '{control_id}_value': value
                }}
            }};
            updateServer(data);
        }}
        
        // Registra un observer per questo slider specifico
        document.addEventListener('DOMContentLoaded', function() {{
            const slider = document.getElementById('{control_id}');
            if (slider) {{
                // Aggiorna il display del valore iniziale
                const displayElement = document.getElementById('{control_id}-value');
                if (displayElement) {{
                    const value = parseFloat(slider.value);
                    displayElement.textContent = formatNumber_{control_id}(value);
                }}
            }}
        }});

        // Gestisci l'inizializzazione dei valori salvati
        document.addEventListener('controlsInitialized', function() {{
            const slider = document.getElementById('{control_id}');
            if (slider) {{
                // Recupera il valore salvato
                const savedValue = localStorage.getItem('{control_id}_value');
                if (savedValue !== null) {{
                    slider.value = savedValue;
                }}
                const displayElement = document.getElementById('{control_id}-value');
                if (displayElement) {{
                    const value = parseFloat(slider.value);
                    displayElement.textContent = formatNumber_{control_id}(value);
                }}
            }}
        }});
    </script>
    """
    return html

# Output: HTML dello slider
slider_html = create_slider(label, min_val, max_val, default_val, step, control_id, custom_css) 