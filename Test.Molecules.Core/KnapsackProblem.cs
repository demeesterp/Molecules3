namespace Test.Molecules.Core
{
    using System;

    class KnapsackProblem
    {
        public static int Knapsack(int[] weights, int[] values, int capacity)
        {
            int n = weights.Length;
            int[,] dp = new int[n + 1, capacity + 1];

            // Build the DP table
            for (int i = 1; i <= n; i++)
            {
                for (int w = 1; w <= capacity; w++)
                {
                    if (weights[i - 1] <= w)
                    {
                        // Maximize value: include or exclude the item
                        dp[i, w] = Math.Max(dp[i - 1, w], values[i - 1] + dp[i - 1, w - weights[i - 1]]);
                    }
                    else
                    {
                        // Item can't be included
                        dp[i, w] = dp[i - 1, w];
                    }
                }
            }

            // Return the maximum value achievable within the given capacity
            return dp[n, capacity];
        }

        static void Test(string[] args)
        {
            int[] weights = { 2, 3, 4, 5 }; // Item weights
            int[] values = { 3, 4, 5, 6 };  // Item values
            int capacity = 5;              // Knapsack capacity

            int maxProfit = Knapsack(weights, values, capacity);

            Console.WriteLine("Maximum value in Knapsack: " + maxProfit);
        }
    }
}
