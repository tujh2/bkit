using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Figures
{
    public class Circle : Figures.Figure, Figures.IPrint
    {
        private double _r;
        public double R
        {
            get
            {
                return this._r;
            }
            set
            {
                this._r = value;
            }
        }
        public Circle() { this._r = 0; }
        public Circle(double r)
        {
            this._r = r;
        }

        public override double Area()
        {
            return Math.PI * this._r * this._r;
        }
        public override string ToString()
        {
            return "Circle, Radius = " + this._r + ", Area = " + this.Area();
        }
        public void Print()
        {
            Console.WriteLine(this.ToString());
        }
    }
}
