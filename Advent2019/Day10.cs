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
            
            foreach (Point point in asteroids)
            {
                point.X = point.X - bestPosition.Item2;
                point.Y = -1*point.Y + bestPosition.Item3;
            }
            
            Point shooter = new Point() { X = bestPosition.Item2, Y = bestPosition.Item3 };
            long result = 0; int counter = 0; List<double> orderedAngles = null;

            while (true)
            {
                IList<Point> removedAsteroids = new List<Point>();
                foreach (Point pointCandidate in asteroids)
                {
                    if (pointCandidate.Equals(shooter)) { continue; }

                    double angle = Math.Atan2(pointCandidate.Y, pointCandidate.X) * 180 / Math.PI;
                    if (!asteroidsToShoot.ContainsKey(angle)) { asteroidsToShoot[angle] = new List<Point>(); }
                    asteroidsToShoot[angle].Add(pointCandidate);
                }

                ICollection<double> anglesOrder = asteroidsToShoot.Keys;
                orderedAngles = anglesOrder.Where(x => x >= 0 && x <= 90).ToList();
                orderedAngles.Sort();
                orderedAngles.Reverse();
                GetAstoridOnAngle(orderedAngles, asteroidsToShoot, removedAsteroids, shooter);

                orderedAngles = anglesOrder.Where(x => x < 0).ToList();
                orderedAngles.Sort();
                GetAstoridOnAngle(orderedAngles, asteroidsToShoot, removedAsteroids, shooter);

                orderedAngles = anglesOrder.Where(x => x > 90).ToList();
                orderedAngles.Sort();
                orderedAngles.Reverse();
                GetAstoridOnAngle(orderedAngles, asteroidsToShoot, removedAsteroids, shooter);


                if (counter + removedAsteroids.Count < 200)
                {
                    counter = removedAsteroids.Count;
                    foreach (Point p in removedAsteroids)
                    {
                        asteroids.Remove(p);
                    }
                    removedAsteroids.Clear();
                    asteroidsToShoot.Clear();
                }
                else
                {
                    long x = removedAsteroids.ElementAt(199 - counter).X;
                    long y = removedAsteroids.ElementAt(199 - counter).Y;

                    result = ((x + bestPosition.Item2) * 100) + (-1 * y + bestPosition.Item3);
                    break;
                }
            }

            //404 x = 4 y = 4
            Console.WriteLine("Day 10 task 2 : " + result);
        }

        private void GetAstoridOnAngle(List<double> orderedAngles, IDictionary<double, IList<Point>> asteroidsToShoot, IList<Point> removedAsteroids, Point shooter)
        {
            foreach (double angle in orderedAngles)
            {
                double minDistance = double.MaxValue; Point pointOnAngle = null;
                foreach (Point point in asteroidsToShoot[angle])
                {
                    double distance = Math.Abs(point.X - shooter.X) + Math.Abs(point.Y - shooter.Y);
                    if (distance < minDistance) { minDistance = distance; pointOnAngle = point; }

                }
                removedAsteroids.Add(pointOnAngle);
            }
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
}