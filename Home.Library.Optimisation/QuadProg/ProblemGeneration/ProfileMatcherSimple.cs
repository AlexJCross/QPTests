namespace Home.Library.Optimisation.QuadProg.ProblemGeneration
{
    using System;

    public class ProfileMatcherSimple : IProblemGenerator<IProfileMatchingProblem>
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

                A = new double[,]   
                { 
                    {  1,  0,  0 },
                    {  0,  1,  0 },
                    {  0,  0,  1 },
                    { -1,  0,  0 },
                    {  0, -1,  0 },
                    {  0,  0, -1 }
                },

                B = new double[] { 0, 0, 0, -2, -2, -2 },

                Aeq = null,
                Beq = null
            }.Build();
        }

        public IProfileMatchingProblem Generate(int order)
        {
            throw new NotImplementedException();
        }
    }
}
