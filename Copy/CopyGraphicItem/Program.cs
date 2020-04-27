using System;
using System.Drawing;

namespace CopyGraphicItem
{
    internal static class Program
    {
        private static void Main()
        {
            var rect1 = new Rectangle(new Point2D(10, 20), 100, 80, Color.Red);
            var rect2 = rect1.CopyDeep();
            rect2.Location.X += 10;
            Console.WriteLine(rect1.Location.X == rect2.Location.X);

            var circle1 = new Circle(new Point2D(20, 50), 100, Color.Green);
            var circle2 = (Circle)circle1.CopyShallow();
            circle2.Location.Y += 100;
            Console.WriteLine(circle1.Location.Y == circle2.Location.Y);

            circle1.AreaColor = Color.Yellow;
            Console.WriteLine(circle1.AreaColor == circle2.AreaColor);
            Console.ReadLine();
        }
    }
}
