# GH-Web Interface

A universal web interface for Grasshopper has been developed that operates through standard web browsers, ensuring broad accessibility across Windows and Mac operating systems. This technological solution enables interactive visualization and manipulation of three-dimensional geometries.

## Description

The development of this system was driven by the necessity to address limitations in traditional Grasshopper interfaces. The web-based implementation eliminates compatibility constraints while enabling real-time visualization and manipulation of complex 3D geometries through custom controls integrated with the Grasshopper environment.

The system's architecture has been implemented in Python, chosen for its computational capabilities. A local web server manages the bidirectional communication between Grasshopper and the web interface, where native geometries are converted to Three.js format for optimized web visualization. The interface has been developed with responsive design principles, featuring an advanced 3D visualization system with orbital controls for comprehensive model exploration.


## Version 0.2.0 Changelog

### Architecture Changes
- Decoupled server functionality into separate components:
  - Server creation component
  - HTML update component
  - Web page opening component
- Implemented volatile web page serving (no physical file storage)
- Introduced dual JSON system for component state management:
  - Write JSON: Controls state updates from Grasshopper
  - Read JSON: Handles user input from web interface

### New Custom Input Components
Introduced a suite of customizable input controls with the following features:
- Configurable CSS styling
- Label support
- Minimum and maximum value constraints
- Unique ID system

New components include:
1. Slider Control
   - Single value slider with continuous input
2. Range Slider Control
   - Dual-handle slider for range selection
3. Number Input Control
   - Direct numerical input field
4. Stepper Control
   - Increment/decrement controls for precise value adjustment

### Component Properties
Each input component supports:
- Custom labeling
- Value constraints (min/max)
- Unique identifier
- Custom styling through CSS
- Real-time value updates

### Technical Details
- Compatible with Rhino 8
- Built with Python 3
- Uses native Python libraries for server functionality
- Real-time bidirectional communication between Grasshopper and web interface

### Requirements
- Rhino 8
- Python 3.x
- Web browser with JavaScript enabled

These packages are essential for:
- Flask: Web server functionality and API endpoints
- NumPy: Numerical operations and array handling
- json: Enhanced JSON processing

### Usage
1. Add server component to your Grasshopper definition
2. Connect desired input components
3. Configure component parameters
4. Access the interface through your web browser

### Notes
- Web interface runs in memory without creating physical files
- Component states are managed through JSON data exchange
- All components support real-time updates


## Current Features

The Three.js-based visualization system has been equipped with real-time geometry updates and responsive controls. The implementation includes support for colored meshes and normals, ensuring detailed model representation. Interactive controls, such as sliders and dropdown menus, have been integrated to facilitate parameter manipulation within the Grasshopper environment.

## Future Developments

Future enhancements will introduce a comprehensive system of customizable interface components and flexible layouts adaptable to specific user requirements. The visualization capabilities will be expanded to include advanced components for both two-dimensional and three-dimensional model representation.

### Supported Inputs

The system processes deconstructed meshes (vertices, faces), vertex colors, and vertex normals.

## Contributing

Contributions to this project are welcomed through the standard fork and pull request workflow. Contributors are encouraged to create dedicated branches for their modifications, commit their changes with descriptive messages, and submit pull requests for review.

## License

This project is distributed under the GNU General Public License v3.0
