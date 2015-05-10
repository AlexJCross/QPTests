namespace Home.Library.Optimisation.QuadProg
{
    public interface IProfileMatchingProblem
    {
        double[,] Vectors { get; }
    
        double[] Target { get; }
    
        double[,] A { get; }
    
        double[] B { get; }
    
        double[,] Aeq { get; }
    
        double[] Beq { get; }
    }
}
