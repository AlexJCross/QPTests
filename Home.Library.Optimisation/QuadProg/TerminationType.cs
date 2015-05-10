namespace Home.Library.Optimisation.QuadProg
{
    public enum TerminationType
    {
        None = 0,
        InnapropriateSolver,
        Infeasible,
        Unconstrained,
        MaxIterationsExceeded,
        Success,
    }
}
