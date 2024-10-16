using System.Collections.ObjectModel;

namespace Molecules.Core.Domain.ValueObjects.AtomData
{
    public static class AtomPropertiesTable
    {
        private static readonly ReadOnlyCollection<AtomProperties> _atomProperties =
            new([
                   new ("H","Hydrogen",1),
                   new ("He","Helium",2),
                   new ("Li", "Lithium", 3),
                   new ("Be","Beryllium",4),
                   new ("B", "Boron",5),
                   new ("C","Carbon",6),
                   new ("N", "Nitrogen",7),
                   new ("O", "Oxygen",8),
                   new ("F", "Fluorine",9),
                   new ("Ne","Neon",10),
                   new ("Na","Sodium",11),
                   new ("Mg","Magnesium",12),
                   new ("Al","Aluminium",13),
                   new ("Si","Silicon",14),
                   new ("P","Phosphorus",15),
                   new ("S", "Sulfur",16),
                   new ("Cl", "Chlorine", 17),
                   new ("Ar", "Argon", 18),
                   new ("K", "Potassium",19),
                   new ("Ca", "Calcium", 20),
                   new ("Fe","Iron",21)
            ]);

        public static AtomProperties? GetAtomProperties(string symbol)
        {
            return _atomProperties.FirstOrDefault(x => x.Symbol == symbol);
        }
    }
}
