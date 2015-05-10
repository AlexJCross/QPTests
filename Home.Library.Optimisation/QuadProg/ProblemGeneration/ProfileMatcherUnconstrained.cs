namespace Home.Library.Optimisation.QuadProg.ProblemGeneration
{
    using System;

    public class ProfileMatcherUnconstrained : IProblemGenerator<IProfileMatchingProblem>
    {
        public IProfileMatchingProblem Generate()
        {
            return new ProfileMatchingProblem.Builder
            {
                Vectors = new double[,]   
                { 
                    {  1,  2,  1 },
                    {  0,  1,  0 },
                    {  0,  4,  2 }
                },
    
                Target = new double[] { 7, 1, 10 },
            }.Build();
        }
    
        public IProfileMatchingProblem Generate(int order)
        {
            throw new NotImplementedException();
        }
    }
}
