using System;

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
        }
    }
}
