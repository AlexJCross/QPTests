namespace Home.Library.Optimisation.QuadProg
{
    public interface IVectorMatchingQpConverter
    {
        ProfileMatchingMetric Metric { get; }
    
        IQpProblem Convert(IProfileMatchingProblem problem);
    }
}
