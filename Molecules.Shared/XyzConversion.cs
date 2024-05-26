namespace Molecules.Shared
{
    public static class XyzConversion
    {
        private static readonly string[] _returns = ["\r\n", "\r", "\n"];

        public static List<(string symbol, decimal x, decimal y, decimal z)> ParseXyz(string xyz)
        {
            List<(string symbol, decimal x, decimal y, decimal z)> retval = [];

            string[] lines = xyz.Split(_returns, StringSplitOptions.None);
            foreach (var line in lines)
            {
                string[] lineItems = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                if (lineItems.Length > 2)
                {
                    retval.Add((lineItems[0],
                                    StringConversion.ToDecimal(lineItems[1]),
                                        StringConversion.ToDecimal(lineItems[2]),
                                            StringConversion.ToDecimal(lineItems[3])));
                }
            }
            return retval;
        }
    }
}
