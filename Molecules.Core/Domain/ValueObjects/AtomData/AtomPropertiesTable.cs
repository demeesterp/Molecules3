using System.Collections.ObjectModel;

namespace Molecules.Core.Domain.ValueObjects.AtomData
{
    public static class AtomPropertiesTable
    {
        private static readonly ReadOnlyCollection<AtomProperties> _atomProperties =
            new([
                   new (Atoms.H,"Hydrogen",1),
                   new (Atoms.He,"Helium",2),
                   new (Atoms.Li,"Lithium", 3),
                   new (Atoms.Be,"Beryllium",4),
                   new (Atoms.B,"Boron",5),
                   new (Atoms.C,"Carbon",6),
                   new (Atoms.N,"Nitrogen",7),
                   new (Atoms.O,"Oxygen",8),
                   new (Atoms.F,"Fluorine",9),
                   new (Atoms.Ne,"Neon",10),
                   new (Atoms.Na,"Sodium",11),
                   new (Atoms.Mg,"Magnesium",12),
                   new (Atoms.Al,"Aluminium",13),
                   new (Atoms.Si,"Silicon",14),
                   new (Atoms.P,"Phosphorus",15),
                   new (Atoms.S,"Sulfur",16),
                   new (Atoms.Cl,"Chlorine", 17),
                   new (Atoms.Ar,"Argon", 18),
                   new (Atoms.K,"Potassium",19),
                   new (Atoms.Ca,"Calcium", 20),
                   new (Atoms.Fe,"Iron",21)
            ]);

        public static AtomProperties? GetAtomProperties(string symbol)
        {
            return _atomProperties.FirstOrDefault(x => x.Symbol == Enum.Parse<Atoms>(symbol));
        }

        public static AtomProperties? GetAtomProperties(Atoms symbol)
        {
            return _atomProperties.FirstOrDefault(x => x.Symbol == symbol);
        }

        public static AtomProperties? GetAtomProperties(int atomNumber)
        {
            return _atomProperties.FirstOrDefault(x => x.AtomNumber == atomNumber);
        }
    }
}
