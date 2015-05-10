namespace Home.Library.Optimisation.RootFinding
{
    using System;

    public class NewtonRaphsonAlgorithm : IRootFindingAlgorithm
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
    
                trialRoot -= trialOutput / function.FDash(trialRoot);
                trialOutput = function.F(trialRoot);
    
                count++;
            }
    
            return trialRoot;
        }
    }
}
