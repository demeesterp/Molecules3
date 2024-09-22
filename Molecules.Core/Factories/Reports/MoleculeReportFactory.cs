using Molecules.Core.Domain.ValueObjects.Molecules;
using Molecules.Core.Domain.ValueObjects.Reports;
using Molecules.Shared;

namespace Molecules.Core.Factories.Reports
{
    public class MoleculeReportFactory : IMoleculeReportFactory
    {
        public List<MoleculeAtomPositionReport> GetAtomPositionReport(Molecule? molecule)
        {
            List<MoleculeAtomPositionReport> report = [];
            if (molecule == null) return report;
            foreach (var atom in molecule.Atoms)
            {
                MoleculeAtomPositionReport toAdd = new MoleculeAtomPositionReport()
                {
                    MoleculeName = molecule.Name,
                    AtomPosition = atom.Position,
                    AtomSymbol = atom.Symbol,
                    PosX = atom.PosX,
                    PosZ = atom.PosZ,
                    PosY = atom.PosY
                };
                report.Add(toAdd);
            }
            return report;
        }

        public List<GeneralMoleculeReport> GetGeneralMoleculeReport(Molecule? molecule)
        {
            List<GeneralMoleculeReport> report = [];
            if (molecule == null) return report;
            foreach (var atom in molecule.Atoms)
            {
                GeneralMoleculeReport toAdd = new()
                {
                    MoleculeName = molecule.Name,
                    AtomID = $"{atom.Symbol}{atom.Position}",
                    CHelpGCharge = atom.CHelpGCharge,
                    MullNeutral = atom.MullikenPopulation.HasValue ? Math.Round(atom.MullikenPopulation.Value, 6) : null,
                    MullLewisAcid = atom.MullikenPopulationLUMO.HasValue ? Math.Round(atom.MullikenPopulationLUMO.Value, 6) : null,
                    MullLewisBase = atom.MullikenPopulationHOMO.HasValue ? Math.Round(atom.MullikenPopulationHOMO.Value, 6) : null,
                };
                foreach (var orbitalReport in from item in atom.Orbitals orderby item.Position ascending
                                select new AtomOrbitalReport()
                                {
                                    AtomID = $"{atom.Symbol}{atom.Position}",
                                    MoleculeName = molecule.Name,
                                    OrbitalPosition = item.Position,
                                    OrbitalSymbol = $"{item.Symbol}",
                                    Population = item.MullikenPopulation,
                                    PopulationHOMO = item.MullikenPopulationHomo,
                                    PopulationLUMO = item.MullikenPopulationLumo,
                                    PopulationFraction = item.MullikenPopulation / atom.MullikenPopulation,
                                    PopulationFractionHOMO = item.MullikenPopulationHomo / atom.MullikenPopulationHOMO,
                                    PopulationFractionLUMO = item.MullikenPopulationLumo / atom.MullikenPopulationLUMO
                                })
                {
                    toAdd.Configuration += orbitalReport.OrbitalSymbol
                        + "(" + StringConversion.ToString(orbitalReport.PopulationFraction, "0.00") + ")";
                    toAdd.ConfigurationLewisAcid += orbitalReport.OrbitalSymbol
                        + "(" + StringConversion.ToString(orbitalReport.PopulationFractionLUMO, "0.00") + ")";
                    toAdd.ConfigurationLewisBase += orbitalReport.OrbitalSymbol
                        + "(" + StringConversion.ToString(orbitalReport.PopulationFractionHOMO, "0.00") + ")";


                    toAdd.ConfigurationItems.Add(new ConfigurationReportItem(orbitalReport.OrbitalSymbol,
                                                                                orbitalReport.Population,
                                                                                    orbitalReport.PopulationFraction));

                    toAdd.ConfigurationItemsLewisBase.Add(new ConfigurationReportItem(orbitalReport.OrbitalSymbol,
                                                                                orbitalReport.PopulationHOMO,
                                                                                    orbitalReport.PopulationFractionHOMO));

                    toAdd.ConfigurationItemsLewisAcid.Add(new ConfigurationReportItem(orbitalReport.OrbitalSymbol,
                                                                                orbitalReport.PopulationLUMO,
                                                                                    orbitalReport.PopulationFractionLUMO));
                }
                report.Add(toAdd);
            }
            return report;
        }

        public List<MoleculeAtomOrbitalReport> GetMoleculeAtomOrbitalReport(Molecule? molecule)
        {
            List<MoleculeAtomOrbitalReport> report = [];
            if (molecule == null) return report;
            foreach (var atom in molecule.Atoms)
            {
                report.Add(new MoleculeAtomOrbitalReport()
                {
                    MoleculeName = molecule.Name,
                    AtomID = $"{atom.Symbol}{atom.Position}",
                    MullikenPopulation = atom.MullikenPopulation.HasValue ? Math.Round(atom.MullikenPopulation.Value, 6) : null,
                    OrbitalReport = (from item in atom.Orbitals
                                     orderby item.Position ascending
                                     select new AtomOrbitalReport()
                                     {
                                         AtomID = $"{atom.Symbol}{atom.Position}",
                                         MoleculeName = molecule.Name,
                                         OrbitalPosition = item.Position,
                                         OrbitalSymbol = $"{item.Symbol}",
                                         PopulationFraction = item.MullikenPopulation / atom.MullikenPopulation,
                                         PopulationFractionHOMO = item.MullikenPopulationHomo / atom.MullikenPopulationHOMO,
                                         PopulationFractionLUMO = item.MullikenPopulationLumo / atom.MullikenPopulationLUMO
                                     }).ToList()
                });
            }
            return report;
        }

        public List<MoleculeAtomsChargeReport> GetMoleculeAtomsChargeReport(Molecule? molecule)
        {
            List<MoleculeAtomsChargeReport> report = [];
            if (molecule == null) return report;
            foreach (var atom in molecule.Atoms)
            {
                report.Add(new MoleculeAtomsChargeReport()
                {
                    MoleculeName = molecule.Name,
                    AtomID = $"{atom.Symbol}{atom.Position}",
                    MullikenCharge = atom.Number - atom.MullikenPopulation,
                    CHelpGHCharge = atom.CHelpGCharge,
                    GeoDiscCharge = atom.GeoDiscCharge,
                    LowdinCharge = atom.Number - atom.LowdinPopulation,
                });
            }
            return report;
        }

        public List<MoleculeBondsReport> GetMoleculeBondsReports(Molecule? molecule)
        {
            List<MoleculeBondsReport> report = [];
            if (molecule == null) return report;
            foreach (var bond in molecule.Bonds)
            {
                if (bond.OverlapPopulation >= 0.1M || bond.OverlapPopulationHOMO >= 0.1M || bond.OverlapPopulationLUMO >= 0.1M)
                {
                    Atom? atom1 = molecule.Atoms.Find(a => a.Position == bond.Atom1Position);
                    Atom? atom2 = molecule.Atoms.Find(a => a.Position == bond.Atom2Position);

                    report.Add(new MoleculeBondsReport()
                    {
                        MoleculeName = molecule.Name,
                        BondID = $"{atom1?.Symbol}{atom1?.Position}-{atom2?.Symbol}{atom2?.Position}",
                        Distance = bond.Distance,
                        BondOrder = bond.BondOrder,
                        Atom1Pos = bond.Atom1Position,
                        Atom2Pos = bond.Atom2Position,
                        OverlapPopulation = bond.OverlapPopulation,
                        OverlapPopulationHOMO = bond.OverlapPopulationHOMO,
                        OverlapPopulationLUMO = bond.OverlapPopulationLUMO
                    }); ;
                }
            }
            return report;
        }

        public List<MoleculeAtomsPopulationReport> GetMoleculePopulationReport(Molecule? molecule)
        {
            List<MoleculeAtomsPopulationReport> report = [];
            if (molecule == null) return report;
            foreach (var atom in molecule.Atoms)
            {
                report.Add(new MoleculeAtomsPopulationReport()
                {
                    MoleculeName = molecule.Name,
                    AtomID = $"{atom.Symbol}{atom.Position}",
                    MullikenPopulation = atom.MullikenPopulation,
                    MullikenPopulationHOMO = atom.MullikenPopulationHOMO,
                    MullikenPopulationLUMO = atom.MullikenPopulationLUMO,
                    LowdinPopulation = atom.LowdinPopulation,
                    LowdinPopulationHOMO = atom.LowdinPopulationHOMO,
                    LowdinPopulationLUMO = atom.LowdinPopulationLUMO
                });
            }
            return report;
        }
    }
}
