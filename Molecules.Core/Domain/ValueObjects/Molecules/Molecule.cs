using Molecules.Core.Domain.ValueObjects.AtomData;
using Molecules.Core.Domain.ValueObjects.Calc.Order;
using Molecules.Shared;
using Molecules.Shared.Constants;
using System.Text;
using System.Text.Json.Serialization;

namespace Molecules.Core.Domain.ValueObjects.Molecules
{
    public class Molecule
    {

        public Molecule()
        {

        }

        public Molecule(CalcOrderItemDetails calcDetails)
        {
            int counter = 1;
            foreach (var (symbol, x, y, z) in XyzConversion.ParseXyz(calcDetails.Xyz))
            {
                Atoms.Add(new Atom()
                {
                    Position = counter++,
                    Symbol = symbol,
                    Number = AtomPropertiesTable.GetAtomProperties(symbol)?.AtomNumber ?? 0,
                    PosX = x,
                    PosY = y,
                    PosZ = z
                });
            }
        }

        public string Name { get; set; } = string.Empty;

        public string CalcValidityRemarks { get; set; } = string.Empty;

        public List<Bond> Bonds { get; set; } = [];

        public List<Atom> Atoms { get; set; } = [];

        public List<ElectronicPotential> ElPot { get; set; } = [];

        public int? Charge { get; set; }

        public double? DftEnergy { get; set; }

        public double? HFEnergy { get; set; }

        public double? HFEnergyHOMO { get; set; }

        public double? HFEnergyLUMO { get; set; }

        [JsonIgnore]
        public double? IonisationEnergy => HFEnergyHOMO - HFEnergy;

        [JsonIgnore]
        public double? ElectronAffinitiy => HFEnergy - HFEnergyLUMO;

        [JsonIgnore]
        public double? ChemicalPotential => 0.5 * (IonisationEnergy + ElectronAffinitiy);

        [JsonIgnore]
        public double? Hardness => 0.5 * (IonisationEnergy - ElectronAffinitiy);


        public static string GetXyzFileData(Molecule? molecule)
        {
            ArgumentNullException.ThrowIfNull(molecule);
            StringBuilder retval = new();
            if (molecule.Atoms.Count > 1)
            {
                retval.AppendLine($"{molecule.Atoms.Count}");
                retval.AppendLine();
                foreach (var ln in molecule.Atoms)
                {
                    retval.AppendLine($"{ln.Symbol}" +
                                        $" {StringConversion.ToString(ln.PosX)}" +
                                        $" {StringConversion.ToString(ln.PosY)}" +
                                        $" {StringConversion.ToString(ln.PosZ)}");
                }
            }
            return retval.ToString();
        }


        public string AtomGroup(Atom atom)
        {
            var retval = $"{atom.Symbol}{atom.Number}({atom.Position})";
            foreach (var grpItem in Bonds.Where(b => b.OverlapPopulation >= MoleculesConstants.BondThreshold
                                                            &&
                                                            (
                                                                b.Atom1Position == atom.Position
                                                                ||
                                                                b.Atom2Position == atom.Position)
                                                            ))
            {
                if (grpItem.Atom1Position == atom.Position)
                {
                    var correspondingAtom = Atoms.First(x => x.Position == grpItem.Atom2Position);
                    retval += $"{grpItem.BondSymbol}{correspondingAtom.Symbol}{correspondingAtom.Position}";
                }

                if (grpItem.Atom2Position == atom.Position)
                {
                    var correspondingAtom = Atoms.First(x => x.Position == grpItem.Atom1Position);
                    retval += $"{grpItem.BondSymbol}{correspondingAtom.Symbol}{correspondingAtom.Position}";
                }
            }
            return retval;
        }
    }
}
