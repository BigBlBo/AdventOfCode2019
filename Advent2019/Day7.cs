using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advent2019
{
    class Day7
    {
        public void Task1()
        {
            IList<string> lines = AdventUtils.ReadFileByLines(@"..\..\..\Files\Day7.txt");
            //AdventUtils.WriteLines(lines);

            string[] input = lines[0].Split(',');
            int[] inputIntClean = new int[input.Length];
            int[] inputInt = new int[input.Length];
            for (int index = 0; index < input.Length; index++)
            {
                inputIntClean[index] = int.Parse(input[index]);
            }
            int max = int.MinValue;
            int result = 0;

            for (int firstPhase = 0; firstPhase < 5; firstPhase++)
            {
                for (int secondPhase = 0; secondPhase < 5; secondPhase++)
                {
                    if (secondPhase == firstPhase) continue;
                    for (int thirdPhase = 0; thirdPhase < 5; thirdPhase++)
                    {
                        if (secondPhase == thirdPhase || thirdPhase == firstPhase) continue;
                        for (int forthPhase = 0; forthPhase < 5; forthPhase++)
                        {
                            if (forthPhase == thirdPhase || forthPhase == secondPhase || forthPhase == firstPhase) continue;
                            for (int fifthPhase = 0; fifthPhase < 5; fifthPhase++)
                            {
                                if (forthPhase == fifthPhase || fifthPhase == thirdPhase || fifthPhase == secondPhase || fifthPhase == firstPhase) continue;

                                int pointer1 = 0; int pointer2 = 0; int pointer3 = 0; int pointer4 = 0; int pointer5 = 0;
                                inputInt = new List<int>(inputIntClean).ToArray();
                                result = RunProgram1(inputInt, firstPhase, 0, ref pointer1);
                                inputInt = new List<int>(inputIntClean).ToArray();
                                result = RunProgram1(inputInt, secondPhase, result, ref pointer2);
                                inputInt = new List<int>(inputIntClean).ToArray();
                                result = RunProgram1(inputInt, thirdPhase, result, ref pointer3);
                                inputInt = new List<int>(inputIntClean).ToArray();
                                result = RunProgram1(inputInt, forthPhase, result, ref pointer4);
                                inputInt = new List<int>(inputIntClean).ToArray();
                                result = RunProgram1(inputInt, fifthPhase, result, ref pointer5);

                                if (max < result) max = result;
                            }
                        }
                    }
                }
            }

            //17406
            Console.WriteLine("Day 7 task 1 : " + max);
        }

        public void Task2()
        {
            IList<string> lines = AdventUtils.ReadFileByLines(@"..\..\..\Files\Day7.txt");
            //AdventUtils.WriteLines(lines);

            string[] input = lines[0].Split(',');
            int[] inputIntClean = new int[input.Length];
            for (int index = 0; index < input.Length; index++)
            {
                inputIntClean[index] = int.Parse(input[index]);
            }
            int max = int.MinValue;
            

            for (int firstPhase = 5; firstPhase < 10; firstPhase++)
            {
                for (int secondPhase = 5; secondPhase < 10; secondPhase++)
                {
                    if (secondPhase == firstPhase) continue;
                    for (int thirdPhase = 5; thirdPhase < 10; thirdPhase++)
                    {
                        if (secondPhase == thirdPhase || thirdPhase == firstPhase) continue;
                        for (int forthPhase = 5; forthPhase < 10; forthPhase++)
                        {
                            if (forthPhase == thirdPhase || forthPhase == secondPhase || forthPhase == firstPhase) continue;
                            for (int fifthPhase = 5; fifthPhase < 10; fifthPhase++)
                            {
                                if (forthPhase == fifthPhase || fifthPhase == thirdPhase || fifthPhase == secondPhase || fifthPhase == firstPhase) continue;

                                int result = 0; int finalResult = 0;
                                int[] inputInt1 = new List<int>(inputIntClean).ToArray();
                                int[] inputInt2 = new List<int>(inputIntClean).ToArray();
                                int[] inputInt3 = new List<int>(inputIntClean).ToArray();
                                int[] inputInt4 = new List<int>(inputIntClean).ToArray();
                                int[] inputInt5 = new List<int>(inputIntClean).ToArray();
                                int pointer1 = 0; int pointer2 = 0; int pointer3 = 0; int pointer4 = 0; int pointer5 = 0;

                                //first run
                                result = RunProgram1(inputInt1, firstPhase, result, ref pointer1);
                                result = RunProgram1(inputInt2, secondPhase, result, ref pointer2);
                                result = RunProgram1(inputInt3, thirdPhase, result, ref pointer3);
                                result = RunProgram1(inputInt4, forthPhase, result, ref pointer4);
                                result = RunProgram1(inputInt5, fifthPhase, result, ref pointer5);

                                while (true)
                                {
                                    try
                                    {
                                        result = RunProgram1(inputInt1, result, result, ref pointer1);
                                        result = RunProgram1(inputInt2, result, result, ref pointer2);
                                        result = RunProgram1(inputInt3, result, result, ref pointer3);
                                        result = RunProgram1(inputInt4, result, result, ref pointer4);
                                        result = RunProgram1(inputInt5, result, result, ref pointer5);

                                    }
                                    catch
                                    {
                                        break;
                                    }

                                    finalResult = result;
                                }
                                if (max < finalResult) max = finalResult;
                            }
                        }
                    }
                }
            }

            //1047153
            Console.WriteLine("Day 7 task 2 : " + max);
        }

        private int RunProgram1(int[] inputInt, int phase, int input, ref int pointer)
        {
            bool first = true;
            for (int index = pointer; index < inputInt.Length;)
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
                    
                    inputInt[resultPos] = first ? phase : input;
                    first = false;
                    index += 2;
                }
                else if (opCode == 4)
                {
                    int operand1 = instruction[2] == 0 ? inputInt[firstPos] : firstPos;
                    pointer = index + 2;

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
