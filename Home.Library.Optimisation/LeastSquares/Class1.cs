namespace Home.Library.Optimisation.LeastSquares
{
    using System;

    class Class1
    {
        public static void ObjectiveFunction(double[] c, double[] x, ref double func, object obj)
        {
            // Objective function
            func = c[0] * Math.Pow(x[0] + x[1], 3) + c[1];
        }

        public static void Grad(double[] c, double[] x, ref double func, double[] grad, object obj)
        {
            // Objective function
            func = c[0] * Math.Pow(x[0] + x[1], 3) + c[1];

            // Gradient
            grad[0] = Math.Pow(x[0] + x[1], 3);
            grad[1] = 1;
        }

        public static void Hessian(double[] c, double[] x, ref double func, double[] grad, double[,] hess, object obj)
        {
            // Objective function
            func = c[0] * Math.Pow(x[0] + x[1], 3) + c[1];

            // Gradient
            grad[0] = Math.Pow(x[0] + x[1], 3);
            grad[1] = 1;

            // Hessian
            hess[0, 0] = 0;
            hess[0, 1] = 0;
            hess[1, 0] = 0;
            hess[1, 1] = 0;
        }

        public static int Test()
        {
            double[,] x = new double[,]
            {
                { 1, 1 },
                { 2, 3 },
                { 4, 6 },
                { 6, 2 }
            };

            double[] y = new double[] { 2, 13.6, 102, 52.3 };

            double[] parameters = new double[] { 0.3, 1.4 };

            double epsf = 0;
            double epsx = 0.000001;
            int maxits = 0;
            int stoppingCode;
            alglib.lsfitstate state;
            alglib.lsfitreport report;

            // Fitting without weights
            alglib.lsfitcreatefgh(x, y, parameters, out state);
            alglib.lsfitsetcond(state, epsf, epsx, maxits);
            alglib.lsfitfit(state, ObjectiveFunction, Grad, Hessian, null, null);
            alglib.lsfitresults(state, out stoppingCode, out parameters, out report);
            
            Console.WriteLine("{0}", stoppingCode); // EXPECTED: 2
            Console.WriteLine("{0}", alglib.ap.format(parameters, 1)); // EXPECTED: [1.5]

            // Fitting with weights
            // (you can change weights and see how it changes result)
            double[] w = new double[] { 1, 1, 1, 1 };
            alglib.lsfitcreatewfg(x, y, w, parameters, true, out state);
            alglib.lsfitsetcond(state, epsf, epsx, maxits);
            alglib.lsfitfit(state, ObjectiveFunction, Grad, null, null);
            alglib.lsfitresults(state, out stoppingCode, out parameters, out report);
            
            Console.WriteLine("{0}", stoppingCode); // EXPECTED: 2
            Console.WriteLine("{0}", alglib.ap.format(parameters, 1)); // EXPECTED: [1.5]
            Console.ReadLine();
            
            return 0;
        }
    }
    
}
