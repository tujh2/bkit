using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.Figures
{
    public abstract class Figure : IComparable
    {
        public abstract double Area();
        public abstract override string ToString();
        public int CompareTo(Object obj) {
            if (obj == null)
                return 1;
            Figure tmp = (Figure)obj;
            if (tmp != null) {
                return Area().CompareTo(tmp.Area());
            }
            else {
                return 1;
            }
        }
    }
}

