using System.Text.Json.Serialization;

namespace MoleculesWebApp.Client.Data.ServiceAgents.Molecules
{
    public class Molecule
    {

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

    }
}
