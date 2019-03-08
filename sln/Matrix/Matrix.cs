using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Matrices
{
    /// <summary>
    /// Represents a matrix
    /// </summary>
    public class Matrix
    {
        /// <summary>
        /// Represents the height of the matrix
        /// </summary>
        public readonly int Height;

        /// <summary>
        /// The matrix values in memory as an array.
        /// A single dimensional array is used instead of a two dimensional array to speed up the matrix.
        /// </summary>
        internal double[] InnerMatrix;

        /// <summary>
        /// Represents the width of the matrix
        /// </summary>
        public readonly int Width;

        /// <summary>
        /// Creates a matrix with the specified height and width.
        /// </summary>
        /// <param name="height">The height.</param>
        /// <param name="width">The width.</param>
        public Matrix(int height, int width)
        {
            InnerMatrix = new double[width * height];
            Height = height;
            Width = width;
        }

        /// <summary>
        /// Adds the passed in matrix to the current matrix.
        /// </summary>
        /// <param name="a">The matrix to add.</param>
        public void Add(Matrix a)
        {
            if (a == null)
            {
                throw new NullMatrixException("Cannot add a null matrix.");
            }
            Add(this, a, this);
        }

        /// <summary>
        /// Creates a copy of the current matrix that has allocated its own memory
        /// </summary>
        /// <returns>The copied matrix.</returns>
        public Matrix Copy()
        {
            Matrix NewMatrix = new Matrix(Height, Width);

            int counter = 0;
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    NewMatrix.InnerMatrix[counter] = InnerMatrix[counter];
                    counter++;
                }
            }

            return NewMatrix;
        }

        /// <summary>
        /// Calculates the dot product of this matrix with the passed in vector.
        /// </summary>
        /// <param name="b">The vector.</param>
        /// <returns>The result.</returns>
        public Vector DotProduct(Vector b)
        {
            return DotProduct(this, b);
        }

        /// <summary>
        /// Aquires a column of matrix as a vector
        /// </summary>
        /// <param name="columnIndex">The index of the column.</param>
        /// <returns>The column.</returns>
        public Vector GetColumn(int columnIndex)
        {
            if (columnIndex >= Width || columnIndex < 0)
            {
                throw new IndexOutOfRangeException("Matrix call is out of bounds!");
            }

            Vector column = new Vector(Height, VectorType.Column);
            for (int i = 0; i < Height; i++)
            {
                column.InnerVector[i] = InnerMatrix[i * Width + columnIndex];
            }
            return column;
        }

        /// <summary>
        /// Aquires a row of a matrix as a vector
        /// </summary>
        /// <param name="rowIndex">The index of the row.</param>
        /// <returns>The row.</returns>
        public Vector GetRow(int rowIndex)
        {
            if (rowIndex >= Height || rowIndex < 0)
            {
                throw new IndexOutOfRangeException("Matrix call is out of bounds!");
            }

            int counter = rowIndex * Width;

            Vector row = new Vector(Width, VectorType.Row);
            for (int i = 0; i < Width; i++)
            {
                row.InnerVector[i] = InnerMatrix[counter];
                counter++;
            }
            return row;
        }

        /// <summary>
        /// Gets the value of the cell.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="column">The column.</param>
        /// <returns>The value.</returns>
        public double GetValue(int row, int column)
        {
            if (Height > row && Width > column && row >= 0 && column >= 0)
            {
                return InnerMatrix[row * Width + column];
            }

            throw new IndexOutOfRangeException("Matrix call is out of bounds!");
        }

        /// <summary>
        /// Creates a copy of the matrix and performs the passed in operation on each cell.
        /// </summary>
        /// <param name="operation">The operation to perform.</param>
        /// <returns>The result matrix.</returns>
        public Matrix PerformOperation(Func<double, double> operation)
        {
            Matrix result = new Matrix(Height, Width);

            PerformOperation(this, operation, result);

            return result;
        }

        /// <summary>
        /// Sets the row at the passed in index to the passed in rows values
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="RowIndex">The index of the row.</param>
        public void SetColumn(double[] columnValues, int columnNumber)
        {
            if (columnValues == null)
            {
                throw new NullVectorException("Cannot add a null row to a matrix");
            }

            if (columnValues.Length != Height)
            {
                throw new UnevenMatricesException("Cannot add a row of incorrect size to the matrix.");
            }

            for (int i = 0; i < columnValues.Length; i++)
            {
                InnerMatrix[i * Width + columnNumber] = columnValues[i];
            }
        }

        /// <summary>
        /// Sets the row at the passed in index to the passed
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="rowIndex"></param>
        public void SetColumn(Vector columnValues, int columnNumber)
        {
            if (columnValues == null)
            {
                throw new NullVectorException("Cannot add a null vector to a matrix");
            }

            if (columnValues.VectorType != VectorType.Column)
            {
                throw new VectorIncorrectAlignmentException("Cannot set a row equal to a column.");
            }

            SetColumn(columnValues.InnerVector, columnNumber);
        }

        /// <summary>
        /// Sets the values of the matrix to the values of the passed in array.
        /// </summary>
        /// <param name="matrix">The two dimensional array.</param>
        public void SetMatrixToTwoDimensionalArray(double[,] matrix)
        {
            if (matrix == null)
            {
                throw new NullMatrixException("Cannot set matrix equal to a null matrix.");
            }

            if (matrix.GetLength(0) != Height || matrix.GetLength(1) != Width)
            {
                throw new UnevenMatricesException("Cannot set matrix equal to matrix of different size.");
            }

            int counter = 0;
            if (matrix.GetLength(0) == Height && matrix.GetLength(1) == Width)
            {
                for (int row = 0; row < Height; row++)
                {
                    for (int column = 0; column < Width; column++)
                    {
                        InnerMatrix[counter] = matrix[row, column];
                        counter++;
                    }
                }
            }
        }

        /// <summary>
        /// Sets the row at the passed in index to the passed in rows values
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="RowIndex">The index of the row.</param>
        public void SetRow(double[] row, int rowIndex)
        {
            if (row == null)
            {
                throw new NullVectorException("Cannot add a null row to a matrix");
            }

            if (row.Length != Width)
            {
                throw new UnevenMatricesException("Cannot add a row of incorrect size to the matrix.");
            }

            int counter = rowIndex * Width;
            if (row.Length == Width)
            {
                for (int i = 0; i < row.Length; i++)
                {
                    InnerMatrix[counter] = row[i];
                    counter++;
                }
            }
        }

        /// <summary>
        /// Sets the row at the passed in index to the passed
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="rowIndex"></param>
        public void SetRow(Vector row, int rowIndex)
        {
            if (row == null)
            {
                throw new NullVectorException("Cannot add a null vector to a matrix");
            }

            if (row.VectorType != VectorType.Row)
            {
                throw new VectorIncorrectAlignmentException("Cannot set a row equal to a column.");
            }

            SetRow(row.InnerVector, rowIndex);
        }

        /// <summary>
        /// Sets the value of the cell to the passed in value.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="column">The column.</param>
        /// <param name="Value">The value.</param>
        public void SetValue(int row, int column, double Value)
        {
            if (Height <= row || Width <= column || row < 0 || column < 0)
            {
                throw new IndexOutOfRangeException("The passed in indexes must be within the Matrix.");
            }

            InnerMatrix[row * Width + column] = Value;
        }

        /// <summary>
        /// Subtracts the passed in matrix from the current matrix.
        /// </summary>
        /// <param name="a">The matrix to subtract.</param>
        /// <returns>The result matrix.</returns>
        public Matrix Subtract(Matrix a)
        {
            if (a == null)
            {
                throw new NullMatrixException("Cannot subtract matrices with a null matrix.");
            }

            Matrix result = new Matrix(Height, Width);

            Subtract(this, a, result);

            return result;
        }

        /// <summary>
        /// Returns the transpose of the matrix
        /// </summary>
        /// <returns>The transposed matrix.</returns>
        public Matrix Transpose()
        {
            Matrix transpose = new Matrix(Width, Height);

            int counter = 0;
            for (int row = 0; row < Height; row++)
            {
                for (int column = 0; column < Width; column++)
                {
                    transpose.InnerMatrix[column * transpose.Width + row] = InnerMatrix[counter];
                    counter++;
                }
            }

            return transpose;
        }

        /// <summary>
        /// Initializes the matrix using Xavier Initialization
        /// </summary>
        /// <param name="numberOfInputNeurons">The number of input neurons.</param>
        public void XavierInitialization(int numberOfInputNeurons)
        {
            if (numberOfInputNeurons < 1)
            {
                throw new Exception("Cannot have a number of neurons less than or equal to 0.");
            }

            double Variance = 2 / ((double)numberOfInputNeurons - 1);
            double StandardDev = Math.Sqrt(Variance);
            double Value;

            double v1, v2;
            double w;
            Random r = new Random(Guid.NewGuid().GetHashCode());

            int counter = 0;
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    v1 = r.NextDouble() - .5;
                    v2 = r.NextDouble() - .5;

                    w = v1 * v1 + v2 * v2;

                    Value = Math.Sqrt(-2 * Math.Log(w) / w) * StandardDev;

                    if (r.NextDouble() > .5)
                    {
                        Value *= v1;
                    }
                    else
                    {
                        Value *= v2;
                    }

                    InnerMatrix[counter] = Value;
                    counter++;
                }
            }
        }

        /// <summary>
        /// Adds the two passed in matrices, returns the result
        /// </summary>
        /// <param name="a">The first matrix.</param>
        /// <param name="b">The second matrix.</param>
        /// <returns>The result matrix.</returns>
        public static Matrix Add(Matrix a, Matrix b)
        {
            if (a == null || b == null)
            {
                throw new NullMatrixException("Cannot add two matrices when either matrix is null.");
            }

            Matrix output = new Matrix(a.Height, b.Width);

            Add(a, b, output);

            return output;
        }

        /// <summary>
        /// Calculates the dot product of a matrix and vector.
        /// </summary>
        /// <param name="a">The matrix.</param>
        /// <param name="b">The vector.</param>
        /// <returns>The resulting vector.</returns>
        public static Vector DotProduct(Matrix a, Vector b)
        {
            if (a == null)
            {
                throw new NullMatrixException("Cannot perform a dot product operation with a null matrix.");
            }

            if (b == null)
            {
                throw new NullVectorException("Cannot perform a dot product operation with a null vector.");
            }

            var result = new Vector(a.Height, VectorType.Column);

            DotProduct(a, b, result);

            return result;
        }

        /// <summary>
        /// Calculates the dot product of a matrix and vector
        /// </summary>
        /// <param name="a">The vector.</param>
        /// <param name="b">The matrix.</param>
        /// <returns>The result vector.</returns>
        public static Vector DotProduct(Vector a, Matrix b)
        {
            if (a == null)
            {
                throw new NullVectorException("Cannot perform a dot product operation with a null matrix.");
            }

            if (b == null)
            {
                throw new NullMatrixException("Cannot perform a dot product operation with a null vector.");
            }

            var result = new Vector(a.Length, VectorType.Row);

            DotProduct(a, b, result);

            return result;
        }

        /// <summary>
        /// Multiplies the two supplied matrices together. Uses a customizable number of threads to compute the result.
        /// </summary>
        /// <param name="a">The first matrix.</param>
        /// <param name="b">The second matrix.</param>
        /// <param name="numberOfThreads">The number of threads to use. The main thread counts as one of the used threads.</param>
        /// <returns></returns>
        public static Matrix Multiplication(Matrix a, Matrix b, int NumberOfThreads)
        {
            if (a.Width != b.Height)
            {
                throw new UnevenMatricesException("To perform dot product the first matrix must have a width equal to the seconds height!");
            }
            if (NumberOfThreads < 1)
            {
                throw new Exception("Number of threads must be greater than 0");
            }

            Matrix result = new Matrix(a.Height, b.Width);

            int divided = a.Height / NumberOfThreads;

            // Use the tranpose so we can loop through row by row. Reduces the number of chunks loaded
            b = b.Transpose();

            int counter = 0;

            List<Thread> threads = new List<Thread>();

            for (int i = 0; i < NumberOfThreads; i++)
            {
                if (i == NumberOfThreads - 1)
                {
                    threads.Add(new Thread(() =>
                    {
                        MulitplicationInner(a, b, ref result, counter, a.Height);
                    }));
                }
                else
                {
                    threads.Add(new Thread(() =>
                    {
                        MulitplicationInner(a, b, ref result, counter, counter + divided);
                    }));
                    counter += divided;
                }
            }

            foreach (var thread in threads)
            {
                thread.Start();
            }
            foreach (var thread in threads)
            {
                thread.Join();
            }

            return result;
        }

        /// <summary>
        /// Creates a copy of the matrix and performs the passed in operation on each cell.
        /// </summary>
        /// <param name="matrix">The data matrix.</param>
        /// <param name="operation">The operation to perform.</param>
        /// <returns>The result matrix.</returns>
        public static Matrix PerformOperation(Matrix matrix, Func<double, double> operation)
        {
            if (matrix == null)
            {
                throw new NullMatrixException("Cannot perform an operation on a null matrix.");
            }

            Matrix result = new Matrix(matrix.Height, matrix.Width);

            PerformOperation(matrix, operation, result);

            return result;
        }
        
        /// <summary>
        /// Subtracts matrix j from matrix i.
        /// </summary>
        /// <param name="a">The matrix to subtract from.</param>
        /// <param name="b">The matrix to subtract.</param>
        /// <returns>The result matrix.</returns>
        public static Matrix Subtract(Matrix a, Matrix b)
        {
            if (a == null || b == null)
            {
                throw new NullMatrixException("Cannot subtract matrices with a null matrix.");
            }

            Matrix result = new Matrix(a.Height, a.Width);

            Subtract(a, b, result);

            return result;
        }

        /// <summary>
        /// Adds the two matrices again.
        /// </summary>
        /// <param name="a">The matrix a.</param>
        /// <param name="b">The matrix b.</param>
        /// <returns>The return matrix.</returns>
        public static Matrix operator+ (Matrix a, Matrix b)
        {
            return Add(a, b);
        }

        /// <summary>
        /// Subtracts matrix b from matrix a.
        /// </summary>
        /// <param name="a">The matrix a.</param>
        /// <param name="b">The matrix b.</param>
        /// <returns>The return matrix.</returns>
        public static Matrix operator- (Matrix a, Matrix b)
        {
            return Subtract(a, b);
        }

        /// <summary>
        /// Performs the matrix multiplication operation on the two matrices using a single thread.
        /// </summary>
        /// <param name="a">Matrix a.</param>
        /// <param name="b">Matrix b.</param>
        /// <returns>The matrix.</returns>
        public static Matrix operator* (Matrix a, Matrix b)
        {
            return Multiplication(a, b, 1);
        }

        /// <summary>
        /// Performs the division operator the two matrices.
        /// </summary>
        /// <param name="a">Matrix a.</param>
        /// <param name="b">Matrix b.</param>
        /// <returns>Returns the result matrix.</returns>
        public static Matrix operator/ (Matrix a, Matrix b)
        {
            throw new NotImplementedException("Not yet implemented.");
        }
        
        /// <summary>
        /// Adds two matrices together.
        /// </summary>
        /// <param name="a">The first matrix.</param>
        /// <param name="b">The second matrix.</param>
        /// <param name="output">The output matrix.</param>
        private static void Add(Matrix a, Matrix b, Matrix output)
        {
            if (!EquallySized(a, b) || !EquallySized(a, output))
            {
                throw new UnevenMatricesException("Cannot add unevent matrices!");
            }

            int counter = 0;
            for (int p = 0; p < a.Height; p++)
            {
                for (int q = 0; q < a.Width; q++)
                {
                    output.InnerMatrix[counter] = a.InnerMatrix[counter] + b.InnerMatrix[counter];
                    counter++;
                }
            }
        }

        /// <summary>
        /// Performs the dot product for the matrix / vector.
        /// </summary>
        /// <param name="a">The matrix a.</param>
        /// <param name="b">The vector.</param>
        /// <param name="result">The result vector.</param>
        private static void DotProduct(Matrix a, Vector b, Vector result)
        {
            if (b.VectorType != VectorType.Column)
            {
                throw new VectorIncorrectAlignmentException("Cannot perform dot product with a vector on the right side of the equation if it's not a column.");
            }

            if (b.Length != a.Width)
            {
                throw new UnevenMatricesException("Vector must be a column with length equal to the matrices width!");
            }

            double c;
            int counter = 0;
            int i, j;

            for (i = 0; i < a.Height; i++)
            {
                c = 0;
                for (j = 0; j < a.Width; j++)
                {
                    c += a.InnerMatrix[counter] * b.InnerVector[j];
                    counter++;
                }
                result.InnerVector[i] = c;
            }
        }

        /// <summary>
        /// Performs thedot product for the vector / matrix.
        /// </summary>
        /// <param name="a">The vector a.</param>
        /// <param name="b">The matrix b.</param>
        /// <param name="result">The result matrix.</param>
        private static void DotProduct(Vector a, Matrix b, Vector result)
        {
            if (a.VectorType != VectorType.Row)
            {
                throw new VectorIncorrectAlignmentException("Cannot perform dot product with a vector on the left side of the equation if it's not a row.");
            }
            if (a.Length != b.Height)
            {
                throw new UnevenMatricesException("Vector must be a column with length equal to the matrices width!");
            }

            double c;
            int counter = 0;
            int i, j;

            b = b.Transpose();

            for (i = 0; i < b.Height; i++)
            {
                c = 0;
                for (j = 0; j < b.Width; j++)
                {
                    c += b.InnerMatrix[counter] * a.InnerVector[j];
                    counter++;
                }
                result.InnerVector[i] = c;
            }
        }

        /// <summary>
        /// Checks if the two matrices are equally sized.
        /// </summary>
        /// <param name="a">The first matrix.</param>
        /// <param name="b">The second matrix.</param>
        /// <returns>True if the two matrices have the same dimensions; Otherwise, false.</returns>
        private static bool EquallySized(Matrix a, Matrix b)
        {
            if (a.Height == b.Height && a.Width == b.Width)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Performs matrix multiplication on values between the specified start and end value.
        /// </summary>
        /// <param name="a">The first matrix.</param>
        /// <param name="b">The second matrix.</param>
        /// <param name="result">The matrix to store the results in.</param>
        /// <param name="start">The index to start at.</param>
        /// <param name="end">The index to end at.</param>
        /// <returns></returns>
        private static Matrix MulitplicationInner(Matrix a, Matrix b, ref Matrix result, int start, int end)
        {
            if (a.Width != b.Height)
            {
                throw new Exception("To perform dot product the first matrix must have a width equal to the seconds height!");
            }

            int i, j, k;
            double c;
            int PositionA = 0;
            int PositionB = 0;
            int TempPositionA = 0;
            int TempPositionB = 0;
            int TempPositionC = 0;

            // Multiply
            for (i = start; i < end; i++)
            {
                for (j = 0; j < b.Height; j++)
                {
                    c = 0;
                    TempPositionA = PositionA;
                    TempPositionB = PositionB;
                    for (k = 0; k < a.Width; k++)
                    {
                        c += a.InnerMatrix[TempPositionA] * b.InnerMatrix[TempPositionB];
                        TempPositionA++;
                        TempPositionB++;
                    }
                    PositionB += b.Height;
                    result.InnerMatrix[TempPositionC] = c;
                    TempPositionC++;
                }
                PositionA += a.Height;
                PositionB = 0;
            }

            return result;
        }

        /// <summary>
        /// A method that performs the passed in operation on each cell of the data matrix.
        /// </summary>
        /// <param name="data">The data matrix.</param>
        /// <param name="operation">The operation to perform.</param>
        /// <param name="result">The result matrix.</param>
        private static void PerformOperation(Matrix data, Func<double, double> operation, Matrix result)
        {
            int counter = 0;
            if (operation == null)
            {
                for (int i = 0; i < result.Height; i++)
                {
                    for (int j = 0; j < result.Width; j++)
                    {
                        result.InnerMatrix[counter] = data.InnerMatrix[counter];
                        counter++;
                    }
                }
                return;
            }

            if (!EquallySized(data, result))
            {
                throw new UnevenMatricesException("Cannot perform an operation using incompatible result and data matrices.");
            }

            for (int i = 0; i < result.Height; i++)
            {
                for (int j = 0; j < result.Width; j++)
                {
                    result.InnerMatrix[counter] = operation(data.InnerMatrix[counter]);
                    counter++;
                }
            }
        }

        /// <summary>
        /// Subtract matrix i from j into the result matrix.
        /// </summary>
        /// <param name="a">The matrix to subtract from.</param>
        /// <param name="b">The matrix to subtract.</param>
        /// <param name="result">The result matrix.</param>
        /// <returns></returns>
        private static Matrix Subtract(Matrix a, Matrix b, Matrix result)
        {
            if (!EquallySized(a, b) || !EquallySized(a, result))
            {
                throw new UnevenMatricesException("Cannot subtract uneven matrices.");
            }

            int counter = 0;

            for (int p = 0; p < a.Height; p++)
            {
                for (int q = 0; q < a.Width; q++)
                {
                    result.InnerMatrix[counter] = a.InnerMatrix[counter] - b.InnerMatrix[counter];
                    counter++;
                }
            }

            return result;
        }
    }
}
