using MathNet.Numerics.LinearAlgebra;
using System;

namespace Home.Library.Optimisation.QuadProg.ProblemGeneration
{
    public class QpGeneratorSimple : IProblemGenerator<IQpProblem>
    {
        public void Setup()
        {
    
        }
    
        public IQpProblem Generate()
        {
            return this.Generate(200);
        }
    
        public IQpProblem Generate(int order)
        {
            // Arrange
            Matrix<double> Q = Matrix<double>.Build.DenseIdentity(order);
    
            Vector<double> c = Vector<double>.Build.Random(order);
            Matrix<double> topA = Matrix<double>.Build.DenseIdentity(order);
            var A = (topA.Append(-topA)).Transpose();
    
            Vector<double> b = -20 * Vector<double>.Build.Dense(2 * order, 1);
    
            Vector<double> x = Vector<double>.Build.Dense(order, 1);
    
            return new QpProblem.Builder()
                .WithQMatrix(Q.ToArray())
                .WithCVector(c.ToArray())
                .WithAMatrix(A.ToArray())
                .WithBVector(b.ToArray())
                .Build();
        }
    }
}
