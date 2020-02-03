using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent2019
{
    public class Day5
    {
        public void Task1()
        {
            IList<string> lines = AdventUtils.ReadFileByLines(@"..\..\..\Files\Day5.txt");
            //AdventUtils.WriteLines(lines);

            string[] input = lines[0].Split(',');
            int[] inputInt = new int[input.Length];
            for (int index = 0; index < input.Length; index++)
            {
                inputInt[index] = int.Parse(input[index]);
            }

            int result = RunProgram1(inputInt);

            //16209841
            Console.WriteLine("Day 5 task 1 : " + result);
        }

        public void Task2()
        {
            IList<string> lines = AdventUtils.ReadFileByLines(@"..\..\..\Files\Day5.txt");
            //AdventUtils.WriteLines(lines);  19690720

            string[] input = lines[0].Split(',');
            int[] inputInt = new int[input.Length];
            for (int index = 0; index < input.Length; index++)
            {
                inputInt[index] = int.Parse(input[index]);
            }

            int result = RunProgram2(inputInt);

            //8834787
            Console.WriteLine("Day 5 task 2 : " + result);
        }

        private int RunProgram1(int[] inputInt)
        {
            for (int index = 0; index < inputInt.Length; )
            {
                int[] instruction = GetInstructions(inputInt[index]);

                int opCode = instruction[4];
                int firstPos = inputInt[index + 1];
                int secondPos = inputInt[index + 2];

                if (opCode == 1)
                {
                    int resultPos = inputInt[index + 3];
                    int operand1 = instruction[2] == 0 ? inputInt[firstPos] : firstPos;
                    int operand2 = instruction[1] == 0 ? inputInt[secondPos] : secondPos;
                    inputInt[resultPos] = operand1 + operand2;
                    index += 4;
                }
                else if (opCode == 2)
                {
                    int resultPos = inputInt[index + 3];
                    int operand1 = instruction[2] == 0 ? inputInt[firstPos] : firstPos;
                    int operand2 = instruction[1] == 0 ? inputInt[secondPos] : secondPos;
                    inputInt[resultPos] = operand1 * operand2;
                    index += 4;
                }
                else if (opCode == 3)
                {
                    int resultPos = inputInt[index + 1];
                    inputInt[resultPos] = 1;
                    index += 2;
                }
                else if (opCode == 4)
                {
                    int operand1 = instruction[2] == 0 ? inputInt[firstPos] : firstPos;
                    if(operand1 != 0)
                    {
                        return operand1;
                    }
                    index += 2;
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

        private int RunProgram2(int[] inputInt)
        {
            for (int index = 0; index < inputInt.Length;)
            {
                int[] instruction = GetInstructions(inputInt[index]);

                int opCode = instruction[4];
                int firstPos = inputInt[index + 1];
                int secondPos = inputInt[index + 2];

                if (opCode == 1)
                {
                    int resultPos = inputInt[index + 3];
                    int operand1 = instruction[2] == 0 ? inputInt[firstPos] : firstPos;
                    int operand2 = instruction[1] == 0 ? inputInt[secondPos] : secondPos;
                    inputInt[resultPos] = operand1 + operand2;
                    index += 4;
                }
                else if (opCode == 2)
                {
                    int resultPos = inputInt[index + 3];
                    int operand1 = instruction[2] == 0 ? inputInt[firstPos] : firstPos;
                    int operand2 = instruction[1] == 0 ? inputInt[secondPos] : secondPos;
                    inputInt[resultPos] = operand1 * operand2;
                    index += 4;
                }
                else if (opCode == 3)
                {
                    int resultPos = inputInt[index + 1];
                    inputInt[resultPos] = 5;
                    index += 2;
                }
                else if (opCode == 4)
                {
                    int operand1 = instruction[2] == 0 ? inputInt[firstPos] : firstPos;

                    return operand1;
                }
                else if (opCode == 5)
                {
                    int operand1 = instruction[2] == 0 ? inputInt[firstPos] : firstPos;
                    int operand2 = instruction[1] == 0 ? inputInt[secondPos] : secondPos;

                    index = operand1 != 0 ? operand2 : index + 3;
                }
                else if (opCode == 6)
                {
                    int operand1 = instruction[2] == 0 ? inputInt[firstPos] : firstPos;
                    int operand2 = instruction[1] == 0 ? inputInt[secondPos] : secondPos;

                    index = operand1 == 0 ? operand2 : index + 3;
                }
                else if (opCode == 7)
                {
                    int resultPos = inputInt[index + 3];
                    int operand1 = instruction[2] == 0 ? inputInt[firstPos] : firstPos;
                    int operand2 = instruction[1] == 0 ? inputInt[secondPos] : secondPos;

                    inputInt[resultPos] = operand1 < operand2 ? 1 : 0;
                    index += 4;
                }
                else if (opCode == 8)
                {
                    int resultPos = inputInt[index + 3];
                    int operand1 = instruction[2] == 0 ? inputInt[firstPos] : firstPos;
                    int operand2 = instruction[1] == 0 ? inputInt[secondPos] : secondPos;

                    inputInt[resultPos] = operand1 == operand2 ? 1 : 0;
                    index += 4;
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

        public int[] GetInstructions(int instructionRaw)
        {
            var instructionRawint = instructionRaw.ToString().Select(digit => int.Parse(digit.ToString()));
            int[] instructionRawintArray = instructionRawint.ToArray();
            int[] instruction = new int[5];
            Array.Clear(instruction, 0, instruction.Length);

            for (int indexInstruction = 0; indexInstruction < instructionRawintArray.Length; indexInstruction++)
            {
                instruction[5 - 1 - indexInstruction] = instructionRawintArray[instructionRawintArray.Length - 1 - indexInstruction];
            }

            return instruction;
        }
    }
}