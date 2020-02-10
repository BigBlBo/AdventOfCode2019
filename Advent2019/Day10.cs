using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent2019
{
    public class Day10
    {
        public void Task1()
        {
            ISet<Point> asteroids = ReadAndParse();

            long result = GetBestPosition(asteroids).Item1;

            //284
            Console.WriteLine("Day 10 task 1 : " + result);
        }

        public void Task2()
        {
            ISet<Point> asteroids = ReadAndParse();
            IDictionary<double, IList<Point>> asteroidsToShoot = new Dictionary<double, IList<Point>>();

            (long, long, long) bestPosition = GetBestPosition(asteroids);
            foreach(Point point in asteroids)
            {
                point.X = point.X - bestPosition.Item2;
                point.Y = point.Y - bestPosition.Item3;
            }

            //Point shooter = new Point() { X = bestPosition.Item2, Y = bestPosition.Item3 };
            Point shooter = new Point() { X = 0, Y = 0 };
            int counter = 0;

            while (true)
            {
                IList<Point> removedAsteroids = new List<Point>();
                foreach (Point pointCandidate in asteroids)
                {
                    if (pointCandidate.Equals(shooter)) { continue; }

                    //Console.WriteLine((Math.Atan2(point.Y - pointCandidate.Y, point.X - pointCandidate.X) * 180) / Math.PI);
                    double angle = Math.Atan2(shooter.Y - pointCandidate.Y, shooter.X - pointCandidate.X) * 180 / Math.PI;
                    if (!asteroidsToShoot.ContainsKey(angle)) { asteroidsToShoot[angle] = new List<Point>(); }
                    asteroidsToShoot[angle].Add(pointCandidate);

                    Console.WriteLine(angle + " " + pointCandidate.X + " " + pointCandidate.Y);
                    if (pointCandidate.X == -16 && pointCandidate.Y == -15)
                    {
                        int ii = 0;
                    }

                }

                ICollection<double> anglesOrder = asteroidsToShoot.Keys;
                List<double> orderedAngles = anglesOrder.Where(x => x >= 0 && x <= 90).ToList();
                orderedAngles.Sort();
                orderedAngles.Reverse();

                Point pointOnAngle = null;
                foreach (double angle in orderedAngles)
                {
                    double minDistance = double.MaxValue; pointOnAngle = null;
                    foreach (Point point in asteroidsToShoot[angle])
                    {
                        double distance = Math.Abs(point.X - shooter.X) + Math.Abs(point.Y - shooter.Y);
                        if (distance < minDistance) { minDistance = distance;  pointOnAngle = point; }
                        
                    }
                    removedAsteroids.Add(pointOnAngle);
                }

                //orderedAngles = anglesOrder.Where(x => x < 0 && x >= -90).ToList();
                orderedAngles = anglesOrder.Where(x => x < 0).ToList();
                //orderedAngles.Sort();
                //orderedAngles.Reverse();
                foreach (double angle in orderedAngles)
                {
                    double minDistance = double.MaxValue; pointOnAngle = null;
                    foreach (Point point in asteroidsToShoot[angle])
                    {
                        double distance = Math.Abs(point.X - shooter.X) + Math.Abs(point.Y - shooter.Y);
                        if (distance < minDistance) { minDistance = distance; pointOnAngle = point; }

                    }
                    removedAsteroids.Add(pointOnAngle);
                }
                /*
                orderedAngles = anglesOrder.Where(x => x < -90).ToList();
                orderedAngles.Sort();
                //orderedAngles.Reverse();
                foreach (double angle in orderedAngles)
                {
                    int minDistance = int.MaxValue; pointOnAngle = null;
                    foreach (Point point in asteroidsToShoot[angle])
                    {
                        if (Math.Pow(point.X - shooter.X, 2) + Math.Pow(point.Y - shooter.Y, 2) < minDistance) { pointOnAngle = point; }

                    }
                    removedAsteroids.Add(pointOnAngle);
                }
                */
                orderedAngles = anglesOrder.Where(x => x > 90).ToList();
                orderedAngles.Sort();
                foreach (double angle in orderedAngles)
                {
                    int minDistance = int.MaxValue; pointOnAngle = null;
                    foreach (Point point in asteroidsToShoot[angle])
                    {
                        if (Math.Pow(point.X - shooter.X, 2) + Math.Pow(point.Y - shooter.Y, 2) < minDistance) { pointOnAngle = point; }

                    }
                    removedAsteroids.Add(pointOnAngle);
                }
                foreach(Point p in removedAsteroids)
                {
                    asteroids.Remove(p);
                }

                Console.WriteLine(removedAsteroids.ElementAt(199).X + " " + removedAsteroids.ElementAt(199).Y);
                break;
            }
            long result = 0;

            //
            Console.WriteLine("Day 10 task 2 : " + result);
        }

        private (long, long, long) GetBestPosition(ISet<Point> asteroids)
        {
            ISet<double> angles = new HashSet<double>();

            long result = long.MinValue; long xMax = 0; long yMax = 0;

            foreach (Point pointCandidate in asteroids)
            {
                foreach (Point point in asteroids)
                {
                    if (pointCandidate.Equals(point)) { continue; }

                    //Console.WriteLine((Math.Atan2(point.Y - pointCandidate.Y, point.X - pointCandidate.X) * 180) / Math.PI);
                    double angle = Math.Atan2(point.Y - pointCandidate.Y, point.X - pointCandidate.X) * 180 / Math.PI;
                    if (!angles.Contains(angle)) { angles.Add(angle); }
                }

                if (angles.Count > result) { xMax = pointCandidate.X; yMax = pointCandidate.Y; result = angles.Count; }
                angles.Clear();
            }

            return (result, xMax, yMax);
        }

        private ISet<Point> ReadAndParse()
        {
            IList<string> lines = AdventUtils.ReadFileByLines(@"..\..\..\Files\Day10.txt");
            //AdventUtils.WriteLines(lines);
            ISet<Point> asteroids = new HashSet<Point>();
            int x = 0; int y = 0;

            foreach(string line in lines)
            {
                char[] input = line.ToCharArray();
                for(int index = 0; index < input.Length; index++)
                {
                    if(input[index] == '#')
                    {
                        asteroids.Add(new Point() { X = x, Y = y });
                    }

                    x++;
                }

                x = 0;  y++;
            }


            return asteroids;
        }
    }

    public class MyAnglesOrdering : IComparer<double>
    {
        public int Compare(double x, double y)
        {
            if(x >= 0 && x <= 90 && y >= 0 && y <= 90)
            {
                return y.CompareTo(x);
            }
            else if (x < 0 && y >= 0 && y <= 90)
            {
                return x.CompareTo(y);
            }
            else if (y < 0 && x >= 0 && x <= 90)
            {
                return x.CompareTo(y);
            }
            else if (y < 0 && y < 0)
            {
                return Math.Abs(x).CompareTo(Math.Abs(y));
            }
            else if (x >= 0 && x <= 90 && y > 90)
            {
                return 1;
            }
            else if (y >= 0 && y <= 90 && x > 90)
            {
                return -1;
            }
            else
            {
                return x.CompareTo(y);
            }
        }
    }
}