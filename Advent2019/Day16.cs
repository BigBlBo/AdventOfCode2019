using System;
using System.Collections.Generic;

namespace Advent2019
{
    public class Day16
    {
        public void Task1()
        {
            long[] inputInt = ReadAndParse();
            long[] outputInt = new long[inputInt.Length];
            long[] pattern = new long[4] { 0, 1, 0, -1};
            long[] resultInt = new long[8];

            for (int iterations = 0; iterations < 100; iterations++)
            {
                for (int indexOutput = 0; indexOutput < outputInt.Length; indexOutput++)
                {
                    long outputValue = 0;
                    int index = 0; bool firstIteration = true;
                    while (index < inputInt.Length)
                    {
                        for (int patternIndex = 0; patternIndex < pattern.Length; patternIndex++)
                        {
                            for (int patternRepeatIndex = 0; patternRepeatIndex <= indexOutput; patternRepeatIndex++)
                            {
                                if (firstIteration || index == inputInt.Length) { firstIteration = false; continue; }
                                long inputValue = inputInt[index];
                                outputValue += inputValue * pattern[patternIndex];
                                index++;
                            }
                        }
                    }
                    outputInt[indexOutput] = Math.Abs(outputValue % 10);
                }
                inputInt = outputInt;
            }

            Array.Copy(inputInt, 0, resultInt, 0, 8);
            string result = string.Join("", resultInt);
            //45834272
            Console.WriteLine("Day 16 task 1 : " + result);
        }

        public void Task2()
        {
            long[] inputIntOrig = ReadAndParse();
            long[] inputInt = new long[inputIntOrig.Length * 10000];
            long[] outputInt = new long[inputInt.Length];
            long[] offset = new long[7];
            long[] resultInt = new long[8];

            for (int index = 0; index < 10000; index++)
            {
                Array.Copy(inputIntOrig, 0, inputInt, inputIntOrig.Length * index, inputIntOrig.Length);
            }
            Array.Copy(inputIntOrig, 0, offset, 0, 7);
            
            int offsetInt = int.Parse(string.Join("", offset));
            for (int iterations = 0; iterations < 100; iterations++)
            {
                outputInt[outputInt.Length - 1] = inputInt[outputInt.Length - 1];
                for (int index = inputInt.Length - 2; index >= offsetInt - 1; index--)
                {
                    outputInt[index] = outputInt[index + 1] + inputInt[index];
                    outputInt[index + 1] = Math.Abs(outputInt[index + 1] % 10);
                }
                inputInt = outputInt;
            }

            Array.Copy(inputInt, offsetInt, resultInt, 0, 8);
            string result = string.Join("", resultInt);
            //37615297
            Console.WriteLine("Day 16 task 2 : " + result);
        }

        private long[] ReadAndParse()
        {
            IList<string> lines = AdventUtils.ReadFileByLines(@"..\..\..\Files\Day16.txt");
            //AdventUtils.WriteLines(lines);

            char[] input = lines[0].ToCharArray();
            long[] inputInt = new long[input.Length];
            for (int index = 0; index < input.Length; index++)
            {
                inputInt[index] = long.Parse(input[index].ToString());
            }

            return inputInt;
        }
    }
}