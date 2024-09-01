using MoleculesWebApp.Client.Data.Model.Molecule;
using NCDK;
using NCDK.Default;
using NCDK.IO;
using NCDK.Numerics;

namespace MoleculesWebApp.Client.Services.Molecules
{
    public class MoleculesDisplayService
    {
        public static string GetMolFile(MoleculeReportModel moleculeReport)
        {
            var molecule = new AtomContainer();

            // Add atoms
            foreach (var atom in moleculeReport.AtomPositions)
            {
                var currentAtom = new Atom(atom.AtomSymbol,
                    new Vector3(Convert.ToDouble(atom.PosX ?? 0),
                                 Convert.ToDouble(atom.PosY ?? 0),
                                 Convert.ToDouble(atom.PosZ ?? 0)));
                currentAtom.Id = atom.AtomPosition.ToString();
                molecule.Add(currentAtom);
            }

            // Add bonds
            foreach (var bond in moleculeReport.MoleculeBonds)
            {
                if (bond.BondOrder is not null)
                {
                    IAtom atom1 = molecule.Atoms.First(i => i.Id == bond.Atom1Pos.ToString());
                    IAtom atom2 = molecule.Atoms.First(i => i.Id == bond.Atom2Pos.ToString());
                    molecule.AddBond(atom1, atom2, bond.BondOrderEnum());
                }
            }

            using var writer = new StringWriter();
            using var mdlWriter = new MDLV2000Writer(writer);
            mdlWriter.Write(molecule);
            mdlWriter.Close();

            return writer.ToString();
        }
    }
}
