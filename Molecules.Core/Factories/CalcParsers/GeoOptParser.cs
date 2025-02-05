﻿using Molecules.Core.Domain.ValueObjects.AtomData;
using Molecules.Core.Domain.ValueObjects.Molecules;
using Molecules.Shared;

namespace Molecules.Core.Factories.CalcParsers
{
    public class GeoOptParser
    {
        private const string OptimizationResultTag = "***** EQUILIBRIUM GEOMETRY LOCATED *****";

        public static void Parse(List<string> fileLines, Molecule molecule)
        {
            bool start = false;
            int position = 1;

            if (!IsValid(fileLines))
            {
                molecule.CalcValidityRemarks += "| No valid geometry optimization result found.";
                return;
            }

            for (int c = 0; c < fileLines.Count; ++c)
            {
                var line = fileLines[c];
                if (line.Contains(OptimizationResultTag))
                {
                    start = true;
                    c += 3;
                    continue;
                }

                if (start)
                {
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        break;
                    }

                    var newatom = ParseOptAtomPosition(line);
                    newatom.Position = position;
                    newatom.Number = AtomPropertiesTable.GetAtomProperties(newatom.Symbol)?.AtomNumber ?? 0;

                    var existingAtom = molecule.Atoms.Find((i) => i.Symbol == newatom.Symbol
                                                                && i.Position == position);
                    if (existingAtom != null)
                    {
                        existingAtom.PosX = newatom.PosX;
                        existingAtom.PosY = newatom.PosY;
                        existingAtom.PosZ = newatom.PosZ;
                        existingAtom.AtomicWeight = newatom.AtomicWeight;
                    }
                    else
                    {
                        molecule.Atoms.Add(newatom);
                    }

                    ++position;
                }
            }
        }



        private static bool IsValid(List<string> fileLines)
        {
            return fileLines.Exists((i) => i.Contains(OptimizationResultTag));
        }


        private static Atom ParseOptAtomPosition(string line)
        {
            Atom retval = new();

            var current = FindNthSegment(line, 1);
            string atomsymbol = line[current.Item1..current.Item2];
            current = FindNthSegment(line, 2);
            string charge = line[current.Item1..current.Item2];

            string posx = line.Substring(current.Item2, 15);
            string posy = line.Substring(current.Item2 + 15, 15);
            string posz = line.Substring(current.Item2 + 30, 15);

            retval.Symbol = atomsymbol;
            retval.AtomicWeight = (int)StringConversion.ToDouble(charge);
            retval.PosX = StringConversion.ToDouble(posx);
            retval.PosY = StringConversion.ToDouble(posy);
            retval.PosZ = StringConversion.ToDouble(posz);

            return retval;
        }

        private static int FindFirstSpace(string input, int startPos = 0)
        {
            int retval = startPos;
            for (int c = startPos; c < input.Length; ++c)
            {
                if (input[c] == ' ')
                {
                    retval = c;
                    break;
                }
            }
            return retval;
        }

        private static int FindFirstNoSpace(string input, int startPos = 0)
        {
            int retval = startPos;
            for (int c = startPos; c < input.Length; ++c)
            {
                if (input[c] != ' ')
                {
                    retval = c;
                    break;
                }
            }
            return retval;
        }

        private static Tuple<int, int> FindNthSegment(string input, int n)
        {
            int currentbegin = 0;
            int currentend = 0;
            while (n > 0)
            {
                currentbegin = FindFirstNoSpace(input, currentend);
                currentend = FindFirstSpace(input, currentbegin);
                --n;
            }
            return new Tuple<int, int>(currentbegin, currentend);
        }

    }
}
