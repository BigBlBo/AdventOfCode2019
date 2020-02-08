using System;
using System.Collections.Generic;

namespace Advent2019
{
    public class Day15
    {
        public void Task1()
        {
            long[] inputInt = ReadAndParse();

            IntCode intCode = new IntCode(inputInt, new long[0]);
            bool end = false;
            long result = MoveRobot(intCode, new int[0], ref end).Item1;

            //222
            Console.WriteLine("Day 15 task 1 : " + result);
        }

        public void Task2()
        {
            long[] inputInt = ReadAndParse();

            IntCode intCode = new IntCode(inputInt, new long[0]);
            bool end = false;
            intCode = MoveRobot(intCode, new int[0], ref end).Item2;
            long result = MoveOxygen(intCode);

            //394
            Console.WriteLine("Day 15 task 2 : " + result);
        }

        private (long, IntCode) MoveRobot(IntCode intCode, int[] moves, ref bool end)
        {
            int[] movesNew = new int[moves.Length + 1];
            for(int index = 0; index < moves.Length; index++)
            {
                movesNew[index] = moves[index];
            }

            var returnValue = (0L, intCode);

            if (moves.Length == 0 || moves.Length > 0 && moves[moves.Length - 1] != 2)
            {
                IntCode intCodeCopy = intCode.DeepCopy();
                intCode.SetNewReadBuffer(new long[1] { 1 });
                long returnCode = intCode.RunProgram();
                if (returnCode == 1)
                {
                    movesNew[moves.Length] = 1;
                    returnValue = MoveRobot(intCode, movesNew, ref end);
                    if (!end) { intCode = intCodeCopy; }
                }
                else if (returnCode == 2)
                {
                    end = true; return (movesNew.Length, intCode.DeepCopy());
                }
            }

            if(end) { return returnValue; }
            if(moves.Length == 0 || moves.Length > 0 && moves[moves.Length - 1] != 1)
            {
                IntCode intCodeCopy = intCode.DeepCopy();
                intCode.SetNewReadBuffer(new long[1] { 2 });
                long returnCode = intCode.RunProgram();
                if (returnCode == 1)
                {
                    movesNew[moves.Length] = 2;
                    returnValue = MoveRobot(intCode, movesNew, ref end);
                    if (!end) { intCode = intCodeCopy; }
                }
                else if (returnCode == 2)
                {
                    end = true; return (movesNew.Length, intCode.DeepCopy());
                }
            }

            if (end) { return returnValue; }
            if (moves.Length == 0 || moves.Length > 0 && moves[moves.Length - 1] != 4)
            {
                IntCode intCodeCopy = intCode.DeepCopy();
                intCode.SetNewReadBuffer(new long[1] { 3 });
                long returnCode = intCode.RunProgram();
                if (returnCode == 1)
                {
                    movesNew[moves.Length] = 3;
                    returnValue = MoveRobot(intCode, movesNew, ref end);
                    if (!end) { intCode = intCodeCopy; }
                }
                else if (returnCode == 2)
                {
                    end = true; return (movesNew.Length, intCode.DeepCopy());
                }
            }

            if (end) { return returnValue; }
            if (moves.Length == 0 || moves.Length > 0 && moves[moves.Length - 1] != 3)
            {
                IntCode intCodeCopy = intCode.DeepCopy();
                intCode.SetNewReadBuffer(new long[1] { 4 });
                long returnCode = intCode.RunProgram();
                if (returnCode == 1)
                {
                    movesNew[moves.Length] = 4;
                    returnValue = MoveRobot(intCode, movesNew, ref end);
                    //if (!end) { intCode = intCodeCopy; }
                }
                else if (returnCode == 2)
                {
                    end = true; return (movesNew.Length, intCode.DeepCopy());
                }
            }
            return returnValue;
        }

        private long MoveOxygen(IntCode intCode)
        {
            Point point = new Point() { X = 0, Y = 0 };
            IDictionary<Point, IntCode> PointState = new Dictionary<Point, IntCode>();
            PointState[point] = intCode;

            int count = 0;
            while (true)
            {
                bool canMove = false;
                IDictionary<Point, IntCode> PointStateNew = new Dictionary<Point, IntCode>();
                foreach(Point pointInitial in PointState.Keys)
                {
                    Point newPointNorth = new Point() { X = pointInitial.X, Y = pointInitial.Y + 1 };
                    if(!PointState.ContainsKey(newPointNorth))
                    {
                        IntCode intCodeCopy = PointState[pointInitial].DeepCopy();
                        intCodeCopy.SetNewReadBuffer(new long[1] { 1 });
                        long returnCode = intCodeCopy.RunProgram();
                        if(returnCode == 1)
                        {
                            PointStateNew[newPointNorth] = intCodeCopy;
                            canMove = true;
                        }
                    }

                    Point newPointSouth = new Point() { X = pointInitial.X, Y = pointInitial.Y - 1 };
                    if (!PointState.ContainsKey(newPointSouth) && !PointStateNew.ContainsKey(newPointSouth))
                    {
                        IntCode intCodeCopy = PointState[pointInitial].DeepCopy();
                        intCodeCopy.SetNewReadBuffer(new long[1] { 2 });
                        long returnCode = intCodeCopy.RunProgram();
                        if (returnCode == 1)
                        {
                            PointStateNew[newPointSouth] = intCodeCopy;
                            canMove = true;
                        }
                    }

                    Point newPointEast = new Point() { X = pointInitial.X + 1, Y = pointInitial.Y };
                    if (!PointState.ContainsKey(newPointEast) && !PointStateNew.ContainsKey(newPointEast))
                    {
                        IntCode intCodeCopy = PointState[pointInitial].DeepCopy();
                        intCodeCopy.SetNewReadBuffer(new long[1] { 3 });
                        long returnCode = intCodeCopy.RunProgram();
                        if (returnCode == 1)
                        {
                            PointStateNew[newPointEast] = intCodeCopy;
                            canMove = true;
                        }
                    }

                    Point newPointWest = new Point() { X = pointInitial.X - 1, Y = pointInitial.Y };
                    if (!PointState.ContainsKey(newPointWest) && !PointStateNew.ContainsKey(newPointWest))
                    {
                        IntCode intCodeCopy = PointState[pointInitial].DeepCopy();
                        intCodeCopy.SetNewReadBuffer(new long[1] { 4 });
                        long returnCode = intCodeCopy.RunProgram();
                        if (returnCode == 1)
                        {
                            PointStateNew[newPointWest] = intCodeCopy;
                            canMove = true;
                        }
                    }
                }

                if(canMove)
                {
                    count++;
                    foreach(Point pointNew in PointStateNew.Keys)
                    {
                        PointState[pointNew] = PointStateNew[pointNew];
                    }
                }
                else
                {
                    break;
                }
            }

            return count;
        }

        private long[] ReadAndParse()
        {
            IList<string> lines = AdventUtils.ReadFileByLines(@"..\..\..\Files\Day15.txt");
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

    class Point
    {
        public long X { get; set; }
        public long Y { get; set; }

        public override bool Equals(object obj)
        {
            Point item = obj as Point;

            if (item.X == X)
            {
                if (item.Y == Y)
                {
                    return true;
                }
            }

            return false;
        }
        public override int GetHashCode()
        {
            return X.GetHashCode() + Y.GetHashCode();
        }
    }
}