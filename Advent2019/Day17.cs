using System;
using System.Collections.Generic;

namespace Advent2019
{
    public class Day17
    {
        public void Task1()
        {
            long[] inputInt = ReadAndParse();

            IntCode intCode = new IntCode(inputInt, new long[0]);

            long result = 0;
            ISet<Point> wallPoints = new HashSet<Point>();
            int x = 0; int y = 0;
            while (true)
            {
                long output = intCode.RunProgram();
                if (output == 99) { break; }
                Point wallPoint = new Point() { X = x++, Y = y };
                if (output != 10 && output != 46)
                {
                    wallPoints.Add(wallPoint);
                }

                if (output == 10)
                {
                    x = 0; y++;
                }

                if (output == 10)
                {
                    Console.WriteLine();
                }
                else
                {
                    Console.Write((char)output);
                }
            }

            foreach (Point wallPoint in wallPoints)
            {
                Point pointUp = new Point() { X = wallPoint.X, Y = wallPoint.Y + 1};
                Point pointRight = new Point() { X = wallPoint.X + 1, Y = wallPoint.Y };
                Point pointDown = new Point() { X = wallPoint.X, Y = wallPoint.Y - 1 };
                Point pointLeft = new Point() { X = wallPoint.X - 1, Y = wallPoint.Y };

                if(wallPoints.Contains(pointUp) && wallPoints.Contains(pointRight) && wallPoints.Contains(pointDown) && wallPoints.Contains(pointLeft))
                {
                    result += wallPoint.X * wallPoint.Y;
                }
            }

            //5056
            Console.WriteLine("Day 17 task 1 : " + result);
        }

        public void Task2()
        {
            long[] inputInt = ReadAndParse();
            inputInt[0] = 2;

            IntCode intCode = new IntCode(inputInt, new long[0]);
            int result = 0;


            //
            Console.WriteLine("Day 17 task 2 : " + result);
        }

        private long[] ReadAndParse()
        {
            IList<string> lines = AdventUtils.ReadFileByLines(@"..\..\..\Files\Day17.txt");
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