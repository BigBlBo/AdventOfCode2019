using System;
using System.Collections.Generic;
using System.Text;

namespace Advent2019
{
    public class Day1
    {
        public void Task1()
        {
            IList<string> lines = AdventUtils.ReadFileByLines(@"..\..\..\Files\Day1.txt");
            //AdventUtils.WriteLines(lines);

            int result = 0;
            foreach(string line in lines)
            {
                result += (int)Math.Floor((decimal)int.Parse(line) / 3);
                result -= 2;
            }


            //3397667
            Console.WriteLine("Day 1 task 1 : " + result);
        }

        public void Task2()
        {
            IList<string> lines = AdventUtils.ReadFileByLines(@"..\..\..\Files\Day1.txt");
            //AdventUtils.WriteLines(lines);

            int result = 0;
            foreach (string line in lines)
            {
                result += AddFuelForFuel(int.Parse(line));
            }

            //5093620
            Console.WriteLine("Day 1 task 2 : " + result);
        }

        private int AddFuelForFuel(int weight)
        {
            int fuel = (int)Math.Floor((decimal)weight / 3);
            fuel -= 2;

            if (fuel <= 0)
            {
                return 0;
            }
            else
            {
                return fuel += AddFuelForFuel(fuel);
            }
        }
    }
}