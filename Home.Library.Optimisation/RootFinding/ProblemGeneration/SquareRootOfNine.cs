namespace Home.Library.Optimisation.RootFinding.ProblemGeneration
{
    using System;

    public class SquareRootOfNine : IMinimisationProblem
    {
        private readonly ObjectiveFunction objectiveFunction;
    
        public SquareRootOfNine()
        {
            this.objectiveFunction = CreateObjectiveFunction();
        }
    
        public ObjectiveFunction ObjectiveFunction
        {
            get { return this.objectiveFunction; }
        }
    
        public static MinimisationParameters GetDefaultParameters()
        {
            return new MinimisationParameters.Builder()
                                             .WithInitialGuess(4)
                                             .WithLowerBound(0)
                                             .WithUpperBound(5)
                                             .Build();
        }
    
        private ObjectiveFunction CreateObjectiveFunction()
        {
            var builder = new ObjectiveFunction.Builder();
    
            return builder.WithF(x => 9 - Math.Pow(x, 2))
                          .WithFDash(x => -2 * x)
                          .WithFDoubleDash(x => -2)
                          .Build();
        }
    }
}
