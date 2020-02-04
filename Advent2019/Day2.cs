using System;
using System.Collections.Generic;

namespace Advent2019
{
    public class Day2
    {
        public void Task1()
        {
            long[] inputInt = ReadAndParse();

            IntCode intCode = new IntCode(inputInt, new long[0]);
            intCode.SetValueAtPosition(1, 12); intCode.SetValueAtPosition(2, 2);

            long result = 0;

            while(true)
            {
                long output = intCode.RunProgram();
                if (output == 99) { result = intCode.GetValueAtPosition(0); break; }
            }

            //2842648
            Console.WriteLine("Day 2 task 1 : " + result);
        }

        public void Task2()
        {
            long[] inputInt = ReadAndParse();

            IntCode intCode = new IntCode(inputInt, new long[0]);
            int result = 0; bool done = false;

            for (int opCode1 = 0; opCode1 <= 99; opCode1++)
            {
                for (int opCode2 = 0; opCode2 <= 99; opCode2++)
                {
                    intCode.Reset(); intCode.SetValueAtPosition(1, opCode1); intCode.SetValueAtPosition(2, opCode2);

                    while (true)
                    {
                        long output = intCode.RunProgram();
                        if (output == 99)
                        {
                            if (intCode.GetValueAtPosition(0) == 19690720) { result = 100 * opCode1 + opCode2; done = true; }
                            break;
                        }
                    }
                    if (done) { break; }
                }
                if (done) { break; }
            }
            
            //9074
            Console.WriteLine("Day 2 task 2 : " + result);
        }

        private long[] ReadAndParse()
        {
            IList<string> lines = AdventUtils.ReadFileByLines(@"..\..\..\Files\Day2.txt");
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