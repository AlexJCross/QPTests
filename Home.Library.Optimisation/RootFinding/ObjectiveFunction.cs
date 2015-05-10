namespace Home.Library.Optimisation.RootFinding
{
    using System;

    public class ObjectiveFunction
    {
        #region Fields
    
        private readonly Func<double, double> f;
    
        private readonly Func<double, double> fDash;
    
        private readonly Func<double, double> fDoubleDash;
    
        #endregion
    
        #region Constructors
    
        private ObjectiveFunction(Builder builder)
        {
            this.f = builder.F;
            this.fDash = builder.FDash;
            this.fDoubleDash = builder.FDoubleDash;
        }
    
        #endregion
    
        #region Properties
    
        public Func<double, double> F
        {
            get { return f; }
        }
    
        public Func<double, double> FDash
        {
            get { return fDash; }
        }
    
        public Func<double, double> FDoubleDash
        {
            get { return fDoubleDash; }
        }
    
        #endregion
    
        #region Builder
    
        public class Builder
        {
            public Func<double, double> F { get; set; }
    
            public Func<double, double> FDash { get; set; }
    
            public Func<double, double> FDoubleDash { get; set; }
    
            public Builder WithF(Func<double, double> value)
            {
                this.F = value;
                return this;
            }
    
            public Builder WithFDash(Func<double, double> value)
            {
                this.FDash = value;
                return this;
            }
    
            public Builder WithFDoubleDash(Func<double, double> value)
            {
                this.FDoubleDash = value;
                return this;
            }
    
            public ObjectiveFunction Build()
            {
                return new ObjectiveFunction(this);
            }
        }
    
        #endregion
    }
}
