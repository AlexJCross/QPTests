namespace Home.Library.Optimisation.Tests
{
    using Home.Library.Optimisation.RootFinding;
    using Home.Library.Optimisation.RootFinding.ProblemGeneration;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.IO;

    [TestClass]
    public class RootFindingTests
    {
        const double Epsilon = 1e-8;

        [TestMethod]
        public void NewtonRaphsonTest()
        {
            // Arrange
            var problem = new SquareRootOfNine();

            var solver = new Solver(
                SquareRootOfNine.GetDefaultParameters(),
                new NewtonRaphsonAlgorithm());

            // Act
            var result = solver.FindRoot(problem);

            // Assert
            Assert.AreEqual(result, 3, Epsilon);
        }

        [TestMethod]
        public void SecantTest()
        {
            // Arrange
            var problem = new SquareRootOfNine();

            var solver = new Solver(
                SquareRootOfNine.GetDefaultParameters(),
                new SecantAlgorithm());

            // Act
            var result = solver.FindRoot(problem);

            // Assert
            Assert.AreEqual(result, 3, Epsilon);
        }

        [TestMethod]
        public void BaileysTest()
        {
            // Arrange
            var problem = new SquareRootOfNine();

            var solver = new Solver(
                SquareRootOfNine.GetDefaultParameters(),
                new BaileysAlgorithm());

            // Act
            var result = solver.FindRoot(problem);

            // Assert
            Assert.AreEqual(result, 3, Epsilon);
        }
    }
}