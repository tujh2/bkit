using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Figures
{
    public class Rectangle : Figures.Figure, Figures.IPrint
    {
        private double _height;
        private double _width;
        public double Height
        {
            get
            {
                return this._height;
            }
            set
            {
                this._height = value;
            }
        }

        public double Width
        {
            get
            {
                return this._width;
            }
            set
            {
                this._width = value;
            }
        }
        public Rectangle() { this._height = 0; this._width = 0; }
        public Rectangle(double h, double w)
        {
            this._height = h;
            this._width = w;
        }
        public override double Area()
        {
            return _height * _width;
        }
        public override string ToString()
        {
            return "Rectangle, Height = " + this._height + ", Width = " + this._width + ", Area = " + this.Area();
        }
        public void Print()
        {
            Console.WriteLine(this.ToString());
        }
    }
}
