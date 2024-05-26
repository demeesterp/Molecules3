using Molecules.Core.Domain.ValueObjects.Atom;
using Molecules.Core.Domain.ValueObjects.Calc.Order;
using Molecules.Shared;
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

        public decimal? DftEnergy { get; set; }

        public decimal? HFEnergy { get; set; }

        public decimal? HFEnergyHOMO { get; set; }

        public decimal? HFEnergyLUMO { get; set; }

        [JsonIgnore]
        public decimal? IonisationEnergy => HFEnergyHOMO - HFEnergy;

        [JsonIgnore]
        public decimal? ElectronAffinitiy => HFEnergy - HFEnergyLUMO;

        [JsonIgnore]
        public decimal? ChemicalPotential => 0.5M * (IonisationEnergy + ElectronAffinitiy);

        [JsonIgnore]
        public decimal? Hardness => 0.5M * (IonisationEnergy - ElectronAffinitiy);


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
    }
}
