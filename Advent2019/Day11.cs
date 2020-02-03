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
            IList<string> lines = AdventUtils.ReadFileByLines(@"..\..\..\Files\Day11.txt");
            //AdventUtils.WriteLines(lines);

            string[] input = lines[0].Split(',');
            long[] inputIntClean = new long[input.Length];

            for (int index = 0; index < input.Length; index++)
            {
                inputIntClean[index] = long.Parse(input[index]);
            }
            IDictionary<int, IDictionary<int, int>> map = new Dictionary<int, IDictionary<int, int>>();

            WalkRobot(inputIntClean, map);

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
            IList<string> lines = AdventUtils.ReadFileByLines(@"..\..\..\Files\Day11.txt");
            //AdventUtils.WriteLines(lines);

            string[] input = lines[0].Split(',');
            long[] inputIntClean = new long[input.Length];

            for (int index = 0; index < input.Length; index++)
            {
                inputIntClean[index] = long.Parse(input[index]);
            }
            IDictionary<int, IDictionary<int, int>> map = new Dictionary<int, IDictionary<int, int>>();
            map[0] = new Dictionary<int, int>();
            map[0][0] = 1;

            WalkRobot(inputIntClean, map);

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

        private long RunProgram1(ref long[] inputInt, long phase, long input, ref long pointer, ref long relativePosition)
        {
            bool first = true;
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

            return 99;
        }

        private long GetOperand(long mode, long[] inputInt, long position, long relativePosition)
        {
            if (mode == 0)
            {
                if(position > inputInt.Length - 1) { return 0; }
                return inputInt[position];
            }
            else if (mode == 1)
            {
                return position;
            }
            else if (mode == 2)
            {
                if (position + relativePosition > inputInt.Length - 1) { return 0; }
                //if (position + relativePosition < 0) { return 0; }
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
            for (int index = 0; index < inputInt.Length; index++)
            {
                newInputInt[index] = inputInt[index];
            }

            return newInputInt;
        }

        private void WalkRobot(long[] inputIntClean, IDictionary<int, IDictionary<int, int>> map)
        {
            int x = 0; int y = 0; char orientation = 'N'; long[] output = new long[2];

            long[] inputInt = new List<long>(inputIntClean).ToArray();
            long pointer = 0; bool done = false; long relativePosition = 0;

            //first run
            while (true)
            {
                int inputColor = map.ContainsKey(x) && map[x].ContainsKey(y) ? map[x][y] : 0;
                //int inputColor = map[x][y];
                for (int index = 0; index < 2; index++)
                {
                    output[index] = RunProgram1(ref inputInt, inputColor, inputColor, ref pointer, ref relativePosition);

                    if (output[index] == 99)
                    {
                        done = true;
                        break;
                    }
                }

                if (done)
                {
                    break;
                }

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