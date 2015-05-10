namespace Home.Library.Optimisation.RootFinding
{
    public interface IMinimisationProblem
    {
        ObjectiveFunction ObjectiveFunction { get; }
    }
}
