namespace Home.Library.Optimisation.RootFinding
{
    public interface IRootFindingAlgorithm
    {
        double FindRoot(ObjectiveFunction function, MinimisationParameters parameters);
    }
}
