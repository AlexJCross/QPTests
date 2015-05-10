namespace Home.Library.Optimisation.QuadProg
{
    public class QpProblem : IQpProblem
    {
        #region Fields

        private readonly double[,] q;

        private readonly double[] c;

        private readonly double[,] a;

        private readonly double[] b;

        private readonly double[,] aEq;

        private readonly double[] bEq;

        #endregion

        #region Constructors

        public QpProblem(
            double[,] q,
            double[] c,
            double[,] a,
            double[] b,
            double[,] aEq,
            double[] bEq)
        {
            this.q = q;
            this.c = c;
            this.a = a;
            this.b = b;
            this.aEq = aEq;
            this.bEq = bEq;
        }

        #endregion

        #region Properties

        public double[,] Q
        {
            get { return this.q; }
        }

        public double[] C
        {
            get { return this.c; }
        }

        public double[,] A
        {
            get { return this.a; }
        }

        public double[] B
        {
            get { return this.b; }
        }

        public double[,] Aeq
        {
            get { return this.aEq; }
        }

        public double[] Beq
        {
            get { return this.bEq; }
        }

        #endregion

        #region Builder

        public class Builder
        {
            #region Properties

            public double[,] Q { get; set; }

            public double[] C { get; set; }

            public double[,] A { get; set; }

            public double[] B { get; set; }

            public double[,] Aeq { get; set; }

            public double[] Beq { get; set; }

            #endregion

            #region Methods

            public Builder WithQMatrix(double[,] value)
            {
                this.Q = value;
                return this;
            }

            public Builder WithCVector(double[] value)
            {
                this.C = value;
                return this;
            }

            public Builder WithAMatrix(double[,] value)
            {
                this.A = value;
                return this;
            }

            public Builder WithBVector(double[] value)
            {
                this.B = value;
                return this;
            }

            public Builder WithAEqualityMatrix(double[,] value)
            {
                this.Aeq = value;
                return this;
            }

            public Builder WithBEqualityVector(double[] value)
            {
                this.Beq = value;
                return this;
            }

            public QpProblem Build()
            {
                return new QpProblem(this.Q, this.C, this.A, this.B, this.Aeq, this.Beq);
            }

            #endregion
        }

        #endregion
    }
}