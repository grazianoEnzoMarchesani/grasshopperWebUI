"""Componente per generare un input numerico con spinbutton"""

def create_number_input(label, min_val=0, max_val=100, default_val=50, step=1, control_id="number1", custom_css=""):
    # CSS di default
    default_css = """
        .number-container {
            margin: 10px 0;
        }
        .number-container label {
            display: block;
            margin-bottom: 5px;
        }
        .number-container input[type="number"] {
            width: 80px;
            padding: 2px 5px;
            border: 1px solid #ccc;
            border-radius: 2px;
        }
        .number-container input[type="number"]:focus {
            outline: none;
            border-color: #666;
        }
    """
    
    css_content = default_css
    if custom_css and custom_css.strip():
        css_content = custom_css.strip()
    
    html = f"""
    <style>
        {css_content}
    </style>
    <div class="control number-container" id="{control_id}-container">
        <label for="{control_id}">{label}</label>
        <input type="number" 
               id="{control_id}" 
               min="{min_val}" 
               max="{max_val}" 
               value="{default_val}"
               step="{step}"
               oninput="updateNumberValue_{control_id}(this)">
    </div>
    <script>
        function updateNumberValue_{control_id}(input) {{
            let value = parseFloat(input.value);
            
            // Validazione dei limiti
            if (value < {min_val}) value = {min_val};
            if (value > {max_val}) value = {max_val};
            
            const data = {{
                controls: {{
                    '{control_id}_value': value
                }}
            }};
            updateServer(data);
        }}
        
        // Inizializzazione
        document.addEventListener('controlsInitialized', function() {{
            const input = document.getElementById('{control_id}');
            if (input) {{
                const value = input.value;
                // Non chiamare updateNumberValue qui per evitare loop
            }}
        }});
    </script>
    """
    return html

# Output: HTML dell'input numerico
number_html = create_number_input(label, min_val, max_val, default_val, step, control_id, custom_css) 