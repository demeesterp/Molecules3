using Molecules.Shared.Constants;
using System.Text.Json.Serialization;

namespace Molecules.Core.Domain.ValueObjects.Molecules
{
    public class Bond
    {
        public int Atom1Position { get; set; }

        public int Atom2Position { get; set; }

        public double? Distance { get; set; }

        public double? BondOrder { get; set; }

        public double? BondOrderMinus1 { get; set; }

        public double? BondOrderPlus1 { get; set; }

        public double? OverlapPopulation { get; set; }

        public double? OverlapPopulationMinus1 { get; set; }

        public double? OverlapPopulationPlus1 { get; set; }

        [JsonIgnore]
        public double? BondOrderHOMO => BondOrder - BondOrderMinus1;

        [JsonIgnore]
        public double? BondOrderLUMO => BondOrderPlus1 - BondOrder;

        [JsonIgnore]
        public double? OverlapPopulationHOMO => OverlapPopulation - OverlapPopulationMinus1;

        [JsonIgnore]
        public double? OverlapPopulationLUMO => OverlapPopulationPlus1 - OverlapPopulation;

        [JsonIgnore]
        public string BondSymbol
        {
            get
            {
                if ( BondOrder > MoleculesConstants.BondOrder3Threshold)
                {
                    return MoleculesConstants.TripleBondSymbol.ToString();
                }
                else if ( BondOrder > MoleculesConstants.BondOrder2Threshold)
                {
                    return MoleculesConstants.DoubleBondSymbol.ToString();
                }
                else if ( BondOrder > MoleculesConstants.BondThreshold)
                {
                    return MoleculesConstants.BondSymbol.ToString();
                }
                else
                {
                    return "..";
                }
            }
        }
    }
}
