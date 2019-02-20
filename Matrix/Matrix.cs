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
        #region Properties

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

        #endregion Properties

        #region Constructor

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

        #endregion Constructor

        #region Methods

        #region Operations

        /// <summary>
        /// Adds the passed in matrix to the current matrix.
        /// </summary>
        /// <param name="i">The matrix to add.</param>
        public void AddMatrix(Matrix i)
        {
            // Cannot add uneven matrices
            if (i.Height != Height || i.Width != Width)
            {
                throw new Exception("Cannot add uneven matrices!");
            }

            int counter = 0;
            for (int p = 0; p < i.Height; p++)
            {
                for (int q = 0; q < i.Width; q++)
                {
                    InnerMatrix[counter] = i.InnerMatrix[counter] + InnerMatrix[counter];
                    counter++;
                }
            }
        }

        /// <summary>
        /// Adds the two passed in matrices, returns the result
        /// </summary>
        /// <param name="i">The first matrix.</param>
        /// <param name="j">The second matrix.</param>
        /// <returns>The result matrix.</returns>
        public static Matrix AddMatrix(Matrix i, Matrix j)
        {
            if(i.Height != j.Height || i.Width != j.Width)
            {
                throw new Exception("Cannot add uneven matrices!");
            }

            Matrix result = new Matrix(i.Height, i.Width);

            int counter = 0;
            for(int p = 0; p < i.Height; p++)
            {
                for(int q = 0; q < i.Width; q++)
                {
                    result.InnerMatrix[counter] = i.InnerMatrix[counter] + j.InnerMatrix[counter];
                    counter++;
                }
            }

            return result;
        }

        /// <summary>
        /// Calculates the dot product of a matrix and vector
        /// </summary>
        /// <param name="A">The matrix.</param>
        /// <param name="B">The vector.</param>
        /// <returns>The resulting vector.</returns>
        public static Vector DotProduct(Matrix A, Vector B)
        {
            if ((B.VectorType != VectorType.Column || B.Length != A.Width))
            {
                throw new Exception("Vector must be a column with length equal to the matrices width!");
            }

            Vector result = new Vector(A.Height, VectorType.Row);
            
            double c;
            int counter = 0;
            int i, j;

            for (i = 0; i < A.Height; i++)
            {
                c = 0;
                for (j = 0; j < A.Width; j++)
                {
                    c += A.InnerMatrix[counter] * B.InnerVector[j];
                    counter++;
                }
                result.InnerVector[i] = c;
            }

            return result;
        }

        /// <summary>
        /// Calculates the dot product of a matrix and vector
        /// </summary>
        /// <param name="A">The vector.</param>
        /// <param name="B">The matrix.</param>
        /// <returns>The result vector.</returns>
        public static Vector DotProduct(Vector A, Matrix B)
        {
            if ((A.VectorType != VectorType.Row || A.Length != B.Height))
            {
                throw new Exception("Vector must be a column with length equal to the matrices width!");
            }

            Vector result = new Vector(B.Width, VectorType.Column);

            double c;
            int counter = 0;
            int i, j;

            B = B.Transpose();

            for (i = 0; i < B.Height; i++)
            {
                c = 0;
                for (j = 0; j < B.Width; j++)
                {
                    c += B.InnerMatrix[counter] * A.InnerVector[j];
                    counter++;
                }
                result.InnerVector[i] = c;
            }

            return result;
        }

        /// <summary>
        /// Multiplies the two supplied matrices together. Uses a customizable number of threads to compute the result.
        /// </summary>
        /// <param name="a">The first matrix.</param>
        /// <param name="b">The second matrix.</param>
        /// <param name="numberOfThreads">The number of threads to use. The main thread counts as one of the used threads.</param>
        /// <returns></returns>
        public static Matrix MatrixMultiplication(Matrix a, Matrix b, int NumberOfThreads)
        {
            if (a.Width != b.Height)
            {
                throw new Exception("To perform dot product the first matrix must have a width equal to the seconds height!");
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
                        MatrixMulitplicationInner(a, b, ref result, counter, a.Height);
                    }));
                }
                else
                {
                    threads.Add(new Thread(() =>
                    {
                        MatrixMulitplicationInner(a, b, ref result, counter, counter + divided);
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
        /// Performs the passed in operation on each value in the matrix individually.
        /// </summary>
        /// <param name="operation">The operation to perform.</param>
        public void PerformOperationAsDouble(Func<double, double> operation)
        {
            int counter = 0;
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    InnerMatrix[counter] = operation(InnerMatrix[counter]);
                    counter++;
                }
            }
        }

        /// <summary>
        /// Performs the passed in operation on each value in the matrix individually.
        /// </summary>
        /// <param name="operation">The operation to perfom.</param>
        public Matrix ReturnOperationAsDouble(Func<double, double> operation)
        {
            Matrix newMatrix = new Matrix(Height, Width);
            int counter = 0;
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    newMatrix.InnerMatrix[counter] = operation(InnerMatrix[counter]);
                    counter++;
                }
            }

            return newMatrix;
        }
        
        /// <summary>
        /// Subtracts matrix j from matrix i
        /// </summary>
        /// <param name="i">The matrix to subtract from..</param>
        /// <param name="j">The matrix to subtract.</param>
        /// <returns>The result matrix.</returns>
        public static Matrix SubtractMatrices(Matrix i, Matrix j)
        {
            if (i.Height != j.Height || i.Width != j.Width)
            {
                return null;
            }

            Matrix result = new Matrix(i.Height, i.Width);

            int counter = 0;

            for (int p = 0; p < i.Height; p++)
            {
                for (int q = 0; q < i.Width; q++)
                {
                    result.InnerMatrix[counter] = i.InnerMatrix[counter] - j.InnerMatrix[counter];
                    counter++;
                }
            }

            return result;
        }

        /// <summary>
        /// Initializes the matrix using Xavier Initialization
        /// </summary>
        /// <param name="numberOfInputNeurons">The number of input neurons.</param>
        public void XavierInitialization(int numberOfInputNeurons)
        {
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

                    if(r.NextDouble() > .5)
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
        /// Performs matrix multiplication on values between the specified start and end value.
        /// </summary>
        /// <param name="a">The first matrix.</param>
        /// <param name="b">The second matrix.</param>
        /// <param name="result">The matrix to store the results in.</param>
        /// <param name="start">The index to start at.</param>
        /// <param name="end">The index to end at.</param>
        /// <returns></returns>
        private static Matrix MatrixMulitplicationInner(Matrix a, Matrix b, ref Matrix result, int start, int end)
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
        #endregion Operations

        #region Utilities


        /// <summary>
        /// Sets the row at the passed in index to the passed in rows values
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="RowIndex">The index of the row.</param>
        public void AddRowToMatrix(double[] row, int rowIndex)
        {
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
        /// Aquires a column of matrix as a vector
        /// </summary>
        /// <param name="columnIndex">The index of the column.</param>
        /// <returns>The column.</returns>
        public Vector GetColumn(int columnIndex)
        {
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
            if (Height > row && Width > column)
            {
                return InnerMatrix[row * Width + column];
            }

            throw new IndexOutOfRangeException("Matrix call is out of bounds!");
        }

        /// <summary>
        /// Sets the value of the column equal to the passed in values.
        /// </summary>
        /// <param name="columnValues">The values of the column.</param>
        /// <param name="columnNumber">The column number.</param>
        public void SetColumn(Vector columnValues, int columnNumber)
        {
            if (columnValues.Length != Height)
            {
                throw new Exception("Passed in vector must have same width as this matrix!");
            }

            for (int i = 0; i < columnValues.Length; i++)
            {
                InnerMatrix[i * Width + columnNumber] = columnValues.InnerVector[i];
            }
        }

        /// <summary>
        /// Sets the values of the matrix to the values of the passed in array.
        /// </summary>
        /// <param name="matrix">The two dimensional array.</param>
        public void SetMatrixToTwoDimensionalArray(double[,] matrix)
        {
            int counter = 0;
            if (matrix.GetLength(0) == Height && matrix.GetLength(1) == Width)
            {
                for (int row = 0; row < Height; row++)
                {
                    for (int column = 0; column < Width; column++)
                    {
                        InnerMatrix[counter] = matrix[row,column];
                        counter++;
                    }
                }
            }
        }

        /// <summary>
        /// Sets the value of the row equal to the passed in values.
        /// </summary>
        /// <param name="rowValues">The row.</param>
        /// <param name="RowNumber">The row number.</param>
        public void SetRow(Vector rowValues, int rowNumber)
        {
            if (rowValues.Length != Width)
            {
                throw new Exception("Passed in vector must have same width as this matrix!");
            }

            int counter = rowNumber * Width;
            for (int i = 0; i < rowValues.Length; i++)
            {
                InnerMatrix[counter] = rowValues.InnerVector[i];
                counter++;
            }
        }

        /// <summary>
        /// Sets the value of the cell to the passed in value.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="column">The column.</param>
        /// <param name="Value">The value.</param>
        public void SetValue(int row, int column, double Value)
        {
            if (Width > column && Height > row)
            {
                InnerMatrix[row * Width + column] = Value;
            }
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

        #endregion Utilities

        #endregion Methods
    }
}
