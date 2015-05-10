namespace Home.Library.Optimisation.QuadProg
{
    public class ProfileMatchingProblem : IProfileMatchingProblem
    {
        #region Fields
    
        private readonly double[,] vectors;
        private readonly double[] target;
        private readonly double[,] a;
        private readonly double[] b;
        private readonly double[,] aEq;
        private readonly double[] bEq;
    
        #endregion
    
        #region Constructors
    
        public ProfileMatchingProblem(
            double[,] vectors,
            double[] target,
            double[,] a,
            double[] b,
            double[,] aEq,
            double[] bEq)
        {
            this.vectors = vectors;
            this.target = target;
            this.a = a;
            this.b = b;
            this.aEq = aEq;
            this.bEq = bEq;
        }
    
        #endregion
    
        #region Properties
    
        public double[,] Vectors
        {
            get { return this.vectors; }
        }
    
        public double[] Target
        {
            get { return this.target; }
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
    
            public double[,] Vectors { get; set; }
    
            public double[] Target { get; set; }
    
            public double[,] A { get; set; }
    
            public double[] B { get; set; }
    
            public double[,] Aeq { get; set; }
    
            public double[] Beq { get; set; }
    
            #endregion
    
            #region Methods
    
            public Builder WithVectors(double[,] value)
            {
                this.Vectors = value;
                return this;
            }
    
            public Builder WithTarget(double[] value)
            {
                this.Target = value;
                return this;
            }
    
            public Builder WithA(double[,] value)
            {
                this.A = value;
                return this;
            }
    
            public Builder WithB(double[] value)
            {
                this.B = value;
                return this;
            }
    
            public Builder WithAeq(double[,] value)
            {
                this.Aeq = value;
                return this;
            }
    
            public Builder WithBeq(double[] value)
            {
                this.Beq = value;
                return this;
            }
    
            public ProfileMatchingProblem Build()
            {
                return new ProfileMatchingProblem(
                    this.Vectors,
                    this.Target,
                    this.A,
                    this.B,
                    this.Aeq,
                    this.Beq);
            }
    
            #endregion
        }
    
        #endregion
    }
}
