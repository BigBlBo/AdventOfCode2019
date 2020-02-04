using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Advent2019
{
    public class Day9
    {
        public void Task1()
        {
            long[] inputInt = ReadAndParse();

            Stopwatch sw = Stopwatch.StartNew();
            IntCode intCode = new IntCode(inputInt, new long[1] { 1 });
            long result = intCode.RunProgram();
            sw.Stop();
            //Console.WriteLine(sw.ElapsedMilliseconds);

            //2518058886
            Console.WriteLine("Day 9 task 1 : " + result);
        }

        public void Task2()
        {
            long[] inputInt = ReadAndParse();

            Stopwatch sw = Stopwatch.StartNew();
            IntCode intCode = new IntCode(inputInt, new long[1] { 2 });
            long result = intCode.RunProgram();
            sw.Stop();
            //Console.WriteLine(sw.ElapsedMilliseconds);

            //44292
            Console.WriteLine("Day 9 task 2 : " + result);
        }

        private long[] ReadAndParse()
        {
            IList<string> lines = AdventUtils.ReadFileByLines(@"..\..\..\Files\Day9.txt");
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