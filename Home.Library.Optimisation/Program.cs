﻿namespace Home.Library.Optimisation
{
    using Home.Library.Optimisation.LeastSquares;
    using Home.Library.Optimisation.QuadProg;
    using Home.Library.Optimisation.QuadProg.ProblemGeneration;
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            Class1.Test();

            var qpProblem = new QpGeneratorSimple().Generate(200);
            IQpSolver solver = new QpSolver();
            QpReport report = solver.Solve(qpProblem);
            Console.WriteLine(report.ElapsedTime);
            Console.ReadLine();
        }
    }
}
