namespace Home.Library.Optimisation.RootFinding
{
    using System;

    public class BaileysAlgorithm : IRootFindingAlgorithm
    {
        public double FindRoot(ObjectiveFunction function, MinimisationParameters parameters)
        {
            int count = 0;
    
            double trialRoot = parameters.InitialGuess;
            double trialOutput = function.F(trialRoot);
    
            while (Math.Abs(trialOutput) > parameters.Tolerance)
            {
                if (count > parameters.MaxIterations)
                {
                    throw new OperationCanceledException("Solution did not converge.");
                }
    
                double fPrime = function.FDash(trialRoot);
                double denominator = fPrime;
                denominator -= trialOutput * function.FDoubleDash(trialRoot) / (2 * fPrime);
    
                trialRoot -= trialOutput / denominator;
                trialOutput = function.F(trialRoot);
    
                count++;
            }
    
            return trialRoot;
        }
    }
}
