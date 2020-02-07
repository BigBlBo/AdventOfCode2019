using System;
using System.Collections.Generic;

namespace Advent2019
{
    public class Day8
    {
        public void Task1()
        {
            IList<string> lines = AdventUtils.ReadFileByLines(@"..\..\..\Files\Day8.txt");
            //AdventUtils.WriteLines(lines);
            IList<IDictionary<int, int>> layers = new List<IDictionary<int, int>>();
            char[] input = lines[0].ToCharArray();

            for (int index = 0; index < input.Length / 150; index++)
            {
                IDictionary<int, int> layer = new Dictionary<int, int>{ [0] = 0, [1] = 0, [2] = 0 };
                layers.Add(layer);

                for(int layerIndex = 0; layerIndex < 150; layerIndex++)
                {
                    int finaIndex = index * 150 + layerIndex;
                    int finalInput = int.Parse(input[finaIndex].ToString());
                    layer[finalInput]++;
                }
            }

            int min = 151; int max = 0;
            foreach(IDictionary<int, int> layer in layers)
            {
                if(min > layer[0])
                {
                    min = layer[0];
                    max = layer[1] * layer[2];
                }
            }
            //1690
            Console.WriteLine("Day 8 task 1 : " + max);
        }

        public void Task2()
        {
            IList<string> lines = AdventUtils.ReadFileByLines(@"..\..\..\Files\Day8.txt");
            //AdventUtils.WriteLines(lines);

            IList<char[][]> layers = new List<char[][]>();
            char[] input = lines[0].ToCharArray();

            for (int index = 0; index < input.Length / 150; index++)
            {
                layers.Add(new char[6][]);

                for (int layerIndex = 0; layerIndex < 6; layerIndex++)
                {
                    layers[index][layerIndex] = new char[25];
                    for (int layerIndex2 = 0; layerIndex2 < 25; layerIndex2++)
                    {
                        int finaIndex = index * 150 + (layerIndex * 25 + layerIndex2);
                        layers[index][layerIndex][layerIndex2] = input[finaIndex];
                    }
                }
            }

            Console.WriteLine("Day 8 task 2 : ");  //ZPZUB
            for (int layerIndex = 0; layerIndex < 6; layerIndex++)
            {
                for (int layerIndex2 = 0; layerIndex2 < 25; layerIndex2++)
                {
                    bool writePixes = false;
                    foreach(char[][] layer in layers)
                    {
                        if(layer[layerIndex][layerIndex2] == '1')
                        {
                            Console.Write("#");
                            writePixes = true;
                            break;
                        }
                        else if (layer[layerIndex][layerIndex2] == '0')
                        {
                            Console.Write(" ");
                            writePixes = true;
                            break;
                        }
                    }
                    if (!writePixes) { Console.Write(" "); }
                }
                Console.WriteLine();
            }
        }
    }
}