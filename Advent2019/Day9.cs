using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Advent2019
{
    public class Day9
    {
        public void Task1()
        {
            IList<string> lines = AdventUtils.ReadFileByLines(@"..\..\..\Files\Day9.txt");
            //AdventUtils.WriteLines(lines);

            string[] input = lines[0].Split(',');
            long[] inputIntClean = new long[input.Length];

            for (int index = 0; index < input.Length; index++)
            {
                inputIntClean[index] = long.Parse(input[index]);
            }

            long pointer = 0;
            Stopwatch sw = Stopwatch.StartNew();
            long result = RunProgram1(inputIntClean, 1, 1, ref pointer);
            sw.Stop();
            //Console.WriteLine(sw.ElapsedMilliseconds);

            //1219070632396864
            Console.WriteLine("Day 9 task 1 : " + result);
        }

        public void Task2()
        {
            IList<string> lines = AdventUtils.ReadFileByLines(@"..\..\..\Files\Day9.txt");
            //AdventUtils.WriteLines(lines);

            string[] input = lines[0].Split(',');
            long[] inputIntClean = new long[input.Length];

            for (int index = 0; index < input.Length; index++)
            {
                inputIntClean[index] = int.Parse(input[index]);
            }
            
            long pointer = 0;
            Stopwatch sw = Stopwatch.StartNew();
            long result = RunProgram1(inputIntClean, 2, 2, ref pointer);
            sw.Stop();
           // Console.WriteLine(sw.ElapsedMilliseconds);

            //17406
            Console.WriteLine("Day 9 task 2 : " + result);
        }

        private long RunProgram1(long[] inputInt, long phase, long input, ref long pointer)
        {
            bool first = true; long relativePosition = 0;
            for (long index = pointer; index < inputInt.Length;)
            {
                //Console.WriteLine(inputInt[index]);
                int[] instruction = GetInstructions(inputInt[index]);

                int opCode = instruction[4];
                long firstPos = inputInt[index + 1];
                long secondPos = inputInt[index + 2];

                if (opCode == 1)
                {
                    long resultPos = inputInt[index + 3];
                    resultPos = instruction[0] == 2 ? resultPos + relativePosition : resultPos;

                    long operand1 = GetOperand(instruction[2], inputInt, firstPos, relativePosition);
                    long operand2 = GetOperand(instruction[1], inputInt, secondPos, relativePosition);

                    if (resultPos > inputInt.Length - 1) { inputInt = CopyAndExtendToSize(inputInt, resultPos + 1); }
                    inputInt[resultPos] = operand1 + operand2;
                    index += 4;
                }
                else if (opCode == 2)
                {
                    long resultPos = inputInt[index + 3];
                    resultPos = instruction[0] == 2 ? resultPos + relativePosition : resultPos;

                    long operand1 = GetOperand(instruction[2], inputInt, firstPos, relativePosition);
                    long operand2 = GetOperand(instruction[1], inputInt, secondPos, relativePosition);

                    if (resultPos > inputInt.Length - 1) { inputInt = CopyAndExtendToSize(inputInt, resultPos + 1); }
                    inputInt[resultPos] = operand1 * operand2;
                    index += 4;
                }
                else if (opCode == 3)
                {
                    long resultPos = inputInt[index + 1];
                    resultPos = instruction[2] == 2 ? resultPos + relativePosition : resultPos;

                    if (resultPos > inputInt.Length - 1) { inputInt = CopyAndExtendToSize(inputInt, resultPos + 1); }
                    inputInt[resultPos] = first ? phase : input;
                    first = false;
                    index += 2;
                }
                else if (opCode == 4)
                {
                    long operand1 = GetOperand(instruction[2], inputInt, firstPos, relativePosition);
                    pointer = index + 2;
                    index += 2;
                    //Console.Write(operand1 + ", ");
                    return operand1;
                }
                else if (opCode == 5)
                {
                    long operand1 = GetOperand(instruction[2], inputInt, firstPos, relativePosition);
                    long operand2 = GetOperand(instruction[1], inputInt, secondPos, relativePosition);

                    index = operand1 != 0 ? operand2 : index + 3;
                }
                else if (opCode == 6)
                {
                    long operand1 = GetOperand(instruction[2], inputInt, firstPos, relativePosition);
                    long operand2 = GetOperand(instruction[1], inputInt, secondPos, relativePosition);

                    index = operand1 == 0 ? operand2 : index + 3;
                }
                else if (opCode == 7)
                {
                    long resultPos = inputInt[index + 3];
                    resultPos = instruction[0] == 2 ? resultPos + relativePosition : resultPos;
                    long operand1 = GetOperand(instruction[2], inputInt, firstPos, relativePosition);
                    long operand2 = GetOperand(instruction[1], inputInt, secondPos, relativePosition);

                    if (resultPos > inputInt.Length - 1) { inputInt = CopyAndExtendToSize(inputInt, resultPos + 1); }
                    inputInt[resultPos] = operand1 < operand2 ? 1 : 0;
                    index += 4;
                }
                else if (opCode == 8)
                {
                    long resultPos = inputInt[index + 3];
                    resultPos = instruction[0] == 2 ? resultPos + relativePosition : resultPos;
                    long operand1 = GetOperand(instruction[2], inputInt, firstPos, relativePosition);
                    long operand2 = GetOperand(instruction[1], inputInt, secondPos, relativePosition);

                    if (resultPos > inputInt.Length - 1) { inputInt = CopyAndExtendToSize(inputInt, resultPos + 1); }
                    inputInt[resultPos] = operand1 == operand2 ? 1 : 0;
                    index += 4;
                }
                else if (opCode == 9)
                {
                    long operand1 = GetOperand(instruction[2], inputInt, firstPos, relativePosition);
                    relativePosition += operand1;
                    index += 2;

                    if (instruction[3] == 9) break;
                }
                else
                {
                    throw new Exception("Unespected opcode");
                }
            }

            return inputInt[0];
        }

        private long GetOperand(long mode, long[] inputInt, long position, long relativePosition)
        {
            if(mode == 0)
            {
                return inputInt[position];
            }
            else if(mode == 1)
            {
                return position;
            }
            else if (mode == 2)
            {
                return inputInt[position + relativePosition];
            }
            else
            {
                throw new Exception();
            }
        }

        private int[] GetInstructions(long instructionRaw)
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

        private long[] CopyAndExtendToSize(long[] inputInt, long size)
        {
            //Console.WriteLine("resize");
            long[] newInputInt = new long[size];
            for(int index = 0; index < inputInt.Length; index++)
            {
                newInputInt[index] = inputInt[index];
            }

            return newInputInt;
        }
    }
}