namespace Home.Library.Optimisation.QuadProg.ProblemGeneration
{
    using MathNet.Numerics.LinearAlgebra;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    public class ProfileMatcherSpreadsheet : IProblemGenerator<IProfileMatchingProblem>
    {
        public IProfileMatchingProblem Generate()
        {
            throw new NotImplementedException();
        }

        public IProfileMatchingProblem Generate(int order)
        {
            var examples = ReadFromFile();
            var example = examples[order];
            
            var a = AssembleAMatrix(example.BasisVectors).ToArray();
            
            var b = AssembleBVector(
                example.BasisVectors,
                example.LowerConstraint,
                example.UpperConstraint).ToArray();

            return new ProfileMatchingProblem.Builder
            {
                Vectors = example.BasisVectors,
                Target = example.TargetVector as double[],
                A = a,
                B = b,
            }.Build();
        }

        private static List<SpreadSheetExample> ReadFromFile()
        {
            var largeExamples = new List<SpreadSheetExample>();

            var spreadsheet = new ExcelSpreadSheet();
            string outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            string filePath = Path.Combine(outPutDirectory, @"QuadraticProgramming2.xlsx");


            var book = spreadsheet.GetExcelWorkBook(filePath);
            {
                var vectors = spreadsheet.ReadRange(book, "QP_Raw50", "BasisVectors");
                var target2d = spreadsheet.ReadRange(book, "QP_Raw50", "TargetProfile");
                var target = FlattenArray(target2d);

                largeExamples.Add(new SpreadSheetExample()
                {
                    Name = "QP_Soln50: 50 Vector problem",
                    BasisVectors = vectors,
                    TargetVector = target,
                    LowerConstraint = 0,
                    UpperConstraint = 2,
                });
            }

            {
                var vectors = spreadsheet.ReadRange(book, "QP_Raw300", "BasisVectors");
                var target2d = spreadsheet.ReadRange(book, "QP_Raw300", "TargetProfile");
                var target = FlattenArray(target2d);

                largeExamples.Add(new SpreadSheetExample()
                {
                    Name = "QP_Soln300: 300 Vector problem",
                    BasisVectors = vectors,
                    TargetVector = target,
                    LowerConstraint = 0,
                    UpperConstraint = 20,
                });
            }

            GC.Collect();
            GC.WaitForPendingFinalizers();

            return largeExamples;
        }

        private static IList<double> FlattenArray(double[,] array)
        {
            int width = array.GetLength(0);
            int height = array.GetLength(1);
            List<double> ret = new List<double>(width * height);
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    ret.Add(array[i, j]);
                }
            }
            return ret.ToArray();
        }

        private Matrix<double> AssembleAMatrix(double[,] basisVectors)
        {
            int cols = basisVectors.GetLength(1);

            // Matrix<double> topA = Matrix<double>.Build.DenseIdentity(cols);
            Matrix<double> topA = Matrix<double>.Build.SparseIdentity(cols);

            return topA.Append(-1 * topA).Transpose();
        }

        private Vector<double> AssembleBVector(
            double[,] basisVectors,
            double lowerConstraint,
            double upperConstraint)
        {
            int cols = basisVectors.GetLength(1);

            return Vector<double>.Build.Dense(cols * 2, (r) =>
                (r < cols) ? lowerConstraint : -1 * upperConstraint);
        }

        public class SpreadSheetExample
        {
            public string Name { get; set; }

            public double UpperConstraint { get; set; }

            public double LowerConstraint { get; set; }

            public double[,] BasisVectors { get; set; }

            public IList<double> TargetVector { get; set; }
        }
    }
}
