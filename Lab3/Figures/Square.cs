using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Figures
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
