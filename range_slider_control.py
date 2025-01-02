"""Componente per generare uno slider con due maniglie per range"""

def create_range_slider(label, min_val=0, max_val=100, default_min=25, default_max=75, step=1, control_id="range1", custom_css=""):
    # CSS di default
    default_css = """
        .range-container {
            margin: 10px 0;
        }
        .range-container label {
            display: block;
            margin-bottom: 5px;
        }
        .range-slider {
            position: relative;
            width: 100%;
            height: 30px;
        }
        .range-slider input[type="range"] {
            position: absolute;
            width: 100%;
            pointer-events: none;
            -webkit-appearance: none;
            z-index: 2;
            height: 4px;
            margin: 0;
            background: none;
            top: 50%;
            transform: translateY(-50%);
        }
        .range-slider input[type="range"]::-webkit-slider-thumb {
            pointer-events: all;
            width: 12px;
            height: 12px;
            border-radius: 2px;
            -webkit-appearance: none;
            background: #666;
            cursor: pointer;
            border: none;
        }
        .range-slider input[type="range"]::-moz-range-thumb {
            pointer-events: all;
            width: 12px;
            height: 12px;
            border-radius: 2px;
            background: #666;
            cursor: pointer;
            border: none;
        }
        .range-track {
            position: absolute;
            width: 100%;
            height: 4px;
            background: #ddd;
            z-index: 1;
            top: 50%;
            transform: translateY(-50%);
        }
    """
    
    css_content = default_css
    if custom_css and custom_css.strip():
        css_content = custom_css.strip()
    
    html = f"""
    <style>
        {css_content}
    </style>
    <div class="control range-container" id="{control_id}-container">
        <label>{label}: 
            <span id="{control_id}-min-value">{default_min}</span> - 
            <span id="{control_id}-max-value">{default_max}</span>
        </label>
        <div class="range-slider">
            <div class="range-track"></div>
            <input type="range" 
                   id="{control_id}-min" 
                   min="{min_val}" 
                   max="{max_val}" 
                   value="{default_min}"
                   step="{step}"
                   oninput="updateRangeValue_{control_id}()">
            <input type="range" 
                   id="{control_id}-max" 
                   min="{min_val}" 
                   max="{max_val}" 
                   value="{default_max}"
                   step="{step}"
                   oninput="updateRangeValue_{control_id}()">
        </div>
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

        function updateRangeValue_{control_id}() {{
            const minSlider = document.getElementById('{control_id}-min');
            const maxSlider = document.getElementById('{control_id}-max');
            const minDisplay = document.getElementById('{control_id}-min-value');
            const maxDisplay = document.getElementById('{control_id}-max-value');
            
            let minVal = parseFloat(minSlider.value);
            let maxVal = parseFloat(maxSlider.value);
            
            // Assicurati che min non superi max
            if (minVal > maxVal) {{
                if (this === minSlider) {{
                    minVal = maxVal;
                    minSlider.value = minVal;
                }} else {{
                    maxVal = minVal;
                    maxSlider.value = maxVal;
                }}
            }}
            
            // Formatta i valori individuali per il display
            const formattedMin = formatNumber_{control_id}(minVal);
            const formattedMax = formatNumber_{control_id}(maxVal);
            
            minDisplay.textContent = formattedMin;
            maxDisplay.textContent = formattedMax;
            
            // Salva i valori nel localStorage
            localStorage.setItem('{control_id}_min', minVal);
            localStorage.setItem('{control_id}_max', maxVal);
            
            // Crea la stringa combinata nel formato richiesto
            const combinedValue = `${{formattedMin}} To ${{formattedMax}}`;
            
            const data = {{
                controls: {{
                    '{control_id}_value': combinedValue
                }}
            }};
            updateServer(data);
        }}
        
        document.addEventListener('controlsInitialized', function() {{
            const minSlider = document.getElementById('{control_id}-min');
            const maxSlider = document.getElementById('{control_id}-max');
            if (minSlider && maxSlider) {{
                // Recupera i valori salvati
                const savedMin = localStorage.getItem('{control_id}_min');
                const savedMax = localStorage.getItem('{control_id}_max');
                if (savedMin !== null) {{
                    minSlider.value = savedMin;
                }}
                if (savedMax !== null) {{
                    maxSlider.value = savedMax;
                }}
                updateRangeValue_{control_id}();
            }}
        }});
    </script>
    """
    return html

# Output: HTML dello slider range
range_html = create_range_slider(label, min_val, max_val, default_min, default_max, step, control_id, custom_css) 