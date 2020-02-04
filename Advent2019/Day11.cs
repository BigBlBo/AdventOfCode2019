using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advent2019
{
    public class Day11
    {
        public void Task1()
        {
            long[] inputInt = ReadAndParse();
            IDictionary<int, IDictionary<int, int>> map = new Dictionary<int, IDictionary<int, int>>();

            WalkRobot(inputInt, map);

            long result = 0;// RunProgram(inputInt);
            foreach (int key in map.Keys)
            {
                result += map[key].Count;
            }
            //2172
            Console.WriteLine("Day 11 task 1 : " + result);
        }

        public void Task2()
        {
            long[] inputInt = ReadAndParse();
            IDictionary<int, IDictionary<int, int>> map = new Dictionary<int, IDictionary<int, int>>();
            map[0] = new Dictionary<int, int>();
            map[0][0] = 1;

            WalkRobot(inputInt, map);

            List<int> keys = map.Keys.ToList();
            keys.Sort();

            //JELEFGHP
            Console.WriteLine("Day 11 task 2 : ");

            for (int index = 0; index > -6; index--)
            {
                foreach (int key in keys)
                {
                    if(map[key].ContainsKey(index) && map[key][index] == 1)
                    {
                        Console.Write("#");
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
        }

        private long[] ReadAndParse()
        {
            IList<string> lines = AdventUtils.ReadFileByLines(@"..\..\..\Files\Day11.txt");
            //AdventUtils.WriteLines(lines);

            string[] input = lines[0].Split(',');
            long[] inputInt = new long[input.Length];
            for (int index = 0; index < input.Length; index++)
            {
                inputInt[index] = long.Parse(input[index]);
            }

            return inputInt;
        }

        private void WalkRobot(long[] inputInt, IDictionary<int, IDictionary<int, int>> map)
        {
            int x = 0; int y = 0; char orientation = 'N'; long[] output = new long[2];
            IntCode intCode = new IntCode(inputInt, new long[0]);

            bool done = false;

            while (true)
            {
                int inputColor = map.ContainsKey(x) && map[x].ContainsKey(y) ? map[x][y] : 0;
                intCode.SetNewReadBuffer(new long[1] { inputColor });
                for (int index = 0; index < 2; index++)
                {
                    output[index] = intCode.RunProgram();

                    if (output[index] == 99)
                    {
                        done = true;
                        break;
                    }
                }

                if (done) { break; }

                if (!map.ContainsKey(x))
                {
                    map[x] = new Dictionary<int, int>();
                }
                map[x][y] = (int)output[0];

                orientation = GetNewOrientation(orientation, output);
                GetNewPosition(orientation, ref x, ref y);
            }
        }

        private char GetNewOrientation(char orientation, long[] output)
        {
            switch (orientation)
            {
                case 'N':
                    return output[1] == 0 ? 'W' : 'E';
                case 'S':
                    return output[1] == 0 ? 'E' : 'W';
                case 'E':
                    return output[1] == 0 ? 'N' : 'S';
                case 'W':
                    return output[1] == 0 ? 'S' : 'N';
                default:
                    throw new Exception();
            }
        }

        private void GetNewPosition(char orientation, ref int x, ref int y)
        {
            switch (orientation)
            {
                case 'N':
                    y++;
                    break;
                case 'S':
                    y--;
                    break;
                case 'E':
                    x++;
                    break;
                case 'W':
                    x--;
                    break;
            }
        }
    }
}