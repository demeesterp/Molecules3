// MoleculeViewer.ts
import * as BABYLON from './babylon.js';

type Vector3 = { x: number; y: number; z: number };

type Atom = {
    element: string;
    position: Vector3;
    radius: number;
};

type Bond = {
    start: Vector3;
    end: Vector3;
    radius: number;
};

type Molecule = {
    atoms: Atom[];
    bonds: Bond[];
};

class MoleculeViewer {
    private canvas: HTMLCanvasElement;
    private engine: BABYLON.Engine;
    private scene: BABYLON.Scene;
    private camera: BABYLON.ArcRotateCamera;
    private light: BABYLON.HemisphericLight;
    private atomColors: Record<string, BABYLON.Color3>;

    constructor(canvasId: string) {
        this.canvas = document.getElementById(canvasId) as HTMLCanvasElement;

        if (!this.canvas) {
            throw new Error(`Canvas with ID '${canvasId}' not found.`);
        }

        this.engine = new BABYLON.Engine(this.canvas, true);
        this.scene = new BABYLON.Scene(this.engine);
        this.camera = new BABYLON.ArcRotateCamera(
            "Camera",
            Math.PI / 2,
            Math.PI / 2,
            20,
            new BABYLON.Vector3(0, 0, 0),
            this.scene
        );
        this.camera.attachControl(this.canvas, true);

        this.light = new BABYLON.HemisphericLight(
            "Light",
            new BABYLON.Vector3(0, 1, 0),
            this.scene
        );

        this.atomColors = {
            H: new BABYLON.Color3(1, 1, 1),
            C: new BABYLON.Color3(0.4, 0.4, 0.4),
            O: new BABYLON.Color3(1, 0, 0),
            N: new BABYLON.Color3(0, 0, 1),
            S: new BABYLON.Color3(1, 1, 0),
            P: new BABYLON.Color3(1, 0.5, 0),
            default: new BABYLON.Color3(0.5, 0.5, 0.5),
        };

        this.engine.runRenderLoop(() => {
            this.scene.render();
        });

        window.addEventListener("resize", () => {
            this.engine.resize();
        });
    }

    private createSphere(position: Vector3, color: BABYLON.Color3, radius: number = 0.5): BABYLON.Mesh {
        const sphere = BABYLON.MeshBuilder.CreateSphere(
            "atom",
            { diameter: radius * 2 },
            this.scene
        );
        sphere.position = new BABYLON.Vector3(position.x, position.y, position.z);
        const material = new BABYLON.StandardMaterial("atomMaterial", this.scene);
        material.diffuseColor = color;
        sphere.material = material;
        return sphere;
    }

    private createCylinder(start: Vector3, end: Vector3, color: BABYLON.Color3, radius: number = 0.1): BABYLON.Mesh {
        const path = [
            new BABYLON.Vector3(start.x, start.y, start.z),
            new BABYLON.Vector3(end.x, end.y, end.z),
        ];
        const tube = BABYLON.MeshBuilder.CreateTube("bond", { path, radius }, this.scene);
        const material = new BABYLON.StandardMaterial("bondMaterial", this.scene);
        material.diffuseColor = color;
        tube.material = material;
        return tube;
    }

    private createLabel(position: Vector3, text: string): BABYLON.Mesh {
        const dynamicTexture = new BABYLON.DynamicTexture("DynamicTexture", { width: 512, height: 256 }, this.scene, false);
        dynamicTexture.hasAlpha = true;
        dynamicTexture.drawText(text, null, 140, "bold 60px Arial", "black", "transparent", true);

        const plane = BABYLON.MeshBuilder.CreatePlane("label", { size: 1 }, this.scene);
        plane.position = new BABYLON.Vector3(position.x, position.y + 0.6, position.z);
        const labelMaterial = new BABYLON.StandardMaterial("labelMaterial", this.scene);
        labelMaterial.diffuseTexture = dynamicTexture;
        labelMaterial.emissiveColor = new BABYLON.Color3(1, 1, 1);
        labelMaterial.backFaceCulling = false;
        plane.material = labelMaterial;

        return plane;
    }

    private addAtom(atom: Atom): BABYLON.Mesh {
        const color = this.atomColors[atom.element] || this.atomColors.default;
        const sphere = this.createSphere(atom.position, color, atom.radius);
        this.createLabel(atom.position, atom.element);
        return sphere;
    }

    private addBond(bond: Bond): BABYLON.Mesh {
        const color = this.atomColors.default;
        return this.createCylinder(bond.start, bond.end, color, bond.radius);
    }

    public loadMolecule(molecule: Molecule): void {
        // Clear existing scene objects
        this.scene.meshes.forEach(mesh => mesh.dispose());

        // Add atoms
        molecule.atoms.forEach(atom => this.addAtom(atom));

        // Add bonds
        molecule.bonds.forEach(bond => this.addBond(bond));
    }
}

// Example Usage
// Assuming you have a canvas with ID 'renderCanvas'
const viewer = new MoleculeViewer("renderCanvas");

const molecule: Molecule = {
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
