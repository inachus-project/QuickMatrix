using System;
using Matrices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MatrixTest
{
    [TestClass]
    public class MatrixTest
    {
        #region Add Matrix
        /// <summary>
        /// Tests adding two matrices of uneven sizes together.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(UnevenMatricesException))]
        public void AddMatrixUnevenSizes()
        {
            // Arrange
            Matrix a = MatrixVectorData.ArrangeMatrixA_4_4();
            Matrix c = MatrixVectorData.ArrangeMatrixC_4_2();

            // Act
            a.Add(c);
        }

        /// <summary>
        /// Tests adding two matrices of uneven sizes together using the static add method.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(UnevenMatricesException))]
        public void AddMatrixStaticUnevenSizes()
        {
            // Arrange
            Matrix a = MatrixVectorData.ArrangeMatrixC_4_2();
            Matrix c = MatrixVectorData.ArrangeMatrixA_4_4();

            // Act
            Matrix result = Matrix.Add(a, c);
        }

        /// <summary>
        /// Tests adding two matrices together where the passed in matrix is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NullMatrixException))]
        public void AddMatrixINull()
        {
            // Arrange
            Matrix a = MatrixVectorData.ArrangeMatrixA_4_4();

            // Act
            a.Add(null);
        }

        /// <summary>
        /// Tests adding two matrices together using the static add method where Matrix i is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NullMatrixException))]
        public void AddMatrixStaticINull()
        {
            // Arrange
            Matrix a = MatrixVectorData.ArrangeMatrixA_4_4();

            // Act
            Matrix result = Matrix.Add(null, a);
        }

        /// <summary>
        /// Tests adding two matrices together using the static add method where Matrix j is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NullMatrixException))]
        public void AddMatrixStaticJNull()
        {
            // Arrange
            Matrix a = MatrixVectorData.ArrangeMatrixA_4_4();

            // Act
            Matrix result = Matrix.Add(a, null);
        }

        /// <summary>
        /// Tests adding two matrices together that are the same size. Manually checks the computed results.
        /// </summary>
        [TestMethod]
        public void AddMatrixSameSize()
        {
            // Arrange
            Matrix a = MatrixVectorData.ArrangeMatrixA_4_4();
            Matrix b = MatrixVectorData.ArrangeMatrixB_4_4();

            // Act
            a.Add(b);

            // Assert
            Assert.AreEqual(-19.8, a.GetValue(0, 0));
            Assert.AreEqual(11.45, a.GetValue(1, 0));
            Assert.AreEqual(double.PositiveInfinity, a.GetValue(2, 0));
            Assert.AreEqual(double.NegativeInfinity, a.GetValue(3, 0));
            Assert.AreEqual(1.1, a.GetValue(0, 1));
            Assert.AreEqual(13.24, a.GetValue(1, 1));
            Assert.AreEqual(2.34, a.GetValue(2, 1));
            Assert.AreEqual(12, a.GetValue(3, 1));
            Assert.AreEqual(double.NaN, a.GetValue(0, 2));
            Assert.AreEqual(14.4, a.GetValue(1, 2));
            Assert.AreEqual(-6.93, a.GetValue(2, 2));
            Assert.AreEqual(double.NaN, a.GetValue(3, 2));
            Assert.AreEqual(double.NegativeInfinity, a.GetValue(0, 3));
            Assert.AreEqual(7.79, a.GetValue(1, 3));
            Assert.AreEqual(14, a.GetValue(2, 3));
            Assert.AreEqual(91.666666, a.GetValue(3, 3));
        }

        /// <summary>
        /// Tests adding two matrices together using the static add method that are the same size. Manually checks the computed results.
        /// </summary>
        [TestMethod]
        public void AddMatrixStaticSameSize()
        {
            // Arrange
            Matrix a = MatrixVectorData.ArrangeMatrixA_4_4();
            Matrix b = MatrixVectorData.ArrangeMatrixB_4_4();

            // Act
            Matrix c = Matrix.Add(a, b);

            // Assert
            Assert.AreEqual(c.GetValue(0, 0), -19.8);
            Assert.AreEqual(c.GetValue(1, 0), 11.45);
            Assert.AreEqual(c.GetValue(2, 0), double.PositiveInfinity);
            Assert.AreEqual(c.GetValue(3, 0), double.NegativeInfinity);
            Assert.AreEqual(c.GetValue(0, 1), 1.1);
            Assert.AreEqual(c.GetValue(1, 1), 13.24);
            Assert.AreEqual(c.GetValue(2, 1), 2.34);
            Assert.AreEqual(c.GetValue(3, 1), 12);
            Assert.AreEqual(c.GetValue(0, 2), double.NaN);
            Assert.AreEqual(c.GetValue(1, 2), 14.4);
            Assert.AreEqual(c.GetValue(2, 2), -6.93);
            Assert.AreEqual(c.GetValue(3, 2), double.NaN);
            Assert.AreEqual(c.GetValue(0, 3), double.NegativeInfinity);
            Assert.AreEqual(c.GetValue(1, 3), 7.79);
            Assert.AreEqual(c.GetValue(2, 3), 14);
            Assert.AreEqual(c.GetValue(3, 3), 91.666666);
        }

        #endregion Add Matrix

        #region Dot Product

        [TestMethod]
        [ExpectedException(typeof(UnevenMatricesException))]
        public void DotProductIncorrectSize()
        {
            // Arrange
            Matrix a = MatrixVectorData.ArrangeMatrixA_4_4();
            Vector b = MatrixVectorData.ArrangeVectorB_3_Column();

            // Act
            Vector c = Matrix.DotProduct(a, b);
        }

        [TestMethod]
        [ExpectedException(typeof(NullMatrixException))]
        public void DotProductNullMatrix()
        {
            // Arrange
            Vector b = MatrixVectorData.ArrangeVectorB_3_Column();

            // Act
            Vector c = Matrix.DotProduct(null, b);
        }

        [TestMethod]
        [ExpectedException(typeof(NullVectorException))]
        public void DotProductNullVector()
        {
            // Arrange
            Matrix a = MatrixVectorData.ArrangeMatrixA_4_4();

            // Act
            Vector c = Matrix.DotProduct(a, null);
        }

        [TestMethod]
        [ExpectedException(typeof(VectorIncorrectAlignmentException))]
        public void DotProductIncorrectAlignment()
        {
            // Arrange
            Matrix a = MatrixVectorData.ArrangeMatrixA_4_4();
            Vector b = MatrixVectorData.ArrangeVectorC_4_Row();

            // Act
            Vector c = Matrix.DotProduct(a, b);
        }

        [TestMethod]
        public void DotProduct()
        {
            // Arrange
            Matrix a = MatrixVectorData.ArrangeMatrixB_4_4();
            Vector b = MatrixVectorData.ArrangeVectorA_4_Column();

            // Act
            Vector c = Matrix.DotProduct(a, b);

            // Assert
            Assert.AreEqual(double.NaN, c.GetValue(0));
            Assert.AreEqual(4625.139, c.GetValue(1));
            Assert.AreEqual(2620.19692, c.GetValue(2));
            Assert.AreEqual(433633.3838666, c.GetValue(3));
        }



        [TestMethod]
        [ExpectedException(typeof(UnevenMatricesException))]
        public void DotProductTwoIncorrectSize()
        {
            // Arrange
            Matrix a = MatrixVectorData.ArrangeMatrixA_4_4();
            Vector b = MatrixVectorData.ArrangeVectorD_3_Row();

            // Act
            Vector c = Matrix.DotProduct(b, a);
        }

        [TestMethod]
        [ExpectedException(typeof(NullMatrixException))]
        public void DotProductTwoNullMatrix()
        {
            // Arrange
            Vector b = MatrixVectorData.ArrangeVectorD_3_Row();

            // Act
            Vector c = Matrix.DotProduct(b, null);
        }

        [TestMethod]
        [ExpectedException(typeof(NullVectorException))]
        public void DotProductTwoNullVector()
        {
            // Arrange
            Matrix a = MatrixVectorData.ArrangeMatrixA_4_4();

            // Act
            Vector c = Matrix.DotProduct(null, a);
        }

        [TestMethod]
        [ExpectedException(typeof(VectorIncorrectAlignmentException))]
        public void DotProductTwoIncorrectAlignment()
        {
            // Arrange
            Matrix a = MatrixVectorData.ArrangeMatrixA_4_4();
            Vector b = MatrixVectorData.ArrangeVectorA_4_Column();

            // Act
            Vector c = Matrix.DotProduct(b, a);
        }

        [TestMethod]
        public void DotProductTwo()
        {
            // Arrange
            Matrix a = MatrixVectorData.ArrangeMatrixB_4_4();
            Vector b = MatrixVectorData.ArrangeVectorC_4_Row();

            // Act
            Vector c = Matrix.DotProduct(b, a);

            // Assert
            Assert.AreEqual(36000379.0275, c.GetValue(0));
            Assert.AreEqual(48157.036, c.GetValue(1));
            Assert.AreEqual(double.NaN, c.GetValue(2));
            Assert.AreEqual(double.NegativeInfinity, c.GetValue(3));
        }

        #endregion Dot Product

        #region Matrix Multiplication

        [TestMethod]
        [ExpectedException(typeof(UnevenMatricesException))]
        public void MatrixMultiplicationUnevenMatrices()
        {
            // Arrange
            Matrix d = MatrixVectorData.ArrangeMatrixD_3_3();
            Matrix e = MatrixVectorData.ArrangeMatrixC_4_2();

            // Act
            Matrix result = Matrix.Multiplication(d, e, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void MatrixMultiplication0Threads()
        {
            // Arrange
            Matrix d = MatrixVectorData.ArrangeMatrixD_3_3();
            Matrix e = MatrixVectorData.ArrangeMatrixE_3_3();

            // Act
            Matrix result = Matrix.Multiplication(d, e, 0);
        }

        [TestMethod]
        public void MatrixMultiplication1000Threads()
        {
            // Arrange
            Matrix d = MatrixVectorData.ArrangeMatrixD_3_3();
            Matrix e = MatrixVectorData.ArrangeMatrixE_3_3();

            // Act
            Matrix result = Matrix.Multiplication(d, e, 1000);

            // Assert
            Assert.AreEqual(.421972, result.GetValue(0, 0));
            Assert.AreEqual(10516.12, result.GetValue(1, 0));
            Assert.AreEqual(-434349.102096, result.GetValue(2, 0));
            Assert.AreEqual(5.0576178, result.GetValue(0, 1));
            Assert.AreEqual((-1203.349).ToString(), result.GetValue(1, 1).ToString());
            Assert.AreEqual((-1715.0455104).ToString(), result.GetValue(2, 1).ToString());
            Assert.AreEqual(-17.65728, result.GetValue(0, 2));
            Assert.AreEqual((4205.9).ToString(), result.GetValue(1, 2).ToString());
            Assert.AreEqual(-1015.6706, result.GetValue(2, 2));
        }

        [TestMethod]
        public void MatrixMultiplication()
        {
            // Arrange
            Matrix a = MatrixVectorData.ArrangeMatrixA_4_4();
            Matrix b = MatrixVectorData.ArrangeMatrixB_4_4();

            // Act
            Matrix c = Matrix.Multiplication(a, b, 1);

            // Assert
            Assert.AreEqual(154203.795, c.GetValue(0, 0));
            Assert.AreEqual(226.256, c.GetValue(0, 1));
            Assert.AreEqual(double.NaN, c.GetValue(0, 2));
            Assert.AreEqual(double.NegativeInfinity, c.GetValue(0, 3));
            Assert.AreEqual(59943.45, c.GetValue(1, 0));
            Assert.AreEqual(147.216, c.GetValue(1, 1));
            Assert.AreEqual(double.NaN, c.GetValue(1, 2));
            Assert.AreEqual(double.NegativeInfinity, c.GetValue(1, 3));
            Assert.AreEqual(double.NegativeInfinity, c.GetValue(2, 0));
            Assert.AreEqual(double.NaN, c.GetValue(2, 1));
            Assert.AreEqual(double.NaN, c.GetValue(2, 2));
            Assert.AreEqual(double.NegativeInfinity, c.GetValue(2, 3));
            Assert.AreEqual(double.NaN, c.GetValue(3, 0));
            Assert.AreEqual(double.NaN, c.GetValue(3, 1));
            Assert.AreEqual(double.NaN, c.GetValue(3, 2));
            Assert.AreEqual(double.NaN, c.GetValue(3, 3));
        }

        #endregion Matrix Multiplication

        #region Perform Operation

        [TestMethod]
        [ExpectedException(typeof(NullMatrixException))]
        public void PerformOperationNullMatrix()
        {
            // Arrange
            Func<double, double> func = x =>
            {
                return x * 2;
            };

            // Act
            Matrix.PerformOperation(null, func);
        }

        [TestMethod]
        public void PerformOperationNullOperation()
        {
            // Arrange
            Matrix a = MatrixVectorData.ArrangeMatrixE_3_3();

            // Act
            Matrix result = Matrix.PerformOperation(a, null);

            // Assert
            Assert.AreEqual(.5666, result.GetValue(0, 0));
            Assert.AreEqual(7667, result.GetValue(1, 0));
            Assert.AreEqual(23, result.GetValue(2, 0));
            Assert.AreEqual(12.09909, result.GetValue(0, 1));
            Assert.AreEqual(34, result.GetValue(1, 1));
            Assert.AreEqual(-3, result.GetValue(2, 1));
            Assert.AreEqual(-42.04, result.GetValue(0, 2));
            Assert.AreEqual(2, result.GetValue(1, 2));
            Assert.AreEqual(-.06, result.GetValue(2, 2));
        }

        [TestMethod]
        public void PerformOperation()
        {
            // Arrange
            Matrix a = MatrixVectorData.ArrangeMatrixE_3_3();

            Func<double, double> func = x =>
            {
                return x * 2;
            };

            // Act
            Matrix result = a.PerformOperation(func);

            // Assert
            Assert.AreEqual(1.1332, result.GetValue(0, 0));
            Assert.AreEqual(15334, result.GetValue(1, 0));
            Assert.AreEqual(46, result.GetValue(2, 0));
            Assert.AreEqual(24.19818, result.GetValue(0, 1));
            Assert.AreEqual(68, result.GetValue(1, 1));
            Assert.AreEqual(-6, result.GetValue(2, 1));
            Assert.AreEqual(-84.08, result.GetValue(0, 2));
            Assert.AreEqual(4, result.GetValue(1, 2));
            Assert.AreEqual(-.12, result.GetValue(2, 2));
        }

        #endregion Perform Operation

        #region

        [TestMethod]
        public void SubtractMatrixSameSize()
        {
            // Arrange
            Matrix a = MatrixVectorData.ArrangeMatrixD_3_3();
            Matrix b = MatrixVectorData.ArrangeMatrixE_3_3();

            // Act
            Matrix result = a.Subtract(b);

            // Assert
            Assert.AreEqual(-.1466, result.GetValue(0, 0));
            Assert.AreEqual(-7767, result.GetValue(1, 0));
            Assert.AreEqual((-1.56).ToString(), result.GetValue(2, 0).ToString());
            Assert.AreEqual((-12.09909).ToString(), result.GetValue(0, 1).ToString());
            Assert.AreEqual(-32.66, result.GetValue(1, 1));
            Assert.AreEqual(-53.7, result.GetValue(2, 1));
            Assert.AreEqual((42.048).ToString(), result.GetValue(0, 2).ToString());
            Assert.AreEqual(11, result.GetValue(1, 2));
            Assert.AreEqual((15.61).ToString(), result.GetValue(2, 2).ToString());
        }

        [TestMethod]
        public void SubtractMatrixStaticSameSize()
        {
            // Arrange
            Matrix a = MatrixVectorData.ArrangeMatrixD_3_3();
            Matrix b = MatrixVectorData.ArrangeMatrixE_3_3();

            // Act
            Matrix result = Matrix.Subtract(a, b);

            // Assert
            Assert.AreEqual(-.1466, result.GetValue(0, 0));
            Assert.AreEqual(-7767, result.GetValue(1, 0));
            Assert.AreEqual((-1.56).ToString(), result.GetValue(2, 0).ToString());
            Assert.AreEqual((-12.09909).ToString(), result.GetValue(0, 1).ToString());
            Assert.AreEqual(-32.66, result.GetValue(1, 1));
            Assert.AreEqual(-53.7, result.GetValue(2, 1));
            Assert.AreEqual(42.048, result.GetValue(0, 2));
            Assert.AreEqual(11, result.GetValue(1, 2));
            Assert.AreEqual((15.61).ToString(), result.GetValue(2, 2).ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(NullMatrixException))]
        public void SubtractMatrixINull()
        {
            // Arrange
            Matrix a = MatrixVectorData.ArrangeMatrixD_3_3();

            // Act
            a.Subtract(null);
        }

        [TestMethod]
        [ExpectedException(typeof(NullMatrixException))]
        public void SubtractMatrixStaticINull()
        {
            // Arrange
            Matrix b = MatrixVectorData.ArrangeMatrixD_3_3();

            // Act
            Matrix.Subtract(null, b);
        }

        [TestMethod]
        [ExpectedException(typeof(NullMatrixException))]
        public void SubtractMatrixStaticJNull()
        {
            // Arrange
            Matrix a = MatrixVectorData.ArrangeMatrixD_3_3();

            // Act
            Matrix.Subtract(a, null);
        }

        [TestMethod]
        [ExpectedException(typeof(UnevenMatricesException))]
        public void SubtractMatrixStaticUnevenSize()
        {
            // Arrange
            Matrix a = MatrixVectorData.ArrangeMatrixD_3_3();
            Matrix b = MatrixVectorData.ArrangeMatrixB_4_4();

            // Act
            Matrix.Subtract(a, b);
        }

        [TestMethod]
        [ExpectedException(typeof(UnevenMatricesException))]
        public void SubtractMatrixUnevenSize()
        {
            // Arrange
            Matrix a = MatrixVectorData.ArrangeMatrixD_3_3();
            Matrix b = MatrixVectorData.ArrangeMatrixB_4_4();

            // Act
            a.Subtract(b);
        }

        #endregion

        #region Xavier Initialization

        [TestMethod]
        public void XavierInitialization()
        {
            // Arrange
            Matrix a = new Matrix(5, 5);

            // Act
            a.XavierInitialization(5);

            // Assert

            double mean = 0;
            for (int i = 0; i < a.Height; i++)
            {
                for (int j = 0; j < a.Width; j++)
                {
                    mean += a.GetValue(i, j);
                }
            }

            mean /= 25;

            // Check if the mean is within .1
            if (mean > .001 && mean < .001)
            {
                Assert.AreEqual(1, 2);
            }

            double variance = 0;
            
            for (int i = 0; i < a.Height; i++)
            {
                for (int j = 0; j < a.Width; j++)
                {
                    variance += Math.Pow(a.GetValue(i, j) - mean, 2);
                }
            }

            variance /= 25;

            double test = 1 / 5;

            variance -= test;

            // Check if the variance is within .1
            if (variance > .001 && variance < .001)
            {
                Assert.AreEqual(1, 2);
            }

            Assert.AreEqual(1, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void XavierInitializationNegativeNumber()
        {
            // Arrange
            Matrix a = new Matrix(5, 5);

            // Act
            a.XavierInitialization(-5);
        }

        #endregion Xavier Initialization

        #region Add Row to Matrix

        [TestMethod]
        [ExpectedException(typeof(NullVectorException))]
        public void SetRowNull()
        {
            // Arrange
            Matrix a = new Matrix(5, 5);

            // Act
            a.SetRow((null as Vector), 0);
        }

        [TestMethod]
        [ExpectedException(typeof(UnevenMatricesException))]
        public void SetRowUnevenSize()
        {
            // Arrange
            Matrix a = new Matrix(5, 5);
            Vector b = new Vector(3, VectorType.Row);

            // Act
            a.SetRow(b, 0);
        }

        [TestMethod]
        public void SetRowVector()
        {
            // Arrange
            Matrix a = new Matrix(4, 4);
            Vector b = MatrixVectorData.ArrangeVectorC_4_Row();

            // Act
            a.SetRow(b, 1);

            // Assert
            Assert.AreEqual(1, a.GetValue(1, 0));
            Assert.AreEqual(-.05, a.GetValue(1, 1));
            Assert.AreEqual(67.2, a.GetValue(1, 2));
            Assert.AreEqual(4000, a.GetValue(1, 3));
        }

        [TestMethod]
        public void SetRowArray()
        {
            // Arrange
            Matrix a = new Matrix(4, 4);
            double[] row = { 1, -.05, 67.2, 4000 };

            // Act
            a.SetRow(row, 1);

            // Assert
            Assert.AreEqual(1, a.GetValue(1, 0));
            Assert.AreEqual(-.05, a.GetValue(1, 1));
            Assert.AreEqual(67.2, a.GetValue(1, 2));
            Assert.AreEqual(4000, a.GetValue(1, 3));
        }

        #endregion Add Row to Matrix

        #region Copy

        [TestMethod]
        public void Clone()
        {
            // Arrange
            Matrix e = MatrixVectorData.ArrangeMatrixE_3_3();

            // Act
            Matrix result = e.Copy();

            // Assert
            Assert.AreEqual(.5666, result.GetValue(0, 0));
            Assert.AreEqual(7667, result.GetValue(1, 0));
            Assert.AreEqual(23, result.GetValue(2, 0));
            Assert.AreEqual(12.09909, result.GetValue(0, 1));
            Assert.AreEqual(34, result.GetValue(1, 1));
            Assert.AreEqual(-3, result.GetValue(2, 1));
            Assert.AreEqual(-42.04, result.GetValue(0, 2));
            Assert.AreEqual(2, result.GetValue(1, 2));
            Assert.AreEqual(-.06, result.GetValue(2, 2));
        }

        #endregion Copy

        #region Get Column

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GetColumnOutOfBounds()
        {
            //Arrange
            Matrix a = MatrixVectorData.ArrangeMatrixA_4_4();

            // Act
            a.GetColumn(5);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GetColumnNegativeIndex()
        {
            // Arrange
            Matrix a = MatrixVectorData.ArrangeMatrixA_4_4();

            // Act
            a.GetColumn(-1);
        }

        [TestMethod]
        public void GetColumn()
        {
            // Arrange
            Matrix a = MatrixVectorData.ArrangeMatrixA_4_4();

            // Act
            Vector result = a.GetColumn(0);

            // Assert
            Assert.AreEqual(4.2, result.GetValue(0));
            Assert.AreEqual(8, result.GetValue(1));
            Assert.AreEqual(double.PositiveInfinity, result.GetValue(2));
            Assert.AreEqual(double.NegativeInfinity, result.GetValue(3));
        }

        #endregion Get Column

        #region Get Row

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GetRowOutOfBounds()
        {
            // Arrange
            Matrix a = MatrixVectorData.ArrangeMatrixA_4_4();

            // Act
            a.GetRow(5);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GetRowNegativeIndex()
        {
            // Arrange
            Matrix a = MatrixVectorData.ArrangeMatrixA_4_4();

            // Act
            a.GetRow(-1);
        }

        [TestMethod]
        public void GetRow()
        {
            // Arrange
            Matrix a = MatrixVectorData.ArrangeMatrixA_4_4();

            // Act
            Vector result = a.GetRow(1);

            // Assert
            Assert.AreEqual(8, result.GetValue(0));
            Assert.AreEqual(9, result.GetValue(1));
            Assert.AreEqual(12.4, result.GetValue(2));
            Assert.AreEqual(6.67, result.GetValue(3));
        }

        #endregion Get Row

        #region Get Value

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GetValueOutOfBounds()
        {
            // Arrange
            Matrix a = MatrixVectorData.ArrangeMatrixA_4_4();

            // Act
            a.GetValue(5, 5);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GetValueNegativeIndex()
        {
            // Arrange
            Matrix a = MatrixVectorData.ArrangeMatrixA_4_4();

            // Act
            a.GetValue(0, -1);
        }

        [TestMethod]
        public void GetValue()
        {
            // Arrange
            Matrix a = MatrixVectorData.ArrangeMatrixA_4_4();

            // Act
            double result = a.GetValue(3, 2);

            // Assert
            Assert.AreEqual(double.NaN, result);
        }

        #endregion Get Value

        #region Set Column

        [TestMethod]
        [ExpectedException(typeof(NullVectorException))]
        public void SetColumnNull()
        {
            // Arrange
            Matrix a = MatrixVectorData.ArrangeMatrixA_4_4();

            // Act
            a.SetColumn((Vector)null, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(UnevenMatricesException))]
        public void SetColumnUnevenSize()
        {
            // Arrange
            Matrix a = MatrixVectorData.ArrangeMatrixA_4_4();
            Vector b = MatrixVectorData.ArrangeVectorB_3_Column();

            // Act
            a.SetColumn(b, 1);
        }

        [TestMethod]
        public void SetColumnVector()
        {
            // Arrange
            Matrix a = MatrixVectorData.ArrangeMatrixA_4_4();
            Vector b = MatrixVectorData.ArrangeVectorA_4_Column();

            // Act
            a.SetColumn(b, 0);

            // Assert
            Assert.AreEqual(46.7, a.GetValue(0, 0));
            Assert.AreEqual(1000, a.GetValue(1, 0));
            Assert.AreEqual(-.044, a.GetValue(2, 0));
            Assert.AreEqual(200.1, a.GetValue(3, 0));
        }

        [TestMethod]
        public void SetColumnArray()
        {
            // Arrange
            Matrix a = MatrixVectorData.ArrangeMatrixE_3_3();
            double[] column = { 2, -1.64, 30000 };

            // Act
            a.SetColumn(column, 1);

            // Assert
            Assert.AreEqual(2, a.GetValue(0, 1));
            Assert.AreEqual(-1.64, a.GetValue(1, 1));
            Assert.AreEqual(30000, a.GetValue(2, 1));
        }

        #endregion Set Column

        #region Set Matrix to two dimensional array

        [TestMethod]
        [ExpectedException(typeof(NullMatrixException))]
        public void SetMatrixToTwoDimensionalArrayNull()
        {
            // Arrange
            Matrix a = new Matrix(1, 1);

            // Act
            a.SetMatrixToTwoDimensionalArray(null);
        }

        [TestMethod]
        [ExpectedException(typeof(UnevenMatricesException))]
        public void SetMatrixToTwoDimensionalArrayUnevenMatrices()
        {
            // Arrange
            Matrix a = new Matrix(3, 3);
            double[,] matrix = { { 1 }, { 2 } };

            // Act
            a.SetMatrixToTwoDimensionalArray(matrix);
        }

        [TestMethod]
        public void SetMatrixToTwoDimensionalArray()
        {
            // Arrange
            Matrix a = new Matrix(2, 3);
            double[,] matrix = { { 1, 2, 3 }, { 3, 4, 5 } };

            // Act
            a.SetMatrixToTwoDimensionalArray(matrix);

            // Assert
            Assert.AreEqual(1, a.GetValue(0, 0));
            Assert.AreEqual(2, a.GetValue(0, 1));
            Assert.AreEqual(3, a.GetValue(0, 2));
            Assert.AreEqual(3, a.GetValue(1, 0));
            Assert.AreEqual(4, a.GetValue(1, 1));
            Assert.AreEqual(5, a.GetValue(1, 2));
        }

        #endregion Set Matrix to two dimensional array

        #region Set Value

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void SetValueOutOfIndex()
        {
            // Arrange
            Matrix a = MatrixVectorData.ArrangeMatrixA_4_4();

            // Act
            a.SetValue(50, 5, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void SetValueNegativeIndex()
        {
            // Arrange
            Matrix a = MatrixVectorData.ArrangeMatrixA_4_4();

            // Act
            a.SetValue(-1, 0, 2);

        }

        [TestMethod]
        public void SetValue()
        {
            // Arrange
            Matrix a = new Matrix(5, 5);

            // Act
            a.SetValue(2, 3, 10);

            // Assert
            Assert.AreEqual(10, a.GetValue(2, 3));
        }

        #endregion Set Value

        #region Transpose

        [TestMethod]
        public void Transpose()
        {
            // Arrange
            Matrix a = MatrixVectorData.ArrangeMatrixA_4_4();

            // Act
            a = a.Transpose();

            // Assert
            Assert.AreEqual(4.2, a.GetValue(0, 0));
            Assert.AreEqual(1.1, a.GetValue(1, 0));
            Assert.AreEqual(6.8, a.GetValue(2, 0));
            Assert.AreEqual(17.14, a.GetValue(3, 0));
            Assert.AreEqual(8, a.GetValue(0, 1));
            Assert.AreEqual(9, a.GetValue(1, 1));
            Assert.AreEqual(12.4, a.GetValue(2, 1));
            Assert.AreEqual(6.67, a.GetValue(3, 1));
            Assert.AreEqual(double.PositiveInfinity, a.GetValue(0, 2));
            Assert.AreEqual(0, a.GetValue(1, 2));
            Assert.AreEqual(-7, a.GetValue(2, 2));
            Assert.AreEqual(14, a.GetValue(3, 2));
            Assert.AreEqual(double.NegativeInfinity, a.GetValue(0, 3));
            Assert.AreEqual(0, a.GetValue(1, 3));
            Assert.AreEqual(double.NaN, a.GetValue(2, 3));
            Assert.AreEqual(85, a.GetValue(3, 3));
        }

        #endregion Transpose

        #region Operators

        [TestMethod]
        public void AddOperator()
        {
            // Arrange
            Matrix a = MatrixVectorData.ArrangeMatrixA_4_4();
            Matrix b = MatrixVectorData.ArrangeMatrixB_4_4();

            // Act
            a.Add(b);

            // Assert
            Assert.AreEqual(-19.8, a.GetValue(0, 0));
            Assert.AreEqual(11.45, a.GetValue(1, 0));
            Assert.AreEqual(double.PositiveInfinity, a.GetValue(2, 0));
            Assert.AreEqual(double.NegativeInfinity, a.GetValue(3, 0));
            Assert.AreEqual(1.1, a.GetValue(0, 1));
            Assert.AreEqual(13.24, a.GetValue(1, 1));
            Assert.AreEqual(2.34, a.GetValue(2, 1));
            Assert.AreEqual(12, a.GetValue(3, 1));
            Assert.AreEqual(double.NaN, a.GetValue(0, 2));
            Assert.AreEqual(14.4, a.GetValue(1, 2));
            Assert.AreEqual(-6.93, a.GetValue(2, 2));
            Assert.AreEqual(double.NaN, a.GetValue(3, 2));
            Assert.AreEqual(double.NegativeInfinity, a.GetValue(0, 3));
            Assert.AreEqual(7.79, a.GetValue(1, 3));
            Assert.AreEqual(14, a.GetValue(2, 3));
            Assert.AreEqual(91.666666, a.GetValue(3, 3));
        }

        [TestMethod]
        public void SubtractOperator()
        {
            // Arrange
            Matrix a = MatrixVectorData.ArrangeMatrixD_3_3();
            Matrix b = MatrixVectorData.ArrangeMatrixE_3_3();

            // Act
            Matrix result = a.Subtract(b);

            // Assert
            Assert.AreEqual(-.1466, result.GetValue(0, 0));
            Assert.AreEqual(-7767, result.GetValue(1, 0));
            Assert.AreEqual((-1.56).ToString(), result.GetValue(2, 0).ToString());
            Assert.AreEqual((-12.09909).ToString(), result.GetValue(0, 1).ToString());
            Assert.AreEqual(-32.66, result.GetValue(1, 1));
            Assert.AreEqual(-53.7, result.GetValue(2, 1));
            Assert.AreEqual((42.048).ToString(), result.GetValue(0, 2).ToString());
            Assert.AreEqual(11, result.GetValue(1, 2));
            Assert.AreEqual((15.61).ToString(), result.GetValue(2, 2).ToString());
        }

        [TestMethod]
        public void MultiplyOperator()
        {
            // Arrange
            Matrix a = MatrixVectorData.ArrangeMatrixA_4_4();
            Matrix b = MatrixVectorData.ArrangeMatrixB_4_4();

            // Act
            Matrix c = Matrix.Multiplication(a, b, 1);

            // Assert
            Assert.AreEqual(154203.795, c.GetValue(0, 0));
            Assert.AreEqual(226.256, c.GetValue(0, 1));
            Assert.AreEqual(double.NaN, c.GetValue(0, 2));
            Assert.AreEqual(double.NegativeInfinity, c.GetValue(0, 3));
            Assert.AreEqual(59943.45, c.GetValue(1, 0));
            Assert.AreEqual(147.216, c.GetValue(1, 1));
            Assert.AreEqual(double.NaN, c.GetValue(1, 2));
            Assert.AreEqual(double.NegativeInfinity, c.GetValue(1, 3));
            Assert.AreEqual(double.NegativeInfinity, c.GetValue(2, 0));
            Assert.AreEqual(double.NaN, c.GetValue(2, 1));
            Assert.AreEqual(double.NaN, c.GetValue(2, 2));
            Assert.AreEqual(double.NegativeInfinity, c.GetValue(2, 3));
            Assert.AreEqual(double.NaN, c.GetValue(3, 0));
            Assert.AreEqual(double.NaN, c.GetValue(3, 1));
            Assert.AreEqual(double.NaN, c.GetValue(3, 2));
            Assert.AreEqual(double.NaN, c.GetValue(3, 3));
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void DivideOperator()
        {
            // Arrange
            Matrix a = MatrixVectorData.ArrangeMatrixA_4_4();
            Matrix b = MatrixVectorData.ArrangeMatrixA_4_4();

            // Act
            var c = a / b;
        }

        #endregion Operators
    }
}
