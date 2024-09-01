window.drawMolecule2D = (canvasId, moleculeData) => {
    // Create a 3D canvas
    var canvas = new ChemDoodle.TransformCanvas(canvasId, 300, 300, true);
    // gradient between JMol colors for atom types when drawing bonds
    canvas.styles.atoms_useJMOLColors = true;
    canvas.styles.bonds_splitColor = true;
    // make bonds thicker
    canvas.styles.bonds_width_2D = 1;
    canvas.styles.bonds_color = 'red';
    canvas.styles.atoms_display = true;
    // change the background color to black
    canvas.styles.backgroundColor = 'white';
    // clear overlaps to show z-depth
    canvas.styles.bonds_clearOverlaps_2D = true;

    canvas.styles.atoms_font_families = ['Arial', 'sans-serif'];
    canvas.styles.atoms_font_size_2D = 8;

    // Convert the molecule data to a ChemDoodle molecule
    var molecule = ChemDoodle.readMOL(moleculeData);

    // molecule.scaleToAverageBondLength(60);

    // Add labels to the atoms
    for (var i = 0; i < molecule.atoms.length; i++) {
        var atom = molecule.atoms[i];
        atom.label = atom.label + ' ' + (i + 1); // Add the atom position to the label
    }



    // Load the molecule into the canvas
    canvas.loadMolecule(molecule);
};