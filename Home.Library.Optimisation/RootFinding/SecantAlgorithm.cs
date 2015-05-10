namespace Home.Library.Optimisation.RootFinding
{
    using System;

    public class SecantAlgorithm : IRootFindingAlgorithm
    {
        public double FindRoot(ObjectiveFunction function, MinimisationParameters parameters)
        {
            int count = 0;
    
            double x1 = parameters.InitialGuess;
            double x2 = parameters.UpperBound;
            double f1 = function.F(x1);
            double f2 = function.F(x2);
    
            while (Math.Abs(f2) > parameters.Tolerance)
            {
                if (count > parameters.MaxIterations)
                {
                    throw new OperationCanceledException("Solution did not converge.");
                }
    
                double deltaX = (x2 - x1) / (1 - f1 / f2);
                x1 = x2;
                x2 -= deltaX;
    
                f1 = function.F(x1);
                f2 = function.F(x2);
    
                count++;
            }
    
            return x2;
        }
    }
}
