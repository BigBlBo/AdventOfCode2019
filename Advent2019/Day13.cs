using System;
using System.Collections.Generic;
using System.Text;

namespace Advent2019
{
    public class Day13
    {
        public void Task1()
        {
            long[] inputInt = ReadAndParse();

            int result = 0;
            


            //3397667
            Console.WriteLine("Day 13 task 1 : " + result);
        }

        public void Task2()
        {
            long[] inputInt = ReadAndParse();

            int result = 0;
            

            //5093620
            Console.WriteLine("Day 13 task 2 : " + result);
        }

        private long[] ReadAndParse()
        {
            IList<string> lines = AdventUtils.ReadFileByLines(@"..\..\..\Files\Day13.txt");
            //AdventUtils.WriteLines(lines);

            string[] input = lines[0].Split(',');
            long[] inputInt = new long[input.Length];
            for (int index = 0; index < input.Length; index++)
            {
                inputInt[index] = long.Parse(input[index]);
            }

            return inputInt;
        }
    }
}
