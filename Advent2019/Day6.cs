using System;
using System.Collections.Generic;

namespace Advent2019
{
    public class Day6
    {
        public void Task1()
        {
            IList<string> lines = AdventUtils.ReadFileByLines(@"..\..\..\Files\Day6.txt");
            //AdventUtils.WriteLines(lines);

            IDictionary<string, IList<string>> orbits = new Dictionary<string, IList<string>>();
            IDictionary<string, int> orbitsCount = new Dictionary<string, int>();

            foreach (string line in lines)
            {
                string inOrbit = line.Substring(0, line.IndexOf(')'));
                string outOrbit = line.Substring(line.IndexOf(')') + 1, line.Length - 1 - inOrbit.Length);
                if (!orbits.ContainsKey(outOrbit))
                {
                    orbits[outOrbit] = new List<string>();
                }

                orbits[outOrbit].Add(inOrbit);
            }

            int count = 0;
            foreach (string orbit in orbits.Keys)
            {
                count += CountOrbits(orbit, orbits, orbitsCount);
            }

            //621125
            Console.WriteLine("Day 6 task 1 : " + count);
        }

        public void Task2()
        {
            IList<string> lines = AdventUtils.ReadFileByLines(@"..\..\..\Files\Day6.txt");
            //AdventUtils.WriteLines(lines);

            IDictionary<string, IList<string>> orbits = new Dictionary<string, IList<string>>();
            IDictionary<string, int> orbitsCount = new Dictionary<string, int>();

            foreach (string line in lines)
            {
                string inOrbit = line.Substring(0, line.IndexOf(')'));
                string outOrbit = line.Substring(line.IndexOf(')') + 1, line.Length - 1 - inOrbit.Length);
                if (!orbits.ContainsKey(outOrbit))
                {
                    orbits[outOrbit] = new List<string>();
                }

                orbits[outOrbit].Add(inOrbit);

                if (!orbits.ContainsKey(inOrbit))
                {
                    orbits[inOrbit] = new List<string>();
                }

                orbits[inOrbit].Add(outOrbit);
            }

            orbitsCount["YOU"] = 0;
            TravelOrbits("YOU", "SAN", orbits, orbitsCount);
            int count = orbitsCount["SAN"] - 2; 

            //550
            Console.WriteLine("Day 6 task 2 : " + count);
        }
        
        private void TravelOrbits(string orbitFrom, string orbitTo, IDictionary<string, IList<string>> orbits, IDictionary<string, int> orbitsCount)
        {
            if (orbits.ContainsKey(orbitFrom))
            {
                foreach (string inOrbit in orbits[orbitFrom])
                {
                    int count = orbitsCount[orbitFrom] + 1;
                    
                    if (!orbitsCount.ContainsKey(inOrbit) || orbitsCount[inOrbit] > count)
                    {
                        orbitsCount[inOrbit] = count;
                        TravelOrbits(inOrbit, orbitTo, orbits, orbitsCount);
                    }

                }
            }
        }
        
        private int CountOrbits(string orbit, IDictionary<string, IList<string>> orbits, IDictionary<string, int> orbitsCount)
        {
            if (orbitsCount.ContainsKey(orbit))
            {
                return orbitsCount[orbit];
            }

            int count = 0;
            if (orbits.ContainsKey(orbit))
            {
                count++;
                foreach (string inOrbit in orbits[orbit])
                {
                    count += CountOrbits(inOrbit, orbits, orbitsCount);
                }
            }
            orbitsCount[orbit] = count;
            return count;
        }
    }
}