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
            IntCode intCode = new IntCode(inputInt, new long[0]);
            IDictionary<long, IDictionary<long, int>> grid = new Dictionary<long, IDictionary<long, int>>();

            while (true)
            {
                long x = 0; long y = 0; int tile = 0; bool done = false;

                for(int index = 0;index < 3; index++)
                {
                    long output = intCode.RunProgram();
                    if (output == 99)
                    {
                        done = true;
                        break;
                    }

                    if (index == 0) { x = output; }
                    if (index == 1) { y = output; }
                    if (index == 2) { tile = (int)output; }

                }

                if (done)
                {
                    break;
                }

                if (!grid.ContainsKey(x)) { grid[x] = new Dictionary<long, int>(); }
                grid[x][y] = tile;
            }
            /*
            for (int index = 0; index < 20; index++)
            {
                foreach (int key in grid.Keys)
                {
                    if(grid[key][index] != 0)
                        Console.Write(grid[key][index]);
                    else
                        Console.Write(" ");
                }
                Console.WriteLine();
            }*/

            foreach (int key in grid.Keys)
            {
                foreach (int key2 in grid[key].Keys)
                {
                    if (grid[key][key2] == 2) { result++; }
                }
            }

           //193
           Console.WriteLine("Day 13 task 1 : " + result);
        }

        public void Task2()
        {
            long[] inputInt = ReadAndParse();

            int result = 0;
            IntCode intCode = new IntCode(inputInt, new long[0]);
            IDictionary<long, IDictionary<long, int>> grid = new Dictionary<long, IDictionary<long, int>>();

            while (true)
            {
                long x = 0; long y = 0; int tile = 0; bool done = false;

                for (int index = 0; index < 3; index++)
                {
                    long output = intCode.RunProgram();
                    if (output == 99)
                    {
                        done = true;
                        break;
                    }

                    if (index == 0) { x = output; }
                    if (index == 1) { y = output; }
                    if (index == 2) { tile = (int)output; }

                }

                if (done)
                {
                    break;
                }

                if (!grid.ContainsKey(x)) { grid[x] = new Dictionary<long, int>(); }
                grid[x][y] = tile;
            }
            /*
            for (int index = 0; index < 20; index++)
            {
                foreach (int key in grid.Keys)
                {
                    if(grid[key][index] != 0)
                        Console.Write(grid[key][index]);
                    else
                        Console.Write(" ");
                }
                Console.WriteLine();
            }*/

            foreach (int key in grid.Keys)
            {
                foreach (int key2 in grid[key].Keys)
                {
                    if (grid[key][key2] == 2) { result++; }
                }
            }

            //193
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
