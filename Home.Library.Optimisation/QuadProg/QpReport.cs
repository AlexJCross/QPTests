namespace Home.Library.Optimisation.QuadProg
{
    public class QpReport
    {
        #region Fields
    
        private readonly double[] x;
        private readonly int iterations;
        private readonly TerminationType type;
        private readonly long time;
    
        #endregion
    
        #region Constructors
    
        public QpReport(double[] x, int iterations, TerminationType type, long time)
        {
            this.x = x;
            this.iterations = iterations;
            this.type = type;
            this.time = time;
        }
    
        #endregion
    
        #region Properties
    
        public double[] X
        {
            get { return this.x; }
        }
    
        public int Iterations
        {
            get { return this.iterations; }
        }
    
        public TerminationType Type
        {
            get { return this.type; }
        }

        public long ElapsedTime
        {
            get { return this.time; }
        }
    
        #endregion
    }
}
