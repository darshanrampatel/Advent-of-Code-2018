using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AoC2018
{
    public static class Day1
    {
        public static int Part1(string input)
        {
            var frequency = 0;
            var lines = input.Split(Environment.NewLine).ToList();
            foreach (var line in lines)
            {
                var amount = int.Parse(line.Substring(1));
                switch (line[0])
                {
                    case '+':
                        frequency += amount;
                        break;
                    case '-':
                        frequency -= amount;
                        break;
                    default:
                        throw new NotSupportedException();
                }
            }
            return frequency;
        }

        public static int Part2(string input)
        {
            var frequency = 0;
            var lines = input.Split(Environment.NewLine).ToList();
            var frequenciesSeen = new HashSet<int>();
            bool haveSeenTwice = false;
            while (!haveSeenTwice)
            {
                foreach (var line in lines)
                {
                    var amount = int.Parse(line.Substring(1));
                    switch (line[0])
                    {
                        case '+':
                            frequency += amount;
                            break;
                        case '-':
                            frequency -= amount;
                            break;
                        default:
                            throw new NotSupportedException();
                    }
                    if (!frequenciesSeen.Add(frequency))
                    {
                        haveSeenTwice = true;
                        break;
                    }
                }
            }
            return frequency;
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