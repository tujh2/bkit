using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Figures.Square square = new Figures.Square(5);
            Figures.Rectangle rectangle = new Figures.Rectangle(4, 18);
            Figures.Circle circle = new Figures.Circle(21.3);
            square.Print();
            rectangle.Print();
            circle.Print();
        }
    }
}
