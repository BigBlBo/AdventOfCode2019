using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent2019
{
    public class Day4
    {
        public void Task1()
        {
            int[] interval = ReadAndParse();

            int result = 0;
            for (int index = interval[0]; index <= interval[1]; index++)
            {
                var intList = index.ToString().Select(digit => int.Parse(digit.ToString()));
                int prevNumber = 0; bool isSort = true; bool isDouble = false;

                foreach(int number in intList)
                {
                    if(prevNumber > number)
                    {
                        isSort = false; break;
                    }
                    if(prevNumber == number)
                    {
                        isDouble = true;
                    }
                    prevNumber = number;
                }

                if (isSort && isDouble) { result++; }
            }

            //1955
            Console.WriteLine("Day 4 task 1 : " + result);
        }

        public void Task2()
        {
            int[] interval = ReadAndParse();

            int result = 0;
            for (int index = interval[0]; index <= interval[1]; index++)
            {
                var intList = index.ToString().Select(digit => int.Parse(digit.ToString()));
                bool isSort = true; bool isDouble1 = false; bool isDouble2 = false;

                int[] candidate = intList.ToArray();
                
                for (int candidateIndex = 0; candidateIndex < candidate.Length; candidateIndex++)
                {
                    if (candidateIndex >= 1 && candidate[candidateIndex - 1] > candidate[candidateIndex])
                    {
                        isSort = false; break;
                    }

                    if (candidateIndex >= 1 && candidate[candidateIndex - 1] == candidate[candidateIndex])
                    {
                        if (candidateIndex < 2 || candidate[candidateIndex - 2] != candidate[candidateIndex])
                        {
                            isDouble1 = true;
                        }

                        if (candidateIndex == candidate.Length - 1 || candidate[candidateIndex + 1] != candidate[candidateIndex])
                        {
                            isDouble2 = true;
                        }

                        if (isDouble1 ^ isDouble2) //XOR
                        {
                            isDouble1 = false; isDouble2 = false;
                        }
                    }
                }
                

                if (isSort && isDouble1 && isDouble2) { result++; /*Console.WriteLine(index);*/ }
            }

            //1319
            Console.WriteLine("Day 4 task 2 : " + result);
        }

        private int[] ReadAndParse()
        {
            IList<string> lines = AdventUtils.ReadFileByLines(@"..\..\..\Files\Day4.txt");
            //AdventUtils.WriteLines(lines);
            int[] output = new int[2];

            output[0] = int.Parse(lines[0].Split('-')[0]);
            output[1] = int.Parse(lines[0].Split('-')[1]);

            return output;
        }
    }
}