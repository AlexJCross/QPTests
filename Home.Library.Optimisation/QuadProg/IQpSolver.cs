namespace Home.Library.Optimisation.QuadProg
{
    public interface IQpSolver
    {
        QpReport Solve(IQpProblem problem);
    }
}
