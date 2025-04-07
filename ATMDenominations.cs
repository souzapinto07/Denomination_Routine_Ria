using System;
using System.Collections.Generic;

public class ATMDenominations
{
    static int[] PAYOUT_AMOUNTS = new int[] { 30, 50, 60, 80, 140, 230, 370, 610, 980 };
    static int[] DENOMINATIONS = new int[] { 100, 50, 10 };


    public static void Main(string[] args)
    {

        foreach (int amount in PAYOUT_AMOUNTS)
        {
            CalculatePayoutCombinations(amount);
        }
    }


    public static void CalculatePayoutCombinations(int targetAmount)
    {

        List<List<int>> allCombinations = new List<List<int>>();
        FindDenominations(targetAmount, 0, new List<int>(), allCombinations);

        Console.WriteLine($"Combinations for {targetAmount} EUR:");

        if (allCombinations.Count == 0)
        {
            Console.WriteLine("No valid combinations found.");
        }

        foreach (List<int> combination in allCombinations)
        {
            Dictionary<int, int> denominationCounts = new Dictionary<int, int>();
            foreach (int note in combination)
            {
                denominationCounts.TryAdd(note, 0);
                denominationCounts[note]++;
            }

            string combinationString = "";
            foreach (var key in denominationCounts.OrderByDescending(k => k.Key))
            {
                combinationString += $"{key.Value} x {key.Key} EUR + ";
            }

            Console.WriteLine(combinationString.TrimEnd('+', ' '));
        }

        Console.WriteLine();
    }



    static void FindDenominations(int amount, int index, List<int> current, List<List<int>> allCombinations)
    {
        if (amount == 0)
        {
            allCombinations.Add(new List<int>(current));
            return;
        }

        for (int i = index; i < DENOMINATIONS.Length; i++)
        {
            int denomination = DENOMINATIONS[i];
            if (denomination <= amount)
            {
                current.Add(denomination);
                FindDenominations(amount - denomination, i, current, allCombinations);
                current.RemoveAt(current.Count - 1);
            }
        }
    }

}