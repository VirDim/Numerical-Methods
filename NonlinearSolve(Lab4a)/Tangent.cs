using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Reflection;

namespace NonlinearSolve_Lab4a_
{
    public class Tangent
    {
        double
            a,
            b,
            eps,
            x;
        int
            count;
        string
            func,
            derFunc;
        List<string> errors;

        public Tangent(string func, string derFunc, double a, double b) : this(func, derFunc, a, b, 1e-5) { }

        public Tangent(string func, string derFunc, double a, double b, double eps)
        {
            this.func = func;
            this.derFunc = derFunc;
            this.a = a;
            this.b = b;
            this.eps = eps;
            errors = new List<string>();
            solve();
        }

        void solve()
        {
            string source =
            @"
            using System;
            class f
            {
                public static double Func(double a)
                {
                    return " + func + @";
                }
                public static double DerFunc(double a)
                {
                    return " + derFunc + @";
                }
            }   

            ";
            CodeDomProvider codeProvider = new CSharpCodeProvider();
            CompilerParameters compilerParams = new CompilerParameters();

            compilerParams.CompilerOptions = "/target:library";

            compilerParams.GenerateExecutable = false;
            compilerParams.GenerateInMemory = true;
            compilerParams.IncludeDebugInformation = false;
            compilerParams.ReferencedAssemblies.Add("System.dll");

            CompilerResults results = codeProvider.CompileAssemblyFromSource(compilerParams, source);

            foreach (CompilerError err in results.Errors)
            {
                errors.Add(err.ErrorText);
            }

            try
            {
                Assembly asm = results.CompiledAssembly;

                Type t = asm.GetType("f");

                count = 0;

                double F = (double)t.InvokeMember("Func", BindingFlags.Static | BindingFlags.Public | BindingFlags.InvokeMethod, null, null, new object[] { a });
                double dF = (double)t.InvokeMember("DerFunc", BindingFlags.Static | BindingFlags.Public | BindingFlags.InvokeMethod, null, null, new object[] { a });

                if (F * dF > 0)
                    x = a;
                else
                    x = b;

                while (Math.Abs(F) > eps)
                {
                    F = (double)t.InvokeMember("Func", BindingFlags.Static | BindingFlags.Public | BindingFlags.InvokeMethod, null, null, new object[] { x });
                    dF = (double)t.InvokeMember("DerFunc", BindingFlags.Static | BindingFlags.Public | BindingFlags.InvokeMethod, null, null, new object[] { x });
                    x = x - F / dF;
                    F = (double)t.InvokeMember("Func", BindingFlags.Static | BindingFlags.Public | BindingFlags.InvokeMethod, null, null, new object[] { x });
                    count++;
                }

                //Console.WriteLine("X :" + c + "\n Кол-во итераций : " + count);
            }
            catch { }
        }

        public double X { get { return x; } }
        public List<string> Errors { get { return errors; } }
        public int Count { get { return count; } }
    }
}
