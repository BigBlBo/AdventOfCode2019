using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Advent2019
{
    class AdventUtils
    {
        public static IList<string> ReadFileByLines(string path)
        {
            string text = File.ReadAllText(@path);

            string[] lines = File.ReadAllLines(path);

            return new List<string>(lines);
        }

        public static void WriteLines(IList<string> lines)
        {
            foreach(string line in lines)
            {
                Console.WriteLine(line);
            }
            
            Console.ReadLine();
        }

        public static long GCD(long first, long second)
        {
            // Euclidean algorithm
            long x;
            while (second != 0)
            {
                x = second;
                second = first % second;
                first = x;
            }
            return first;
        }

        public static long LCM(long first, long second)
        {
            return (first * second / GCD(first, second));
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