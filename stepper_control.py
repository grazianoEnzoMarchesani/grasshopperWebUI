"""Componente per generare un controllo stepper"""

def create_stepper(label, min_val=0, max_val=100, default_val=50, step=1, control_id="stepper1", custom_css=""):
    # CSS di default
    default_css = """
        .stepper-container {
            margin: 10px 0;
        }
        .stepper-container label {
            display: block;
            margin-bottom: 5px;
        }
        .stepper-controls {
            display: flex;
            align-items: center;
            gap: 5px;
        }
        .stepper-button {
            width: 24px;
            height: 24px;
            border: 1px solid #ccc;
            background: #ffffff;
            cursor: pointer;
            border-radius: 2px;
            font-size: 14px;
        }
        .stepper-button:hover {
            background: #e8e8e8;
        }
        .stepper-value {
            width: 60px;
            text-align: center;
            padding: 2px 5px;
            border: 1px solid #ccc;
            border-radius: 2px;
        }
    """
    
    css_content = default_css
    if custom_css and custom_css.strip():
        css_content = custom_css.strip()
    
    html = f"""
    <style>
        {css_content}
    </style>
    <div class="control stepper-container" id="{control_id}-container">
        <label for="{control_id}">{label}</label>
        <div class="stepper-controls">
            <button class="stepper-button" onclick="updateStepperValue_{control_id}(-{step})">-</button>
            <input type="number" 
                   id="{control_id}" 
                   class="stepper-value"
                   min="{min_val}" 
                   max="{max_val}" 
                   value="{default_val}"
                   step="{step}"
                   oninput="validateStepperValue_{control_id}(this.value)">
            <button class="stepper-button" onclick="updateStepperValue_{control_id}({step})">+</button>
        </div>
    </div>
    <script>
        function validateStepperValue_{control_id}(value) {{
            let numValue = parseFloat(value);
            if (numValue < {min_val}) numValue = {min_val};
            if (numValue > {max_val}) numValue = {max_val};
            
            document.getElementById('{control_id}').value = numValue;
            
            // Salva il valore nel localStorage
            localStorage.setItem('{control_id}_value', numValue);
            
            const data = {{
                controls: {{
                    '{control_id}_value': numValue
                }}
            }};
            updateServer(data);
        }}
        
        function updateStepperValue_{control_id}(increment) {{
            const input = document.getElementById('{control_id}');
            const newValue = parseFloat(input.value) + increment;
            validateStepperValue_{control_id}(newValue);
        }}
        
        document.addEventListener('controlsInitialized', function() {{
            const input = document.getElementById('{control_id}');
            if (input) {{
                // Recupera il valore salvato
                const savedValue = localStorage.getItem('{control_id}_value');
                if (savedValue !== null) {{
                    input.value = savedValue;
                }}
                validateStepperValue_{control_id}(input.value);
            }}
        }});
    </script>
    """
    return html

# Output: HTML dello stepper
stepper_html = create_stepper(label, min_val, max_val, default_val, step, control_id, custom_css) 