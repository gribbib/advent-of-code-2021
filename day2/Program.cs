using System;

namespace day2
{
    class Program
    {
        static void Main(string[] args)
        {
            int horizontal = 0, depth = 0, aim = 0;
            string[] lines = System.IO.File.ReadAllLines(@"input.txt");

            foreach (string line in lines)
            {
                var split = line.Split(" ");
                var x = Convert.ToInt32(split[1]);
                switch (split[0].ToLower()){
                    case "forward": horizontal += x;
                        depth += aim * x;
                        break;
                    case "down": aim += x;
                        break;
                    case "up": aim -= x;
                        break;
                }
            }

            Console.WriteLine(horizontal * depth);
        }
    }
}
