using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Figures
{
    public abstract class Figure : IComparable
    {
        public abstract double Area();
        public abstract override string ToString();
	public int CompareTo(object obj) {
		Figure tmp = (Figure)obj;
		if(this.Area() < tmp.Area()) return -1;
		else if(this.Area() == tmp.Area()) return 0;
		else return 1;
	}
    }
}

