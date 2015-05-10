namespace Home.Library.Optimisation.RootFinding
{
    public class Solver
    {
        private readonly IRootFindingAlgorithm rootFindingStrategy;
        private readonly MinimisationParameters parameters;
    
        public Solver(MinimisationParameters parameters, IRootFindingAlgorithm strategy)
        {
            this.parameters = parameters;
            this.rootFindingStrategy = strategy;
        }
    
        public double FindRoot(IMinimisationProblem problem)
        {
            return this.rootFindingStrategy.FindRoot(problem.ObjectiveFunction, this.parameters);
        }
    }
}
