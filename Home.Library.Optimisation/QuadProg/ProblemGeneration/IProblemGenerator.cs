namespace Home.Library.Optimisation.QuadProg.ProblemGeneration
{
    public interface IProblemGenerator<out TProblem>
    {
        TProblem Generate();

        TProblem Generate(int order);
    }

}