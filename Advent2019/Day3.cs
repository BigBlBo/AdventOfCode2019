﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Advent2019
{
    public class Day3
    {
        public void Task1()
        {
            IList<string> lines = AdventUtils.ReadFileByLines(@"..\..\..\Files\Day3.txt");
            //AdventUtils.WriteLines(lines);

            string[] inputFirst = lines[0].Split(',');
            string[] inputSecond = lines[1].Split(',');

            IDictionary<int, IDictionary<int, int>> wire1 = GetWireWay(inputFirst);
            IDictionary<int, IDictionary<int, int>> wire2 = GetWireWay(inputSecond);

            int result = int.MaxValue;

            foreach (int xPoint in wire1.Keys)
            {
                if (wire2.ContainsKey(xPoint))
                {
                    foreach (int yPoint in wire1[xPoint].Keys)
                    {
                        if (wire2[xPoint].ContainsKey(yPoint))
                        {
                            if (result > Math.Abs(xPoint) + Math.Abs(yPoint))
                            {
                                result = Math.Abs(xPoint) + Math.Abs(yPoint);
                            }
                        }
                    }
                }
            }

            //207
            Console.WriteLine("Day 3 task 1 : " + result);
        }

        public void Task2()
        {
            IList<string> lines = AdventUtils.ReadFileByLines(@"..\..\..\Files\Day3.txt");
            //AdventUtils.WriteLines(lines);

            string[] inputFirst = lines[0].Split(',');
            string[] inputSecond = lines[1].Split(',');

            IDictionary<int, IDictionary<int, int>> wire1 = GetWireWay(inputFirst);
            IDictionary<int, IDictionary<int, int>> wire2 = GetWireWay(inputSecond);

            int result = int.MaxValue;

            foreach (int xPoint in wire1.Keys)
            {
                if (wire2.ContainsKey(xPoint))
                {
                    foreach (int yPoint in wire1[xPoint].Keys)
                    {
                        if (wire2[xPoint].ContainsKey(yPoint))
                        {
                            if (result > wire1[xPoint][yPoint] + wire2[xPoint][yPoint])
                            {
                                result = wire1[xPoint][yPoint] + wire2[xPoint][yPoint];
                            }
                        }
                    }
                }
            }

            //207
            Console.WriteLine("Day 3 task 2 : " + result);
        }

        public IDictionary<int, IDictionary<int, int>> GetWireWay(string[] input)
        {
            IDictionary<int, IDictionary<int, int>> wire = new Dictionary<int, IDictionary<int, int>>();
            int x = 0; int y = 0; int step = 0;

            foreach (string wireWay in input)
            {
                int lenth = int.Parse(wireWay.Substring(1, wireWay.Length - 1));
                if (wireWay.StartsWith('R'))
                {
                    for (int index = 0; index < lenth; index++)
                    {
                        x++; step++;
                        AddWirePoint(wire, x, y, step);
                    }
                }
                else if (wireWay.StartsWith('L'))
                {
                    for (int index = 0; index < lenth; index++)
                    {
                        x--; step++;
                        AddWirePoint(wire, x, y, step);
                    }
                }
                else if (wireWay.StartsWith('U'))
                {
                    for (int index = 0; index < lenth; index++)
                    {
                        y++; step++;
                        AddWirePoint(wire, x, y, step);
                    }
                }
                else if (wireWay.StartsWith('D'))
                {
                    for (int index = 0; index < lenth; index++)
                    {
                        y--; step++;
                        AddWirePoint(wire, x, y, step);
                    }
                }
            }

            return wire;
        }

        private void AddWirePoint(IDictionary<int, IDictionary<int, int>> wire, int x, int y, int step)
        {
            if (!wire.ContainsKey(x))
            {
                wire[x] = new Dictionary<int, int>();
            }
            wire[x][y] = step;
        }
    }
}