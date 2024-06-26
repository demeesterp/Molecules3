﻿namespace Molecules.Core.Domain.ValueObjects.Reports
{
    public class AtomOrbitalReport
    {

        private decimal? _populationFraction;
        private decimal? _populationFractionHOMO;
        private decimal? _populationFractionLUMO;


        private decimal? _population;
        private decimal? _populationHOMO;
        private decimal? _populationLUMO;


        public string MoleculeName { get; set; } = "";
        public string AtomID { get; set; } = "";
        public int OrbitalPosition { get; set; } = 0;
        public string OrbitalSymbol { get; set; } = "";
        public decimal? PopulationFraction
        {
            get
            {
                return _populationFraction;
            }
            set
            {
                _populationFraction = value.HasValue ? Math.Round(value.Value, 6) : null;
            }
        }
        public decimal? PopulationFractionHOMO
        {
            get
            {
                return _populationFractionHOMO;
            }
            set
            {
                _populationFractionHOMO = value.HasValue ? Math.Round(value.Value, 6) : null;
            }
        }
        public decimal? PopulationFractionLUMO
        {
            get
            {
                return _populationFractionLUMO;
            }
            set
            {
                _populationFractionLUMO = value.HasValue ? Math.Round(value.Value, 6) : null;
            }
        }
        public decimal? Population
        {
            get
            {
                return _population;
            }
            set
            {
                _population = value.HasValue ? Math.Round(value.Value, 6) : null;
            }
        }
        public decimal? PopulationHOMO
        {
            get
            {
                return _populationHOMO;
            }
            set
            {
                _populationHOMO = value.HasValue ? Math.Round(value.Value, 6) : null;
            }
        }
        public decimal? PopulationLUMO
        {
            get
            {
                return _populationLUMO;
            }
            set
            {
                _populationLUMO = value.HasValue ? Math.Round(value.Value, 6) : null;
            }
        }
    }
}
