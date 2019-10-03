using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.Figures
{
    public class Square : Figures.Rectangle, Figures.IPrint
    {
        public Square(double a) : base(a, a) { }
        public override string ToString()
        {
            return "Square, Side = " + this.Width + ", Area = " + this.Area();
        }
    }
}
