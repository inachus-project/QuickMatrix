using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrices
{
    public enum VectorType
    {
        Column,
        Row
    }

    public class Vector
    {
        /// <summary>
        /// The matrix values in memory as an array.
        /// </summary>
        internal double[] InnerVector;

        /// <summary>
        /// Represents the width of the matrix
        /// </summary>
        public readonly int Length;

        /// <summary>
        /// Set to true if the vector is a row; Otherwise, its a column.
        /// </summary>
        public readonly VectorType VectorType;

        /// <summary>
        /// Creates a vector of the specified length.
        /// </summary>
        /// <param name="length">The length of the vector</param>
        /// <param name="vectorType">Specifies a vector type</param>
        public Vector(int length, VectorType vectorType)
        {
            InnerVector = new double[length];
            Length = length;
            VectorType = vectorType;
        }

        /// <summary>
        /// Adds the passed in vector to the current vector and returns the result.
        /// </summary>
        /// <param name="a">The vector to add.</param>
        /// <returns>The result.</returns>
        public Vector Add(Vector a)
        {
            return Add(this, a);
        }

        /// <summary>
        /// Creates a copy of the vector that has allocated its own memory
        /// </summary>
        /// <returns></returns>
        public Vector Copy()
        {
            Vector newVector = new Vector(Length, VectorType);

            for (int j = 0; j < Length; j++)
            {
                newVector.InnerVector[j] = InnerVector[j];
            }

            return newVector;
        }

        /// <summary>
        /// Finds the dot product of the two vectors and returns the result.
        /// </summary>
        /// <param name="b">The second vector.</param>
        /// <returns>The result.</returns>
        public double DotProduct(Vector b)
        {
            return DotProduct(this, b);
        }

        /// <summary>
        /// Gets the value at the passed in index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>The value.</returns>
        public double GetValue(int index)
        {
            if (index >= Length || index < 0)
            {
                throw new IndexOutOfRangeException("Cannot get value at out of bounds index.");
            }

            return InnerVector[index];
        }

        /// <summary>
        /// Multiplies the corresonding values in vector a by the values in vector b.
        /// </summary>
        /// <param name="b">Vector b.</param>
        /// <returns>The results.</returns>
        public Vector Multiply(Vector b)
        {
            return Multiply(this, b);
        }

        /// <summary>
        /// Performs the outer product operation on the two vectors.
        /// </summary>
        /// <param name="b">The second vector.</param>
        /// <returns>The result matrix.</returns>
        public Matrix OuterProduct(Vector b)
        {
            return OuterProduct(this, b);
        }

        /// <summary>
        /// Sets the value of the cell to the passed in value.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="index"></param>
        /// <param name="Value"></param>
        public void SetValue(int index, double Value)
        {
            if (index >= Length || index < 0)
            {
                throw new IndexOutOfRangeException("Cannot get value at out of bounds index.");
            }

            InnerVector[index] = Value;
        }

        /// <summary>
        /// Gets the 
        /// </summary>
        /// <returns></returns>
        public Vector Transpose()
        {
            VectorType newType;

            if (VectorType == VectorType.Column)
            {
                newType = VectorType.Row;
            }
            else
            {
                newType = VectorType.Column;
            }

            Vector temp = new Vector(Length, newType);
            for (int i = 0; i < this.Length; i++)
            {
                temp.InnerVector[i] = InnerVector[i];
            }

            return temp;
        }

        /// <summary>
        /// Subtracts the passed in vector from the current vector.
        /// </summary>
        /// <param name="b">The second vector.</param>
        /// <returns></returns>
        public Vector Subtract(Vector b)
        {
            return Subtract(this, b);
        }
        
        /// <summary>
        /// Adds the two passed in vectors and returns the result.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        /// <returns>The second vector.</returns>
        public static Vector Add(Vector a, Vector b)
        {
            if (a == null || b == null)
            {
                throw new NullVectorException("Cannot add a null vector.");
            }

            Vector result = new Vector(a.Length, a.VectorType);

            if (a.Length != b.Length)
            {
                throw new UnevenMatricesException("Cannot add uneven vectors.");
            }

            if (a.VectorType != b.VectorType)
            {
                throw new VectorIncorrectAlignmentException("Cannot add two vectors of differing alignments.");
            }

            for (int q = 0; q < a.Length; q++)
            {
                result.InnerVector[q] = a.InnerVector[q] + b.InnerVector[q];
            }

            return result;
        }

        /// <summary>
        /// Computes the dot product of the two vectors.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        /// <returns>The result.</returns>
        public static double DotProduct(Vector a, Vector b)
        {
            if (a == null || b == null)
            {
                throw new NullVectorException("Cannot perform the dot product if either vector is null.");
            }

            if (a.Length != b.Length)
            {
                throw new UnevenMatricesException("Cannot perform this operation with uneven vectors.");
            }

            if (a.VectorType == b.VectorType)
            {
                throw new VectorIncorrectAlignmentException("Cannot perform the dot product operation on vectors with the same alignment.");
            }

            double c = 0;
            for (int k = 0; k < a.Length; k++)
            {
                c += a.InnerVector[k] * b.InnerVector[k];
            }

            return c;
        }

        /// <summary>
        /// Multiplies the each value in a by the corresponding value in b.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        /// <returns>The result vector.</returns>
        public static Vector Multiply(Vector a, Vector b)
        {
            if (a == null || b == null)
            {
                throw new NullVectorException("Cannot multiply from null vectors.");
            }

            if (a.Length != b.Length)
            {
                throw new UnevenMatricesException("Cannot multiply vectors of different sizes.");
            }

            if (a.VectorType != b.VectorType)
            {
                throw new VectorIncorrectAlignmentException("Cannot multiply vectors of different alignments.");
            }

            Vector result = new Vector(a.Length, a.VectorType);

            for (int i = 0; i < result.Length; i++)
            {
                result.InnerVector[i] = a.InnerVector[i] * b.InnerVector[i];
            }

            return result;
        }

        /// <summary>
        /// Computes the outer product of the two vectors.
        /// </summary>
        /// <param name="a">Vector a.</param>
        /// <param name="b">Vector b.</param>
        /// <returns>The result matrix.</returns>
        public static Matrix OuterProduct(Vector a, Vector b)
        {
            if (a == null || b == null)
            {
                throw new NullVectorException("Cannot perform the outer product operation with null vectors.");
            }

            if (a.VectorType == b.VectorType)
            {
                throw new VectorIncorrectAlignmentException("Cannot perform the outer product operation with two vectors of the same alignment.");
            }

            Matrix m = new Matrix(a.Length, b.Length);

            int counter = 0;
            for (int i = 0; i < a.Length; i++)
            {
                for (int j = 0; j < b.Length; j++)
                {
                    m.InnerMatrix[counter] = a.InnerVector[i] * b.InnerVector[j];
                    counter++;
                }
            }

            return m;
        }

        /// <summary>
        /// Performs the passed in operation on each value in the passed in vector.
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <param name="a">The vector.</param>
        /// <returns>The result vector.</returns>
        public static Vector PerformOperation(Func<double, double> operation, Vector a)
        {
            if (operation == null)
            {
                throw new NullReferenceException("Cannot consume a null operation.");
            }

            if (a == null)
            {
                throw new NullVectorException("Cannot perform an operation on a null vector.");
            }

            Vector result = new Vector(a.Length, a.VectorType);

            for (int j = 0; j < a.Length; j++)
            {
                result.InnerVector[j] = operation(a.InnerVector[j]);
            }

            return result;
        }

        /// <summary>
        /// Subtracts vector j from vector i
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Vector Subtract(Vector a, Vector b)
        {
            if (a == null || b == null)
            {
                throw new NullVectorException("Cannot subtract from null vectors.");
            }

            if (a.Length != b.Length)
            {
                throw new UnevenMatricesException("Cannot subtract vectors of different sizes.");
            }

            if (a.VectorType != b.VectorType)
            {
                throw new VectorIncorrectAlignmentException("Cannot subtract vectors of different alignments.");
            }

            Vector result = new Vector(a.Length, a.VectorType);
            
            for (int q = 0; q < a.Length; q++)
            {
                result.InnerVector[q] = a.InnerVector[q] - b.InnerVector[q];
            }

            return result;
        }

        /// <summary>
        /// Adds the two passed in vectors together.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        /// <returns>The result vector.</returns>
        public static Vector operator+ (Vector a, Vector b)
        {
            return Add(a, b);
        }

        /// <summary>
        /// Subtract the vector b from vector a.
        /// </summary>
        /// <param name="a">Vector a.</param>
        /// <param name="b">Vector b.</param>
        /// <returns>The result vector.</returns>
        public static Vector operator- (Vector a, Vector b)
        {
            return Subtract(a, b);
        }

        /// <summary>
        /// Divides vector a by vector b.
        /// </summary>
        /// <param name="a">Vector a.</param>
        /// <param name="b">Vector b.</param>
        /// <returns></returns>
        public static Vector operator/ (Vector a, Vector b)
        {
            throw new NotImplementedException("Not yet implemented");
        }

        /// <summary>
        /// Multiplies the two vectors together.
        /// </summary>
        /// <param name="a">Vector a.</param>
        /// <param name="b">Vector b.</param>
        /// <returns>The result vector.</returns>
        public static Vector operator* (Vector a, Vector b)
        {
            return Multiply(a, b);
        }
    }
}
