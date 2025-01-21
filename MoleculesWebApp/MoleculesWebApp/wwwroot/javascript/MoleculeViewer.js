"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
// MoleculeViewer.ts
var BABYLON = require("./babylon.js");
var MoleculeViewer = /** @class */ (function () {
    function MoleculeViewer(canvasId) {
        var _this = this;
        this.canvas = document.getElementById(canvasId);
        if (!this.canvas) {
            throw new Error("Canvas with ID '".concat(canvasId, "' not found."));
        }
        this.engine = new BABYLON.Engine(this.canvas, true);
        this.scene = new BABYLON.Scene(this.engine);
        this.camera = new BABYLON.ArcRotateCamera("Camera", Math.PI / 2, Math.PI / 2, 20, new BABYLON.Vector3(0, 0, 0), this.scene);
        this.camera.attachControl(this.canvas, true);
        this.light = new BABYLON.HemisphericLight("Light", new BABYLON.Vector3(0, 1, 0), this.scene);
        this.atomColors = {
            H: new BABYLON.Color3(1, 1, 1),
            C: new BABYLON.Color3(0.4, 0.4, 0.4),
            O: new BABYLON.Color3(1, 0, 0),
            N: new BABYLON.Color3(0, 0, 1),
            S: new BABYLON.Color3(1, 1, 0),
            P: new BABYLON.Color3(1, 0.5, 0),
            default: new BABYLON.Color3(0.5, 0.5, 0.5),
        };
        this.engine.runRenderLoop(function () {
            _this.scene.render();
        });
        window.addEventListener("resize", function () {
            _this.engine.resize();
        });
    }
    MoleculeViewer.prototype.createSphere = function (position, color, radius) {
        if (radius === void 0) { radius = 0.5; }
        var sphere = BABYLON.MeshBuilder.CreateSphere("atom", { diameter: radius * 2 }, this.scene);
        sphere.position = new BABYLON.Vector3(position.x, position.y, position.z);
        var material = new BABYLON.StandardMaterial("atomMaterial", this.scene);
        material.diffuseColor = color;
        sphere.material = material;
        return sphere;
    };
    MoleculeViewer.prototype.createCylinder = function (start, end, color, radius) {
        if (radius === void 0) { radius = 0.1; }
        var path = [
            new BABYLON.Vector3(start.x, start.y, start.z),
            new BABYLON.Vector3(end.x, end.y, end.z),
        ];
        var tube = BABYLON.MeshBuilder.CreateTube("bond", { path: path, radius: radius }, this.scene);
        var material = new BABYLON.StandardMaterial("bondMaterial", this.scene);
        material.diffuseColor = color;
        tube.material = material;
        return tube;
    };
    MoleculeViewer.prototype.createLabel = function (position, text) {
        var dynamicTexture = new BABYLON.DynamicTexture("DynamicTexture", { width: 512, height: 256 }, this.scene, false);
        dynamicTexture.hasAlpha = true;
        dynamicTexture.drawText(text, null, 140, "bold 60px Arial", "black", "transparent", true);
        var plane = BABYLON.MeshBuilder.CreatePlane("label", { size: 1 }, this.scene);
        plane.position = new BABYLON.Vector3(position.x, position.y + 0.6, position.z);
        var labelMaterial = new BABYLON.StandardMaterial("labelMaterial", this.scene);
        labelMaterial.diffuseTexture = dynamicTexture;
        labelMaterial.emissiveColor = new BABYLON.Color3(1, 1, 1);
        labelMaterial.backFaceCulling = false;
        plane.material = labelMaterial;
        return plane;
    };
    MoleculeViewer.prototype.addAtom = function (atom) {
        var color = this.atomColors[atom.element] || this.atomColors.default;
        var sphere = this.createSphere(atom.position, color, atom.radius);
        this.createLabel(atom.position, atom.element);
        return sphere;
    };
    MoleculeViewer.prototype.addBond = function (bond) {
        var color = this.atomColors.default;
        return this.createCylinder(bond.start, bond.end, color, bond.radius);
    };
    MoleculeViewer.prototype.loadMolecule = function (molecule) {
        var _this = this;
        // Clear existing scene objects
        this.scene.meshes.forEach(function (mesh) { return mesh.dispose(); });
        // Add atoms
        molecule.atoms.forEach(function (atom) { return _this.addAtom(atom); });
        // Add bonds
        molecule.bonds.forEach(function (bond) { return _this.addBond(bond); });
    };
    return MoleculeViewer;
}());
// Example Usage
// Assuming you have a canvas with ID 'renderCanvas'
var viewer = new MoleculeViewer("renderCanvas");
var molecule = {
    atoms: [
        { element: "C", position: { x: 0, y: 0, z: 0 }, radius: 0.5 },
        { element: "O", position: { x: 1.5, y: 0, z: 0 }, radius: 0.5 },
        { element: "H", position: { x: -1, y: 1, z: 0 }, radius: 0.3 },
    ],
    bonds: [
        { start: { x: 0, y: 0, z: 0 }, end: { x: 1.5, y: 0, z: 0 }, radius: 0.1 },
        { start: { x: 0, y: 0, z: 0 }, end: { x: -1, y: 1, z: 0 }, radius: 0.1 },
    ],
};
viewer.loadMolecule(molecule);
//# sourceMappingURL=MoleculeViewer.js.map