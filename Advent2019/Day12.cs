using System;
using System.Collections.Generic;

namespace Advent2019
{
    public class Day12
    {
        public void Task1()
        {
            IList<string> lines = AdventUtils.ReadFileByLines(@"..\..\..\Files\Day12.txt");
            //AdventUtils.WriteLines(lines);
            IList<Moon> Moons = ReadAndParse();

            long result = 0;

            for (int index = 0; index < 1000; index++)
            {
                for (int outMoon = 0; outMoon < Moons.Count; outMoon++)
                {
                    for (int inMoon = outMoon + 1; inMoon < Moons.Count; inMoon++)
                    {
                        if (Moons[outMoon].PosX < Moons[inMoon].PosX)
                        {
                            Moons[outMoon].VelX++; Moons[inMoon].VelX--;
                        }
                        else if (Moons[outMoon].PosX > Moons[inMoon].PosX)
                        {
                            Moons[outMoon].VelX--; Moons[inMoon].VelX++;
                        }

                        if (Moons[outMoon].PosY < Moons[inMoon].PosY)
                        {
                            Moons[outMoon].VelY++; Moons[inMoon].VelY--;
                        }
                        else if (Moons[outMoon].PosY > Moons[inMoon].PosY)
                        {
                            Moons[outMoon].VelY--; Moons[inMoon].VelY++;
                        }

                        if (Moons[outMoon].PosZ < Moons[inMoon].PosZ)
                        {
                            Moons[outMoon].VelZ++; Moons[inMoon].VelZ--;
                        }
                        else if (Moons[outMoon].PosZ > Moons[inMoon].PosZ)
                        {
                            Moons[outMoon].VelZ--; Moons[inMoon].VelZ++;
                        }
                    }
                }

                for (int outMoon = 0; outMoon < Moons.Count; outMoon++)
                {
                    Moons[outMoon].PosX += Moons[outMoon].VelX;
                    Moons[outMoon].PosY += Moons[outMoon].VelY;
                    Moons[outMoon].PosZ += Moons[outMoon].VelZ;
                }
            }

            for (int outMoon = 0; outMoon < Moons.Count; outMoon++)
            {
                long potencialEnergy = Math.Abs(Moons[outMoon].PosX) + Math.Abs(Moons[outMoon].PosY) + Math.Abs(Moons[outMoon].PosZ);
                long kineticEnergy = Math.Abs(Moons[outMoon].VelX) + Math.Abs(Moons[outMoon].VelY) + Math.Abs(Moons[outMoon].VelZ);
                result += potencialEnergy * kineticEnergy;
            }

            //7928
            Console.WriteLine("Day 12 task 1 : " + result);
        }

        public void Task2()
        {
            IList<string> lines = AdventUtils.ReadFileByLines(@"..\..\..\Files\Day12.txt");
            //AdventUtils.WriteLines(lines);
            IList<Moon> Moons = ReadAndParse();

            long result = 0;  long resultX = 1; long resultY = 1; long resultZ = 1;

            IList<MoonSingleAxis> MoonsSingleAxis = new List<MoonSingleAxis>();
            IList<MoonSingleAxis> MoonsSingleAxisInitial = new List<MoonSingleAxis>();
            foreach (Moon moon in Moons)
            {
                MoonsSingleAxis.Add(new MoonSingleAxis { Pos = moon.PosX, Vel = moon.VelX });
                MoonsSingleAxisInitial.Add(new MoonSingleAxis { Pos = moon.PosX, Vel = moon.VelX });
            }
            resultX *= SinglAxisMovement(MoonsSingleAxis, MoonsSingleAxisInitial);
            MoonsSingleAxis.Clear(); MoonsSingleAxisInitial.Clear();

            foreach (Moon moon in Moons)
            {
                MoonsSingleAxis.Add(new MoonSingleAxis { Pos = moon.PosY, Vel = moon.VelY });
                MoonsSingleAxisInitial.Add(new MoonSingleAxis { Pos = moon.PosY, Vel = moon.VelY });
            }
            resultY *= SinglAxisMovement(MoonsSingleAxis, MoonsSingleAxisInitial);
            MoonsSingleAxis.Clear(); MoonsSingleAxisInitial.Clear();
            result = AdventUtils.LCM(resultX, resultY);

            foreach (Moon moon in Moons)
            {
                MoonsSingleAxis.Add(new MoonSingleAxis { Pos = moon.PosZ, Vel = moon.VelZ });
                MoonsSingleAxisInitial.Add(new MoonSingleAxis { Pos = moon.PosZ, Vel = moon.VelZ });
            }
            resultZ *= SinglAxisMovement(MoonsSingleAxis, MoonsSingleAxisInitial);
            result = AdventUtils.LCM(result, resultZ);

            //518311327635164
            Console.WriteLine("Day 12 task 1 : " + result);
        }

        private long SinglAxisMovement(IList<MoonSingleAxis> Moons, IList<MoonSingleAxis> MoonsInitial)
        {
            long counter = 0;
            while (true)
            {
                counter++; bool done = true;
                for (int outMoon = 0; outMoon < Moons.Count; outMoon++)
                {
                    for (int inMoon = outMoon + 1; inMoon < Moons.Count; inMoon++)
                    {
                        if (Moons[outMoon].Pos < Moons[inMoon].Pos)
                        {
                            Moons[outMoon].Vel++; Moons[inMoon].Vel--;
                        }
                        else if (Moons[outMoon].Pos > Moons[inMoon].Pos)
                        {
                            Moons[outMoon].Vel--; Moons[inMoon].Vel++;
                        }
                    }
                }

                for (int outMoon = 0; outMoon < Moons.Count; outMoon++)
                {
                    Moons[outMoon].Pos += Moons[outMoon].Vel;
                }

                for (int index = 0; index < Moons.Count; index++)
                {
                    if (!Moons[index].Equals(MoonsInitial[index]))
                    {
                        done = false; break;
                    }
                }

                if (done) { return counter; }
            }
        }

        private IList<Moon> ReadAndParse()
        {
            IList<string> lines = AdventUtils.ReadFileByLines(@"..\..\..\Files\Day12.txt");
            IList<Moon> Moons = new List<Moon>();

            foreach (string line in lines)
            {
                string[] cordinates = line.Split(',');
                Moon moon = new Moon();
                moon.PosX = long.Parse(cordinates[0].Substring(cordinates[0].IndexOf('=') + 1));
                moon.PosY = long.Parse(cordinates[1].Substring(cordinates[1].IndexOf('=') + 1));
                moon.PosZ = long.Parse(cordinates[2].Substring(cordinates[2].IndexOf('=') + 1, cordinates[2].Length - 4));

                Moons.Add(moon);
            }

            return Moons;
        }
    }

    public class Moon
    {
        public long PosX { get; set; }
        public long PosY { get; set; }
        public long PosZ { get; set; }
        public long VelX { get; set; } = 0;
        public long VelY { get; set; } = 0;
        public long VelZ { get; set; } = 0;
    }

    public class MoonSingleAxis
    {
        public long Pos { get; set; }
        public long Vel { get; set; } = 0;

        public override bool Equals(object obj)
        {
            MoonSingleAxis otherMoon = (MoonSingleAxis)obj;

            if (this.Pos == otherMoon.Pos && this.Vel == otherMoon.Vel)
            {
                return true;
            }

            return false;
        }
    }
}