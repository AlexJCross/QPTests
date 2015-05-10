namespace Home.Library.Optimisation.Tests
{
    using Home.Library.Optimisation.QuadProg;
    using Home.Library.Optimisation.QuadProg.ProblemGeneration;

    using MathNet.Numerics.LinearAlgebra;
    
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    
    using System.IO;

    [TestClass]
    public class QuadProgTests
    {
        const int n = 200;
        const double Epsilon = 1e-8;

        [TestMethod]
        public void ProfileMatchingSimple()
        {
            // Arrange
            var problem = new ProfileMatcherSimple().Generate();

            IQpProblem qpProblem = new ProfileMatchingQpConverter(
                ProfileMatchingMetric.CumulativeSumsquares).Convert(problem);

            // Act
            IQpSolver solver = new QpSolver();
            QpReport report = solver.Solve(qpProblem);

            // Assert
            Assert.AreEqual(report.X[0], 1.95238095238094, Epsilon);
            Assert.AreEqual(report.X[1], 1.42857142857143, Epsilon);
            Assert.AreEqual(report.X[2], 2, Epsilon);
        }

        [TestMethod]
        public void ProfileMatchingEquality()
        {
            // Arrange
            var problem = new ProfileMatcherEquality().Generate();

            IQpProblem qpProblem = new ProfileMatchingQpConverter(
                ProfileMatchingMetric.CumulativeSumsquares).Convert(problem);
            
            // Act
            IQpSolver solver = new QpSolver();
            QpReport report = solver.Solve(qpProblem);

            // Assert
            Assert.AreEqual(report.X[0], 1.92682926808697, Epsilon);
            Assert.AreEqual(report.X[1], 1.4390243902732891, Epsilon);
            Assert.AreEqual(report.X[2], 2, Epsilon);
        }

        [TestMethod]
        public void ProfileMatchingUnconstrained()
        {
            // Arrange
            var problem = new ProfileMatcherUnconstrained().Generate();

            IQpProblem qpProblem = new ProfileMatchingQpConverter(
                ProfileMatchingMetric.CumulativeSumsquares).Convert(problem);

            // Act
            IQpSolver solver = new QpSolver();
            QpReport report = solver.Solve(qpProblem);

            // Assert
            Assert.AreEqual(report.X[0], 2, Epsilon);
            Assert.AreEqual(report.X[1], 1, Epsilon);
            Assert.AreEqual(report.X[2], 3, Epsilon);
        }

        [TestMethod]
        [Ignore]
        public void ProfileMatchingSpreadsheet()
        {
            // Arrange
            var problem = new ProfileMatcherSpreadsheet().Generate(1);

            IQpProblem qpProblem = new ProfileMatchingQpConverter(
                ProfileMatchingMetric.CumulativeSumsquares).Convert(problem);

            // Act
            IQpSolver solver = new QpSolver();
            QpReport report = solver.Solve(qpProblem);

            SaveArrayAsCSV(report.X, @"C:\Users\Alex\Documents\TEMP\blah.csv");
        }

        [TestMethod]
        public void ProfileMatchingInfeasible()
        {
            // Arrange
            var problem = new ProfileMatcherInfeasible().Generate();

            IQpProblem qpProblem = new ProfileMatchingQpConverter(
                ProfileMatchingMetric.CumulativeSumsquares).Convert(problem);

            // Act
            IQpSolver solver = new QpSolver();
            QpReport report = solver.Solve(qpProblem);

            // Assert
            Assert.AreEqual(TerminationType.Infeasible, report.Type);
        }

        [TestMethod]
        public void QpInfeasible()
        {
            // Arrange
            var qpProblem = new QpGeneratorInfeasible().Generate(n);

            // Act
            IQpSolver solver = new QpSolver();
            QpReport report = solver.Solve(qpProblem);

            // Assert
            Assert.AreEqual(TerminationType.Infeasible , report.Type);
        }

        [TestMethod]
        public void QpSimple()
        {
            // Arrange
            var qpProblem = new QpGeneratorSimple().Generate(n);

            // Act
            IQpSolver solver = new QpSolver();
            QpReport report = solver.Solve(qpProblem);

            // Assert
            var builder = Vector<double>.Build;
            var check = builder.DenseOfArray(report.X) + builder.DenseOfArray(qpProblem.C);
            Assert.IsTrue(check.Norm(2) < (1e-8 * n));
        }

        public static void SaveArrayAsCSV<T>(T[] arrayToSave, string fileName)
        {
            using (StreamWriter file = new StreamWriter(fileName))
            {
                foreach (T item in arrayToSave)
                {
                    file.Write(item + ",");
                }
            }
        }
    }
}