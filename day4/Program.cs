using System;
using System.Linq;
using System.Collections.Generic;

namespace day4
{
    class Program
    {
        static void Main(string[] args)
        {            
            var lines = System.IO.File.ReadAllLines(@"input.txt").ToList();
            var inputNumbersString = lines.FirstOrDefault().Split(",");
            int length = lines.Count;
            List<BoardNumber> AllBoardsValuesList = new List<BoardNumber>();
            int b = 0;
            for (int i = 2; i < length; i += 6)
            {
                int r = 0;
                var subLines = lines.GetRange(i,5);
                foreach (var line in subLines)
                {
                    var splitLine = line.Split(" ");
                    int c = 0;
                    foreach(var split in splitLine)
                    {
                        if(string.IsNullOrWhiteSpace(split))
                        {
                            continue;
                        }

                        AllBoardsValuesList.Add(new BoardNumber {Value = Convert.ToInt32(split), Row = r, Column = c, Board = b});
                        c++;
                    }
                    r++;
                }
                b++;
            }

            var firstFinalScore = 0;
            var latestFinalScore = 0;
            List<BoardNumber> latestWinningBoard = null;

            foreach(var input in inputNumbersString)
            {
                var inputInt = Convert.ToInt32(input);
                AllBoardsValuesList.Where(v => v.Value == inputInt).ToList().ForEach(v => v.Marked = true);
                var boards = AllBoardsValuesList.Where(v => v.Marked)
                    .GroupBy(v => v.Board, v => v, (key, subList) => 
                        new {
                            Board = key, 
                            TotalNumber = subList.Count(), 
                            RowsCountMax = subList.GroupBy(v => v.Row, v=> v, (row, rows) => rows.Count()).Max(), 
                            ColumnsCountMax = subList.GroupBy(v => v.Column, v=> v, (column, columns) => columns.Count()).Max()
                        });

                var winningBoards = boards.Where(board => board.RowsCountMax == 5 || board.ColumnsCountMax == 5);

                if (winningBoards.Any())
                {
                    foreach(var board in winningBoards){
                        latestWinningBoard = AllBoardsValuesList.Where(v => v.Board == board.Board).ToList();
                        var unmarkedSum = latestWinningBoard.Where(v => !v.Marked).Sum(v => v.Value);
                        latestFinalScore = unmarkedSum * inputInt;

                        if (firstFinalScore == 0){
                            firstFinalScore = latestFinalScore;
                        }

                        latestWinningBoard.ForEach(v => AllBoardsValuesList.Remove(v));
                    }
                }
            }

            Console.WriteLine($"First final score: {firstFinalScore}");
            Console.WriteLine($"Latest final score: {latestFinalScore}");

            // Console.WriteLine(string.Join(Environment.NewLine + Environment.NewLine, AllBoardsValuesList.GroupBy(v => v.Board, v => v, (key, subList) => string.Join(Environment.NewLine, subList.GroupBy(v => v.Row, v=> v, (row, rows) => string.Join(" ", rows))))));
        }
    }

    public class BoardNumber
    {
        public int Value { get; set; }
        public bool Marked { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public int Board { get; set; }

        public override string ToString()
        {
            return (Marked ? "[" : " ") + $"{Value,2}" + (Marked ? "]" : " ");
        }
    }
}
