using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace AoC2018
{
    public static class Day2
    {
        public static int Part1(string input)
        {
            var lines = input.Split(Environment.NewLine).ToList();
            var pairs = 0;
            var triples = 0;
            foreach (var id in lines)
            {
                var groups = id.GroupBy(c => c);
                bool hasPair = false;
                bool hasTriple = false;
                foreach (var letter in groups)
                {
                    var count = letter.Count();
                    if (!hasPair && count == 2)
                    {
                        pairs++;
                        hasPair = true;
                    }
                    else if (!hasTriple && count == 3)
                    {
                        triples++;
                        hasTriple = true;
                    }
                }
            }
            return pairs * triples;
        }

        public static string Part2(string input)
        {
            var commonLetters = string.Empty;
            var matchedID1 = string.Empty;
            var matchedID2 = string.Empty;
            var lines = input.Split(Environment.NewLine).ToList();
            foreach (var id1 in lines)
            {
                foreach (var id2 in lines)
                {
                    if (id1 != id2)
                    {
                        var differentCharacters = 0;

                        for (int i = 0; i < id1.Length; i++)
                        {
                            if (id1[i] != id2[i])
                            {
                                differentCharacters++;
                            }
                        }

                        if (differentCharacters == 1)
                        {
                            matchedID1 = id1;
                            matchedID2 = id2;
                            break;
                        }
                    }
                }
            }
            for (int i = 0; i < matchedID1.Length; i++)
            {
                if (matchedID1[i] == matchedID2[i])
                {
                    commonLetters += matchedID1[i];
                }
            }
            return commonLetters;
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