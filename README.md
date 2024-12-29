# GH-Web Interface

A universal web interface for Grasshopper has been developed that operates through standard web browsers, ensuring broad accessibility across Windows and Mac operating systems. This technological solution enables interactive visualization and manipulation of three-dimensional geometries.

## Description

The development of this system was driven by the necessity to address limitations in traditional Grasshopper interfaces. The web-based implementation eliminates compatibility constraints while enabling real-time visualization and manipulation of complex 3D geometries through custom controls integrated with the Grasshopper environment.

The system's architecture has been implemented in Python, chosen for its computational capabilities. A local web server manages the bidirectional communication between Grasshopper and the web interface, where native geometries are converted to Three.js format for optimized web visualization. The interface has been developed with responsive design principles, featuring an advanced 3D visualization system with orbital controls for comprehensive model exploration.

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
