namespace Home.Library.Optimisation.QuadProg
{
    public interface IQpProblem
    {
        double[,] Q { get; }
    
        double[] C { get; }
    
        double[,] A { get; }
    
        double[] B { get; }
    
        double[,] Aeq { get; }
    
        double[] Beq { get; }
    }
}
