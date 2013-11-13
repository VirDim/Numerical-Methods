using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ILCalc;

namespace SimpleIterationForNonlinear
{
    public class SimpleIterationForNonlinear
    {
        string func1, func2;
        double x, x0, y, y0, eps = 0.0001;
        int count;

        public SimpleIterationForNonlinear(string func1, string func2, double eps)
        {
            this.func1 = func1;
            this.func2 = func2;
            this.eps = eps;            
            x = 0;
            y = 0;
            solve();
        }

        void solve()
        {
            var calc = new CalcContext<double>("y");
            var calc1 = new CalcContext<double>("x");
            //var calc2 = new CalcContext<double>("z");

            //calc.Arguments.Add("y");
            //calc1.Arguments.Add("x");
            //calc2.Arguments.Add("x");

            calc.Functions.ImportBuiltIn();
            calc1.Functions.ImportBuiltIn();
            //calc2.Functions.ImportBuiltIn();

            string expression = func1;
            string expression1 = func2;
            //string expression2 = "2+0.05*x*z";

            count = 0;
            do
            {
                x0 = x;
                y0 = y;
                //z0 = z;
                x = calc.Evaluate(expression, new double[] { y0 });
                y = calc1.Evaluate(expression1, new double[] { x0 });
                //z = calc2.Evaluate(expression2, new double[] { z0, x0 });
                count++;
            }
            while (Math.Abs(x - x0) > eps && Math.Abs(y - y0) > eps);

            //Console.Write("x= " + x.ToString() + " y= " + y.ToString() + " z= " + z.ToString() + "\r\ncount= " + count + "\r\n");
        }

        public double X { get { return x; } }
        public double Y { get { return y; } }
        public int Count { get { return count; } }
    }
}
