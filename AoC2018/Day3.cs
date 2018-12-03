using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace AoC2018
{
    public static class Day3
    {
        public static int Part1(string input)
        {
            var grid = ProgressIDs(input);
            var overlappingCells = grid.Where(g => g.Value.Count > 1);
            return overlappingCells.Count();
        }

        private static Dictionary<(int, int), HashSet<Claim>> ProgressIDs(string input)
        {
            var grid = new Dictionary<(int, int), HashSet<Claim>>();
            var lines = input.Split(Environment.NewLine).ToList();
            var claims = new List<Claim>();
            foreach (var line in lines)
            {
                claims.Add(new Claim(line));
            }

            foreach (var claim in claims)
            {
                claim.FillCells();
                foreach (var cell in claim.Cells)
                {
                    if (grid.ContainsKey(cell))
                    {
                        grid[cell].Add(claim);
                    }
                    else
                    {
                        grid.Add(cell, new HashSet<Claim> { claim });
                    }
                }
            }

            return grid;
        }

        private class Claim
        {
            public Claim(string claim)
            {
                var parts = claim.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                ID = int.Parse(parts[0].Substring(1));
                var positions = parts[2].Split(',', StringSplitOptions.RemoveEmptyEntries);
                LeftEdge = int.Parse(positions[0]);
                TopEdge = int.Parse(positions[1].Replace(":", ""));
                var size = parts[3].Split('x', StringSplitOptions.RemoveEmptyEntries);
                Width = int.Parse(size[0]);
                Height = int.Parse(size[1]);
            }

            public override string ToString() => $"#{ID} @ {LeftEdge},{TopEdge}: {Width}x{Height}";
            public int ID { get; set; }
            public int LeftEdge { get; set; }
            public int TopEdge { get; set; }
            public int Width { get; set; }
            public int Height { get; set; }
            public HashSet<(int, int)> Cells { get; set; } = new HashSet<(int, int)>();

            public void FillCells()
            {
                for (int i = LeftEdge; i < (LeftEdge + Width); i++)
                {
                    for (int j = TopEdge; j < (TopEdge + Height); j++)
                    {
                        Cells.Add((i, j));
                    }
                }
            }
        }

        public static int Part2(string input)
        {
            var IDs = new Dictionary<Claim, int>();
            var grid = ProgressIDs(input);
            var singleCells = grid.Where(g => g.Value.Count == 1).ToList();
            foreach (var cell in singleCells)
            {
                foreach (var claim in cell.Value)
                {
                    if (IDs.ContainsKey(claim))
                    {
                        IDs[claim]++;
                    }
                    else
                    {
                        IDs.Add(claim, 1);
                    }
                }
            }
            foreach (var claim in IDs)
            {
                if (claim.Key.Cells.Count == claim.Value)
                {
                    return claim.Key.ID;
                }
            }

            return 0;
        }

        public static void Run()
        {
            var day = MethodBase.GetCurrentMethod().DeclaringType.Name;
            Console.WriteLine(day);
            try
            {
                var input = File.ReadAllText($"{day}.txt");
                Console.WriteLine($"{nameof(Part1)}: {Part1(input)}");
                Console.WriteLine($"{nameof(Part2)}: {Part2(input)}");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Could not find input file!");
            }
        }
    }
}