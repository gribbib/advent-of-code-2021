using System;
using System.Linq;
using System.Collections.Generic;

namespace day3
{
    class Program
    {
        static void Main(string[] args)
        {
            int gammaRate = 0, epsilonRate = 0;
            string[] lines = System.IO.File.ReadAllLines(@"input.txt");
            int length = lines[0].Length;
            int[] counts = new int[length];
            foreach (string line in lines)
            {
                var x = Convert.ToInt32(line, 2);
                for (int i = 0; i < line.Length; i++)
                {
                    if (line[i] == '1'){
                        counts[i]++;
                    }
                }
            }

            var binaryString1 = string.Empty;
            var binaryString2 = string.Empty;
            foreach(int c in counts){
                if (c >= lines.Length / 2){
                    binaryString1 += "1";
                    binaryString2 += "0";
                }else{
                    binaryString1 += "0";
                    binaryString2 += "1";
                }
            }

            gammaRate = Convert.ToInt32(binaryString1, 2);
            epsilonRate = Convert.ToInt32(binaryString2, 2);

            Console.WriteLine(gammaRate * epsilonRate);

            var oxygenGeneratorRating = Convert.ToInt32(compareRecursion(lines.ToList(), true), 2);
            var co2ScrubberRating = Convert.ToInt32(compareRecursion(lines.ToList(), false), 2);
            Console.WriteLine(oxygenGeneratorRating * co2ScrubberRating);
        }

        private static string compareRecursion(IEnumerable<string> test, bool mostCommen, int position = 0){
            if (test.Count() == 1)
            {
                return test.First();
            }

            if(position >= test.First().Length)
            {
                return "0";
            }

            var countOnes = test.Where(l => l[position] == '1').Count();
            double countThreshold = test.Count() / 2.0;
            // Console.WriteLine($"{countThreshold} - {countOnes}");
            var newPosition = position + 1;
            if (mostCommen)
            {
                if(countOnes >= countThreshold)
                {          
                    return compareRecursion(test.Where(l => l[position] == '1'), mostCommen, newPosition);
                }
                else
                {
                    return compareRecursion(test.Where(l => l[position] == '0'), mostCommen, newPosition);
                }
            }
            else
            {
                if(countOnes < countThreshold)
                {          
                    return compareRecursion(test.Where(l => l[position] == '1'), mostCommen, newPosition);
                }
                else
                {
                    return compareRecursion(test.Where(l => l[position] == '0'), mostCommen, newPosition);
                }
            }
        }
    }
}
