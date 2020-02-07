using System;
using System.Linq;

namespace Advent2019
{
    class IntCode
    {
        long[] inputIntInitial { get; set; }
        long[] inputInt { get; set; }
        long pointer { get; set; }
        long relativePosition { get; set; }
        long[] readBuffer { get; set; }
        long readPointer { get; set; }
        long readEndPointer { get; set; }

        public IntCode()
        { }

        public IntCode(long[] inputInt, long[] readBuffer)
        {
            pointer = 0; relativePosition = 0; readPointer = 0; readEndPointer = readBuffer.Length - 1; readPointer = -1;
            this.inputInt = new long[inputInt.Length];
            this.inputIntInitial = new long[inputInt.Length];
            this.readBuffer = new long[readBuffer.Length];

            for (int index = 0;index < inputInt.Length; index++)
            {
                this.inputInt[index] = inputInt[index];
                this.inputIntInitial[index] = inputInt[index];
            }

            for (int index = 0; index < readBuffer.Length; index++)
            {
                this.readBuffer[index] = readBuffer[index];
            }
        }

        public long RunProgram()
        {
            for (long index = pointer; index < inputInt.Length;)
            {
                int[] instruction = GetInstructions(inputInt[index]);

                int opCode = instruction[4];
                long firstPos =  index + 1 < inputInt.Length ? inputInt[index + 1] : 0;
                long secondPos = index + 2 < inputInt.Length ? inputInt[index + 2] : 0;

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
                    readPointer++;
                    inputInt[resultPos] = this.readBuffer[readPointer];

                    index += 2;
                }
                else if (opCode == 4)
                {
                    long operand1 = GetOperand(instruction[2], inputInt, firstPos, relativePosition);
                    pointer = index + 2;

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
                    if (instruction[3] == 9) break;

                    long operand1 = GetOperand(instruction[2], inputInt, firstPos, relativePosition);
                    relativePosition += operand1;
                    index += 2;
                }
                else
                {
                    throw new Exception("Unespected opcode");
                }
            }

            return 99;
        }

        public void Reset()
        {
            pointer = 0; relativePosition = 0;
            this.inputInt = new long[inputIntInitial.Length];

            for (int index = 0; index < inputIntInitial.Length; index++)
            {
                this.inputInt[index] = inputIntInitial[index];
            }

            readBuffer = new long[0];
            readEndPointer = -1; readPointer = -1;
        }

        public long GetValueAtPosition(int position)
        {
            return this.inputInt[position];
        }

        public void SetValueAtPosition(int position, long value)
        {
            this.inputInt[position] = value;
        }

        public void AddToReadBuffer(long readAdd)
        {
            if(readBuffer.Length < readEndPointer)
            {
                readBuffer = CopyAndExtendToSize(readBuffer, 1000);
            }

            readEndPointer++; 
            readBuffer[readEndPointer] = readAdd;
        }

        public void SetNewReadBuffer(long[] readBuffer)
        {
            this.readBuffer = readBuffer;
            readEndPointer = readBuffer.Length - 1; readPointer = -1;
        }

        public IntCode DeepCopy()
        {
            IntCode intCode = new IntCode(this.inputInt, this.readBuffer);
            intCode.inputIntInitial = this.inputIntInitial;
            intCode.pointer = this.pointer;
            intCode.relativePosition = this.relativePosition;
            intCode.readPointer = this.readPointer;
            intCode.readEndPointer = this.readEndPointer;

            return intCode;
        }

        private long GetOperand(long mode, long[] inputInt, long position, long relativePosition)
        {
            if (mode == 0)
            {
                if (position > inputInt.Length - 1) { return 0; }
                return inputInt[position];
            }
            else if (mode == 1)
            {
                return position;
            }
            else if (mode == 2)
            {
                if (position + relativePosition > inputInt.Length - 1) { return 0; }

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
            long[] newInputInt = new long[size];
            for (int index = 0; index < inputInt.Length; index++)
            {
                newInputInt[index] = inputInt[index];
            }

            return newInputInt;
        }
    }
}