using System;
using System.Diagnostics;

namespace Advent2019
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Day1 day1 = new Day1(); day1.Task1(); day1.Task2();
            Day2 day2 = new Day2(); day2.Task1(); day2.Task2();
            Day3 day3 = new Day3(); day3.Task1(); day3.Task2();
            Day4 day4 = new Day4(); day4.Task1(); day4.Task2();
            Day5 day5 = new Day5(); day5.Task1(); day5.Task2();
            Day6 day6 = new Day6(); day6.Task1(); day6.Task2();
            Day7 day7 = new Day7(); day7.Task1(); day7.Task2();
            Day8 day8 = new Day8(); day8.Task1(); day8.Task2();
            Day9 day9 = new Day9(); day9.Task1(); day9.Task2();
            Day11 day11 = new Day11(); day11.Task1(); day11.Task2();
            Day12 day12 = new Day12(); day12.Task1(); day12.Task2();
            Day13 day13 = new Day13(); day13.Task1(); day13.Task2();
            Day14 day14 = new Day14(); day14.Task1(); day14.Task2();
            Day15 day15 = new Day15(); day15.Task1(); day15.Task2();
            
            Day16 day16 = new Day16(); day16.Task1(); day16.Task2();
            /*
            Day16 day16 = new Day16(); day16.Task1();
            Stopwatch sw = Stopwatch.StartNew();
             day16.Task2();
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);
            */
            Console.ReadLine();
        }
    }
}