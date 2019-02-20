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
        #region Properties

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

        #endregion Properties

        #region Constructor

        public Vector(int length, VectorType vectorType)
        {
            InnerVector = new double[length];
            Length = length;
            VectorType = vectorType;
        }

        #endregion Constructor

        #region Methods

        #region Operations

        /// <summary>
        /// Adds two vectors together
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        public static Vector AddVector(Vector i, Vector j)
        {
            if (i.Length != j.Length || i.VectorType != j.VectorType)
            {
                throw new Exception("Cannot add uneven vectors or vectors of different types!");
            }

            Vector result = new Vector(i.Length, i.VectorType);
            
            for (int q = 0; q < i.Length; q++)
            {
                result.InnerVector[q] = i.InnerVector[q] + j.InnerVector[q];
            }

            return result;
        }

        /// <summary>
        /// Calculates the dot product of two vectors
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        public static double DotProduct(Vector A, Vector B)
        {
            if(A.Length != B.Length || A.VectorType == B.VectorType )
            {
                throw new Exception("Cannot perform dot product for differently sized vectors or vectors of the same type!");
            }
            
            double c = 0;
            for (int k = 0; k < A.Length; k++)
            {
                c += A.InnerVector[k] * B.InnerVector[k];
            }
            
            return c;
        }
        
        /// <summary>
        /// Performs the passed in operation on each value in the vector individually.
        /// </summary>
        /// <param name="Operation"></param>
        public void PerformOperationAsDouble(Func<double, double> Operation)
        {
            for (int j = 0; j < Length; j++)
            {
                InnerVector[j] = Operation(InnerVector[j]);
            }
        }

        /// <summary>
        /// Computes the outer product matrix of two vectors
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        public static Matrix OuterProduct(Vector A, Vector B)
        {
            Matrix m = new Matrix(A.Length, B.Length);

            int counter = 0;
            for(int i = 0; i < A.Length; i++)
            {
                for(int j = 0; j < B.Length; j++)
                {
                    m.InnerMatrix[counter] = A.InnerVector[i] * B.InnerVector[j];
                    counter++;
                }
            }

            return m;
        }

        /// <summary>
        /// Performs the passed in operation on each value in the vector individually.
        /// </summary>
        /// <param name="Operation"></param>
        public Vector ReturnOperationAsDouble(Func<double, double> Operation)
        {
            Vector newVector = new Vector(Length, VectorType);
            
            for (int j = 0; j < Length; j++)
            {
                newVector.InnerVector[j] = Operation(InnerVector[j]);
            }

            return newVector;
        }

        /// <summary>
        /// Subtracts vector j from vector i
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        public static Vector SubtractVectors(Vector i, Vector j)
        {
            if (i.Length != j.Length || i.VectorType != j.VectorType)
            {
                throw new Exception("Cannot subtract vectors of different sizes or of different types.");
            }

            Vector result = new Vector(i.Length, i.VectorType);
            
            for (int q = 0; q < i.Length; q++)
            {
                result.InnerVector[q] = i.InnerVector[q] - j.InnerVector[q];
            }

            return result;
        }

        /// <summary>
        /// Multiplies one vector by another vector
        /// </summary>
        /// <param name="A"></param>
        public void Multiply(Vector A)
        {
            if(A.Length != Length || A.VectorType != VectorType)
            {
                throw new Exception("Vectors must be same size and of the same type to multiply!");
            }

            for(int i = 0; i < Length; i++)
            {
                InnerVector[i] *= A.InnerVector[i];
            }
        }

        #endregion Operations

        #region Utilities

        /// <summary>
        /// Gets the value of the cell
        /// </summary>
        /// <param name="Row"></param>
        /// <param name="Column"></param>
        /// <returns></returns>
        public double GetValue(int Column)
        {
            if (Length > Column)
            {
                return InnerVector[Column];
            }

            throw new IndexOutOfRangeException("Matrix call is out of bounds!");
        }

        /// <summary>
        /// Sets the value of the cell to the passed in value.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="Value"></param>
        public void SetValue(int column, double Value)
        {
            if (Length > column)
            {
                InnerVector[column] = Value;
            }
        }

        /// <summary>
        /// Creates a copy of the vector that has allocated its own memory
        /// </summary>
        /// <returns></returns>
        public Vector Copy()
        {
            Vector NewVector = new Vector(Length, VectorType);

            for (int j = 0; j < Length; j++)
            {
                NewVector.InnerVector[j] = InnerVector[j];
            }

            return NewVector;
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
            for (int i =0; i < this.Length; i++)
            {
                temp.InnerVector[i] = InnerVector[i];
            }

            return temp;
        }

        #endregion Utilities

        #endregion Methods

    }
}
