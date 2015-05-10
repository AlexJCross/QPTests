using MathNet.Numerics.LinearAlgebra;
using System.Diagnostics;
namespace Home.Library.Optimisation.QuadProg
{
    public class QpSolver : IQpSolver
    {
        #region Public Methods
    
        public QpReport Solve(IQpProblem problem)
        {
            double[] x;
            alglib.minqpstate state;
            alglib.minqpreport report;

            var watch = new Stopwatch();
            watch.Start();
    
            // Convert constraints to ALGLIB format.
            ConstraintsAdapter constraints = new ConstraintsAdapter(problem);
    
            // Create the solver, set QP terms and linear constraints.
            alglib.minqpcreate(problem.Q.GetLength(0), out state);
            alglib.minqpsetquadraticterm(state, problem.Q);
            alglib.minqpsetlinearterm(state, problem.C);
            alglib.minqpsetlc(state, constraints.ConstraintMatrix, constraints.ConstraintTypes);
    
            // Set the scale of the parameters and the algorithm choice.
            alglib.minqpsetscale(state, constraints.ScaleFactors);
            alglib.minqpsetalgobleic(state, 0.0, 0.0, 0.0, 0);
    
            // Run the optimisation and report.
            alglib.minqpoptimize(state);
            alglib.minqpresults(state, out x, out report);
            watch.Stop();

            return new QpReport(
                x,
                report.outeriterationscount,
                GetSolveStatus(report.terminationtype),
                watch.ElapsedMilliseconds);
        }
    
        #endregion
    
        #region Methods
    
        private static TerminationType GetSolveStatus(int flag)
        {
            switch (flag)
            {
                case -5:
                    return TerminationType.InnapropriateSolver;
                case -4:
                    return TerminationType.Unconstrained;
                case -3:
                case 7:
                    return TerminationType.Infeasible;
                case 1:
                case 2:
                case 3:
                case 4:
                    return TerminationType.Success;
                case 5:
                    return TerminationType.MaxIterationsExceeded;
                default:
                    return TerminationType.None;
            }
        }
    
        #endregion
    
        #region Private Classes
    
        private class ConstraintsAdapter
        {
            #region Fields
    
            private readonly IQpProblem problem;
    
            private readonly double[,] constraintMatrix;
    
            private readonly double[] scaleFactors;
    
            private readonly int[] constraintVector;

            private readonly int numConstraints;

            private readonly int numVars;
    
            #endregion
    
            #region Constructors
    
            public ConstraintsAdapter(IQpProblem problem)
            {
                this.problem = problem;

                this.numVars = this.problem.Q.GetLength(0);
                this.numConstraints = ComputeNumberOfConstraints();
                this.constraintMatrix = BuildConstraintMatrix();
                this.constraintVector = BuildConstraintVector();
                this.scaleFactors = BuildScaleFactors();
            }

            #endregion
    
            #region Properties
    
            public int[] ConstraintTypes
            {
                get { return this.constraintVector; }
            }
    
            public double[,] ConstraintMatrix
            {
                get { return this.constraintMatrix; }
            }
    
            public double[] ScaleFactors
            {
                get { return this.scaleFactors; }
            }
    
            public int NumberOfConstraints
            {
                get { return this.numConstraints; }
            }
    
            #endregion
    
            #region Methods
    
            private int[] BuildConstraintVector()
            {
                var constraintFlags = new int[this.numConstraints];

                int numOfInequality = 0;

                if (this.problem.B != null)
                {
                    numOfInequality = this.problem.B.Length;

                    for (int i = 0; i < numOfInequality; i++)
                    {
                        constraintFlags[i] = 1;
                    }
                }

                if (this.problem.Beq != null)
                {
                    int numOfEquality = this.problem.Beq.Length;

                    for (int i = 0; i < numOfEquality; i++)
                    {
                        constraintFlags[numOfInequality + i] = 0;
                    }
                }

                return constraintFlags;
            }
    
            private double[,] BuildConstraintMatrix()
            {
                var constraintMatrix = new double[this.numConstraints, this.numVars + 1];

                int numOfInequality = 0;

                if (this.problem.B != null)
                {
                    numOfInequality = this.problem.B.Length;

                    for (int i = 0; i < numOfInequality; i++)
                    {
                        for (int j = 0; j < this.numVars; j++)
                        {
                            constraintMatrix[i, j] = this.problem.A[i, j];
                        }

                        constraintMatrix[i, this.numVars] = this.problem.B[i];
                    }
                }

                if (this.problem.Beq != null)
                {
                    int numOfEquality = this.problem.Beq.Length;

                    for (int i = 0; i < numOfEquality; i++)
                    {
                        for (int j = 0; j < this.numVars; j++)
                        {
                            constraintMatrix[numOfInequality + i, j] = this.problem.Aeq[i, j];
                        }

                        constraintMatrix[numOfInequality + i, this.numVars] = this.problem.Beq[i];
                    }
                }

                return constraintMatrix;
            }

            private double[] BuildScaleFactors()
            {
                var scalars = new double[this.numVars];

                for (int i = 0; i < scalars.Length; i++)
                {
                    scalars[i] = 1;
                }

                return scalars;
            }

            private int ComputeNumberOfConstraints()
            {
                var count = 0;

                count += (this.problem.B == null)
                    ? 0 : this.problem.B.Length;

                count += (this.problem.Beq == null)
                    ? 0 : this.problem.Beq.Length;
            
                return count;
            }

            #endregion
        }
    
        #endregion
    }
}
