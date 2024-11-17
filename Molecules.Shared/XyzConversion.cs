namespace Molecules.Shared
{
    public static class XyzConversion
    {
        private static readonly string[] _returns = ["\r\n", "\r", "\n"];

        public static List<(string symbol, double x, double y, double z)> ParseXyz(string xyz)
        {
            List<(string symbol, double x, double y, double z)> retval = [];

            string[] lines = xyz.Split(_returns, StringSplitOptions.None);
            foreach (var line in lines)
            {
                string[] lineItems = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                if (lineItems.Length > 2)
                {
                    retval.Add((lineItems[0],
                                    StringConversion.ToDouble(lineItems[1]),
                                        StringConversion.ToDouble(lineItems[2]),
                                            StringConversion.ToDouble(lineItems[3])));
                }
            }
            return retval;
        }
    }
}
