namespace Home.Library.Optimisation.QuadProg
{
    using MathNet.Numerics.LinearAlgebra;
    using MathNet.Numerics.LinearAlgebra.Double;
    using System;

    public class ProfileMatchingQpConverter : IVectorMatchingQpConverter
    {
        #region Fields

        private readonly ProfileMatchingMetric metric;

        #endregion

        #region Constructors

        public ProfileMatchingQpConverter(ProfileMatchingMetric metric)
        {
            this.metric = metric;
        }

        #endregion

        #region Properties

        public ProfileMatchingMetric Metric
        {
            get { return this.metric; }
        }

        #endregion

        #region Public Methods

        public IQpProblem Convert(IProfileMatchingProblem problem)
        {
            InternalConverter converter = SelectInternalBuilder(problem);

            return new QpProblem.Builder()
                .WithQMatrix(converter.AssembleQMatrix())
                .WithCVector(converter.AssembleCVector())
                .WithAMatrix(problem.A)
                .WithBVector(problem.B)
                .WithAEqualityMatrix(problem.Aeq)
                .WithBEqualityVector(problem.Beq)
                .Build();
        }

        #endregion

        #region Methods

        private InternalConverter SelectInternalBuilder(IProfileMatchingProblem problem)
        {
            switch (this.metric)
            {
                case ProfileMatchingMetric.SumSquares:
                    return new SumSquaresConverter(problem);
                case ProfileMatchingMetric.CumulativeSumsquares:
                    return new CumulativeSumSquaresConverter(problem);
                default:
                    throw new NotSupportedException("Fitting metric not supported.");
            }
        }

        #endregion

        #region Private Classes

        private abstract class InternalConverter
        {
            protected Matrix<double> basisVectors;
            protected Vector<double> targetVector;

            protected InternalConverter(IProfileMatchingProblem problem)
            {
                this.basisVectors = Matrix<double>.Build.DenseOfArray(problem.Vectors);
                this.targetVector = Vector<double>.Build.DenseOfArray(problem.Target);
            }

            public abstract double[,] AssembleQMatrix();

            public abstract double[] AssembleCVector();
        }

        private class SumSquaresConverter : InternalConverter
        {
            public SumSquaresConverter(IProfileMatchingProblem problem)
                : base(problem)
            {
            }

            public override double[,] AssembleQMatrix()
            {
                return (2 * this.basisVectors.TransposeThisAndMultiply(this.basisVectors)).ToArray();
            }

            public override double[] AssembleCVector()
            {
                return (-2 * this.basisVectors.TransposeThisAndMultiply(this.targetVector)).ToArray();
            }
        }

        private class CumulativeSumSquaresConverter : InternalConverter
        {
            public CumulativeSumSquaresConverter(IProfileMatchingProblem problem)
                : base(problem)
            {
            }

            public override double[,] AssembleQMatrix()
            {
                var rows = this.basisVectors.RowCount;
                var cols = this.basisVectors.ColumnCount;
                var builder = Matrix<double>.Build;

                Matrix<double> lowerOnes = DenseMatrix.Create(
                    rows,
                    rows,
                    (r, c) => (r < c) ? 0 : 1);

                var qMatrix = 2 * this.basisVectors.TransposeThisAndMultiply(
                    lowerOnes.TransposeThisAndMultiply(lowerOnes * this.basisVectors));

                // Slightly perturb to move from SPD to PD matrix.
                var perturb = (1e-12 * qMatrix.InfinityNorm()) * builder.DenseIdentity(cols);
                return (qMatrix + perturb).ToArray();
            }

            public override double[] AssembleCVector()
            {
                var rows = this.basisVectors.RowCount;

                Matrix<double> Lower = DenseMatrix.Create(
                    rows,
                    rows,
                    (r, c) => (r < c) ? 0 : 1);

                return (-2 * Lower
                    .TransposeThisAndMultiply(Lower * this.basisVectors)
                    .TransposeThisAndMultiply(this.targetVector)).ToArray();
            }
        }

        #endregion
    }
}
