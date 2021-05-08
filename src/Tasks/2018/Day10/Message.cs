using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Tasks.Year2018.Day10
{
    public class Message
    {
        private const char DOT = '.';

        private const char HASH = '#';

        public (string, int) FindMessageWhichAppearsInTheSky(List<Point> points)
        {
            int minDiff = int.MaxValue;
            bool messageFound = false;
            int seconds = 0;

            while (!messageFound)
            {
                List<Point> pointsBeforeUpdate = points.Select(p => new Point
                {
                    Position = p.Position,
                    Velocity = p.Velocity
                }).ToList();

                foreach (Point point in points)
                {
                    int x = point.Position.x + point.Velocity.x;
                    int y = point.Position.y + point.Velocity.y;

                    point.Position = (x, y);
                }

                int minX = points.Select(p => p.Position.x).Min();
                int maxX = points.Select(p => p.Position.x).Max();
                int minY = points.Select(p => p.Position.y).Min();
                int maxY = points.Select(p => p.Position.y).Max();

                int diff = Math.Abs(maxX - minX) + Math.Abs(maxY - minY);
                if (minDiff > diff)
                {
                    minDiff = diff;
                    seconds++;
                }
                else
                {
                    messageFound = true;
                    points = pointsBeforeUpdate;
                }
            }

            string message = GetMessage(points);

            return (message, seconds);
        }

        private string GetMessage(List<Point> points)
        {
            StringBuilder message = new StringBuilder(Environment.NewLine);

            int minX = points.Select(p => p.Position.x).Min();
            int maxX = points.Select(p => p.Position.x).Max();
            int minY = points.Select(p => p.Position.y).Min();
            int maxY = points.Select(p => p.Position.y).Max();

            for (int j = minY; j <= maxY; j++)
            {
                for (int i = minX; i <= maxX; i++)
                {
                    char c = DOT;
                    foreach (Point point in points)
                    {
                        if (point.Position.x == i && point.Position.y == j)
                        {
                            c = HASH;
                            break;
                        }
                    }

                    message.Append(c);
                    if (i == maxX)
                    {
                        message.Append(Environment.NewLine);
                    }
                }
            }

            return message.ToString();
        }
    }
}
