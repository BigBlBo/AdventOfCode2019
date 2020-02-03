using System;
using System.Collections.Generic;
using System.Text;

namespace Advent2019
{
    public class Day2
    {
        public void Task1()
        {
            IList<string> lines = AdventUtils.ReadFileByLines(@"..\..\..\Files\Day2.txt");
            //AdventUtils.WriteLines(lines);

            string[] input = lines[0].Split(',');
            int[] inputInt = new int[input.Length];
            for (int index = 0; index < input.Length; index++)
            {
                inputInt[index] = int.Parse(input[index]);
            }
            inputInt[1] = 12; inputInt[2] = 2;

            int result = RunProgram(inputInt);

            //2842648
            Console.WriteLine("Day 2 task 1 : " + result);
        }

        public void Task2()
        {
            IList<string> lines = AdventUtils.ReadFileByLines(@"..\..\..\Files\Day2.txt");
            //AdventUtils.WriteLines(lines);  19690720

            string[] input = lines[0].Split(',');
            int result = 0; bool done = false;

            for (int opCode1 = 0; opCode1 <= 99; opCode1++)
            {
                for (int opCode2 = 0; opCode2 <= 99; opCode2++)
                {
                    int[] inputInt = new int[input.Length];
                    for (int index = 0; index < input.Length; index++)
                    {
                        inputInt[index] = int.Parse(input[index]);
                    }
                    inputInt[1] = opCode1; inputInt[2] = opCode2;

                    if (RunProgram(inputInt) == 19690720)
                    {
                        result = 100 * opCode1 + opCode2;
                        done = true; break;
                    }
                }
                if (done) { break; }
            }
            
            //5093620
            Console.WriteLine("Day 2 task 2 : " + result);
        }

        private int RunProgram(int[] inputInt)
        {
            for (int index = 0; index < inputInt.Length; index = index + 4)
            {
                int opCode = inputInt[index];
                int firstPos = inputInt[index + 1];
                int secondPos = inputInt[index + 2];
                int resultPos = inputInt[index + 3];

                if (opCode == 1)
                {
                    inputInt[resultPos] = inputInt[firstPos] + inputInt[secondPos];
                }
                else if (opCode == 2)
                {
                    inputInt[resultPos] = inputInt[firstPos] * inputInt[secondPos];
                }
                else if (opCode == 99)
                {
                    break;
                }
                else
                {
                    throw new Exception("Unespected opcode");
                }
            }

            return inputInt[0];
        }
    }
}