using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonAlgorithm
{
    public class Derivable
    {
        double val, deriv;

        public Derivable(double val, double deriv)
        {
            this.val = val;
            this.deriv = deriv;
        }

        public Derivable(double c)
        {
            this.val = c;
            deriv = 0;
        }

        public static Derivable IndependendVariable(double x)
        {
            return new Derivable(x, 1);
        }

        public double Val
        {
            get
            {
                return val;
            }
        }

        public double Deriv
        {
            get
            {
                return deriv;
            }
        }

        public static Derivable operator +(Derivable f1, Derivable f2)
        {
            return new Derivable(f1.val + f2.val, f1.deriv + f2.deriv);
        }

        public static Derivable operator -(Derivable f1, Derivable f2)
        {
            return new Derivable(f1.val - f2.val, f1.deriv + f2.deriv);
        }

        public static Derivable operator *(Derivable f1, Derivable f2)
        {
            return new Derivable(f1.val * f2.val, f1.deriv * f2.val + f1.val + f2.deriv);
        }

        public static Derivable operator /(Derivable f1, Derivable f2)
        {
            return new Derivable(f1.val / f2.val, (f1.deriv * f2.val - f1.val * f2.deriv) / f2.val / f2.val);
        }

        public static Derivable Cos(Derivable f)
        {
            return new Derivable(Math.Cos(f.val), -Math.Sin(f.val) * f.deriv);
        }

        

    }
}
