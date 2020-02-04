using System;
using System.Collections.Generic;

namespace Advent2019
{
    class Day7
    {
        public void Task1()
        {
            long[] inputInt = ReadAndParse();

            long max = int.MinValue;
            IntCode intCode = new IntCode(inputInt, new long[0]);

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

                                intCode.Reset(); intCode.SetNewReadBuffer(new long[2] { firstPhase, 0 });
                                long result = intCode.RunProgram(); intCode.Reset(); intCode.SetNewReadBuffer(new long[2] { secondPhase, result });
                                     result = intCode.RunProgram(); intCode.Reset(); intCode.SetNewReadBuffer(new long[2] { thirdPhase, result });
                                     result = intCode.RunProgram(); intCode.Reset(); intCode.SetNewReadBuffer(new long[2] { forthPhase, result });
                                     result = intCode.RunProgram(); intCode.Reset(); intCode.SetNewReadBuffer(new long[2] { fifthPhase, result });
                                     result = intCode.RunProgram();

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
            long[] inputInt = ReadAndParse();
            long max = long.MinValue;
            IntCode intCode1 = new IntCode(inputInt, new long[0]);
            IntCode intCode2 = new IntCode(inputInt, new long[0]);
            IntCode intCode3 = new IntCode(inputInt, new long[0]);
            IntCode intCode4 = new IntCode(inputInt, new long[0]);
            IntCode intCode5 = new IntCode(inputInt, new long[0]);

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

                                long result = 0; long finalResult = 0; bool firstRun = true;

                                while (true)
                                {
                                    intCode1.SetNewReadBuffer(firstRun ? new long[2] { firstPhase, result } : new long[1] { result });
                                    result = intCode1.RunProgram(); if (result == 99) { break; }

                                    intCode2.SetNewReadBuffer(firstRun ? new long[2] { secondPhase, result } : new long[1] { result });
                                    result = intCode2.RunProgram(); if (result == 99) { break; }

                                    intCode3.SetNewReadBuffer(firstRun ? new long[2] { thirdPhase, result } : new long[1] { result });
                                    result = intCode3.RunProgram(); if (result == 99) { break; }

                                    intCode4.SetNewReadBuffer(firstRun ? new long[2] { forthPhase, result } : new long[1] { result });
                                    result = intCode4.RunProgram(); if (result == 99) { break; }

                                    intCode5.SetNewReadBuffer(firstRun ? new long[2] { fifthPhase, result } : new long[1] { result });
                                    result = intCode5.RunProgram(); if (result == 99) { break; }

                                    finalResult = result;  firstRun = false;
                                }

                                if (max < finalResult) max = finalResult;

                                intCode1.Reset(); intCode2.Reset(); intCode3.Reset(); intCode4.Reset(); intCode5.Reset();
                            }
                        }
                    }
                }
            }

            //1047153
            Console.WriteLine("Day 7 task 2 : " + max);
        }
        
        private long[] ReadAndParse()
        {
            IList<string> lines = AdventUtils.ReadFileByLines(@"..\..\..\Files\Day7.txt");
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