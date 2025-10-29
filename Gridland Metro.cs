using System;
using System.Collections.Generic;
using System.Linq;

class Result
{
    /*
     * Complete the 'gridlandMetro' function below.
     *
     * The function is expected to return a LONG.
     * The function accepts following parameters:
     *  1. INTEGER n
     *  2. INTEGER m
     *  3. INTEGER k
     *  4. 2D_INTEGER_ARRAY track
     */
    public static long gridlandMetro(int n, int m, int k, List<List<int>> track)
    {
        Dictionary<int, List<(int start, int end)>> rows = new Dictionary<int, List<(int, int)>>();

        foreach (var t in track)
        {
            int r = t[0];
            int c1 = t[1];
            int c2 = t[2];

            if (!rows.ContainsKey(r))
                rows[r] = new List<(int, int)>();

            rows[r].Add((Math.Min(c1, c2), Math.Max(c1, c2)));
        }

        long occupied = 0;

        foreach (var kvp in rows)
        {
            var intervals = kvp.Value.OrderBy(x => x.start).ToList();
            int currentStart = intervals[0].start;
            int currentEnd = intervals[0].end;

            for (int i = 1; i < intervals.Count; i++)
            {
                var (s, e) = intervals[i];
                if (s <= currentEnd + 1)
                {
                    currentEnd = Math.Max(currentEnd, e);
                }
                else
                {
                    occupied += currentEnd - currentStart + 1;
                    currentStart = s;
                    currentEnd = e;
                }
            }
            occupied += currentEnd - currentStart + 1;
        }

        long totalCells = (long)n * m;
        return totalCells - occupied;
    }
}

class Solution
{
    public static void Main(string[] args)
    {
        string[] firstMultipleInput = Console.ReadLine().Trim().Split(' ');

        int n = Convert.ToInt32(firstMultipleInput[0]);
        int m = Convert.ToInt32(firstMultipleInput[1]);
        int k = Convert.ToInt32(firstMultipleInput[2]);

        List<List<int>> track = new List<List<int>>();

        for (int i = 0; i < k; i++)
        {
            track.Add(Console.ReadLine().Trim().Split(' ').Select(int.Parse).ToList());
        }

        long result = Result.gridlandMetro(n, m, k, track);

        Console.WriteLine(result);
    }
}
