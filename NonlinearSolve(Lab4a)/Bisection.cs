﻿using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CSharp;
using System.Reflection;


namespace NonlinearSolve_Lab4a_
{
    public class Bisection
    {
        double
            a,
            b,
            eps,
            c;
        int
            count;
        string
            func;
        List<string> errors;

        public Bisection(string func, double a, double b) : this(func, a, b, 1e-5) { }

        public Bisection(string func, double a, double b, double eps )
        {
            this.func = func;
            this.a = a;
            this.b = b;
            this.eps = eps;
            c = (a + b) / 2;
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
                while (b - a > 2 * eps)
                {
                    double Fa = (double)t.InvokeMember("Func", BindingFlags.Static | BindingFlags.Public | BindingFlags.InvokeMethod, null, null, new object[] { a });
                    double Fc = (double)t.InvokeMember("Func", BindingFlags.Static | BindingFlags.Public | BindingFlags.InvokeMethod, null, null, new object[] { c });

                    if (Fa * Fc > 0)
                        a = c;
                    else
                        b = c;
                    c = (a + b) / 2;
                    count++;
                }

                //Console.WriteLine("X :" + c + "\n Кол-во итераций : " + count);
            }
            catch { }
        }

        public double X { get { return c; } }
        public List<string> Errors { get { return errors; } }
        public int Count { get { return count; } }

    }
}
