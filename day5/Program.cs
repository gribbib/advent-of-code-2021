using System;
using System.Collections.Generic;
using System.Linq;

namespace day5
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputLines = System.IO.File.ReadAllLines(@"input.txt").ToList();
            var lines = new List<Line>();
            var coordinateSystem = new Dictionary<Point, int>();

            foreach(var inputLine in inputLines)
            {
                var split = inputLine.Split(" -> ");
                var line = new Line(split[0], split[1]);
                lines.Add(line);

                if (line.Type <= 2){
                    foreach (var point in line.GetLinePoints())
                    {
                        if (coordinateSystem.ContainsKey(point)){
                            coordinateSystem[point]++;
                        }
                        else{
                            coordinateSystem[point] = 1;
                        }
                    }
                }
            }

            int intersectionCount = coordinateSystem.Where(t => t.Value > 1).Count();
            Console.WriteLine($"Intersection Count: {intersectionCount}");
            // Console.WriteLine(string.Join(" ", coordinateSystem));
        }
    }

    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public override string ToString()
        {
            return $"({X},{Y})";
        }
        public override bool Equals(object obj)
        {            
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var point = (Point)obj;

            return this.X == point.X && this.Y == point.Y;
        }
        
        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }
    }

    public class Line
    {
        public Point Start { get; set; }

        public Point End { get; set; }

        public int Type { 
            get{
                if (Start.X == End.X && Start.Y == End.Y){
                    return 0;
                }else if (Start.X == End.X ){
                    return 1;
                }else if(Start.Y == End.Y){
                    return 2;
                }
                else {
                    return 99;
                }
            } 
        }

        public Line(string startPoint, string endPoint)
        {
                var point = startPoint.Split(",");
                Start = new Point {X = Convert.ToInt32(point[0]), Y = Convert.ToInt32(point[1])};
                
                point = endPoint.Split(",");
                End = new Point {X = Convert.ToInt32(point[0]), Y = Convert.ToInt32(point[1])};
        }

        public List<Point> GetLinePoints(){
            var returnList = new List<Point>();
            switch (Type){
                case 0: returnList.Add(Start);
                    break;
                case 1:
                    var start = Math.Min(Start.Y, End.Y);
                    var end = Math.Max(Start.Y, End.Y);
                    for (int y = start; y <= end; y++)
                    {
                        returnList.Add(new Point {X = Start.X, Y = y});
                    }
                    break;
                case 2:
                    start = Math.Min(Start.X, End.X);
                    end = Math.Max(Start.X, End.X);
                    for (int x = start; x <= end; x++)
                    {
                        returnList.Add(new Point {X = x, Y = Start.Y});
                    }
                    break;
            }
            return returnList;
        }
    }
}
