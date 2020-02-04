using System;
using System.Collections.Generic;

namespace Advent2019
{
    public class Day5
    {
        public void Task1()
        {
            long[] inputInt = ReadAndParse();

            IntCode intCode = new IntCode(inputInt, new long[1] { 1 });
            IList<long> outputs = new List<long>();

            while (true)
            {
                long output = intCode.RunProgram();
                if (output == 99) { break; }
                outputs.Add(output);
            }
            long result = outputs[outputs.Count - 1];

            //16209841
            Console.WriteLine("Day 5 task 1 : " + result);
        }

        public void Task2()
        {
            long[] inputInt = ReadAndParse();

            IntCode intCode = new IntCode(inputInt, new long[1] { 5 });
            IList<long> outputs = new List<long>();

            while (true)
            {
                long output = intCode.RunProgram();
                if (output == 99) { break; }
                outputs.Add(output);
            }
            long result = outputs[outputs.Count - 1];

            //8834787
            Console.WriteLine("Day 5 task 2 : " + result);
        }

        private long[] ReadAndParse()
        {
            IList<string> lines = AdventUtils.ReadFileByLines(@"..\..\..\Files\Day5.txt");
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