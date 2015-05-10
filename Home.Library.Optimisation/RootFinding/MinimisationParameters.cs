namespace Home.Library.Optimisation.RootFinding
{
    public class MinimisationParameters
    {
        #region Fields
    
        private readonly double initialGuess;
    
        private readonly double lowerBound;
    
        private readonly double upperBound;
    
        private readonly double tolerance;
    
        private readonly int maxIterations;
    
        #endregion
    
        #region Constructors
    
        private MinimisationParameters(Builder builder)
        {
            this.initialGuess = builder.InitialGuess;
            this.lowerBound = builder.LowerBound;
            this.upperBound = builder.UpperBound;
            this.tolerance = builder.Tolerance;
            this.maxIterations = builder.MaxIterations;
        }
    
        #endregion
    
        #region Public Properties
    
        public double InitialGuess
        {
            get { return this.initialGuess; }
        }
    
        public double LowerBound
        {
            get { return this.lowerBound; }
        }
    
        public double UpperBound
        {
            get { return this.upperBound; }
        }
    
        public double Tolerance
        {
            get { return this.tolerance; }
        }
    
        public int MaxIterations
        {
            get { return this.maxIterations; }
        }
    
        #endregion
    
        #region Builder
    
        public class Builder
        {
            #region Fields
    
            private double initialGuess = 0.1;
    
            private double lowerBound = -0.1;
    
            private double upperBound = 1.0;
    
            private double tolerance = 1e-9;
    
            private int maxIterations = 100;
    
            #endregion
    
            #region Properties
    
            public double InitialGuess
            {
                get { return this.initialGuess; }
                set { this.initialGuess = value; }
            }
    
            public double LowerBound
            {
                get { return this.lowerBound; }
                set { this.lowerBound = value; }
            }
    
            public double UpperBound
            {
                get { return this.upperBound; }
                set { this.upperBound = value; }
            }
    
            public double Tolerance
            {
                get { return this.tolerance; }
                set { this.tolerance = value; }
            }
    
            public int MaxIterations
            {
                get { return this.maxIterations; }
                set { this.maxIterations = value; }
            }
    
            #endregion
    
            #region Build Methods
    
            public Builder WithInitialGuess(double value)
            {
                this.InitialGuess = value;
                return this;
            }
    
            public Builder WithLowerBound(double value)
            {
                this.LowerBound = value;
                return this;
            }
    
            public Builder WithUpperBound(double value)
            {
                this.UpperBound = value;
                return this;
            }
    
            public Builder WithMaxIterations(int value)
            {
                this.MaxIterations = value;
                return this;
            }
    
            public MinimisationParameters Build()
            {
                return new MinimisationParameters(this);
            }
    
            #endregion
        }
    
        #endregion
    }
}
