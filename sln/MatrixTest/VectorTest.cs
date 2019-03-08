using Matrices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixTest
{
    [TestClass]
    public class VectorTest
    {
        #region Add Vector
        [TestMethod]
        [ExpectedException(typeof(NullVectorException))]
        public void AddVectorNullVector()
        {
            // Arrange
            Vector a = MatrixVectorData.ArrangeVectorA_4_Column();

            // Act
            Vector.Add(null, a);
        }

        [TestMethod]
        [ExpectedException(typeof(NullVectorException))]
        public void AddVectorNullVector2()
        {
            // Arrange
            Vector a = MatrixVectorData.ArrangeVectorB_3_Column();

            // Act
            Vector.Add(a, null);
        }

        [TestMethod]
        [ExpectedException(typeof(UnevenMatricesException))]
        public void AddVectorUnevenVector()
        {
            // Arrange
            Vector a = MatrixVectorData.ArrangeVectorB_3_Column();
            Vector b = MatrixVectorData.ArrangeVectorA_4_Column();

            // Act
            Vector.Add(a, b);
        }

        [TestMethod]
        [ExpectedException(typeof(VectorIncorrectAlignmentException))]
        public void AddVectorIncorrectAlignment()
        {
            // Arrange
            Vector a = MatrixVectorData.ArrangeVectorB_3_Column();
            Vector b = MatrixVectorData.ArrangeVectorD_3_Row();

            // Act
            a.Add(b);
        }

        [TestMethod]
        public void AddVector()
        {
            // Arrange
            Vector a = MatrixVectorData.ArrangeVectorB_3_Column();
            Vector b = MatrixVectorData.ArrangeVectorB_3_Column();

            // Act
            Vector result = a.Add(b);

            // Assert
            Assert.AreEqual(4, result.GetValue(0));
            Assert.AreEqual(-3.28, result.GetValue(1));
            Assert.AreEqual(60000, result.GetValue(2));
        }

        #endregion Add Vector

        #region Dot Product

        [TestMethod]
        [ExpectedException(typeof(NullVectorException))]
        public void DotProductNullVector()
        {
            // Arrange
            Vector a = MatrixVectorData.ArrangeVectorB_3_Column();

            // Act
            Vector.DotProduct(null, a);
        }

        [TestMethod]
        [ExpectedException(typeof(NullVectorException))]
        public void DotProductNullVector2()
        {
            // Arrange
            Vector a = MatrixVectorData.ArrangeVectorB_3_Column();

            // Act
            Vector.DotProduct(a, null);
        }

        [TestMethod]
        [ExpectedException(typeof(UnevenMatricesException))]
        public void DotProductUnevenVector()
        {
            // Arrange
            Vector a = MatrixVectorData.ArrangeVectorB_3_Column();
            Vector b = MatrixVectorData.ArrangeVectorA_4_Column();

            // Act
            Vector.DotProduct(a, b);
        }

        [TestMethod]
        [ExpectedException(typeof(VectorIncorrectAlignmentException))]
        public void DotProductIncorrectAlignment()
        {
            // Arrange
            Vector a = MatrixVectorData.ArrangeVectorB_3_Column();
            Vector b = MatrixVectorData.ArrangeVectorB_3_Column();

            // Act
            Vector.DotProduct(a, b);
        }

        [TestMethod]
        public void DotProduct()
        {
            // Arrange
            Vector a = MatrixVectorData.ArrangeVectorA_4_Column();
            Vector b = MatrixVectorData.ArrangeVectorC_4_Row();

            // Act
            double result = a.DotProduct(b);

            // Assert
            Assert.AreEqual(800393.7432, result);
        }

        #endregion Dot Product

        #region Perform Operation

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void PerformOperationNull()
        {
            // Arrange
            Vector a = MatrixVectorData.ArrangeVectorC_4_Row();

            // Act
            Vector.PerformOperation(null, a);
        }

        [TestMethod]
        [ExpectedException(typeof(NullVectorException))]
        public void PerformOperationNullVector()
        {
            // Arrange
            Func<double, double> func = x => x * 2;

            // Act
            Vector.PerformOperation(func, null);
        }

        [TestMethod]
        public void PerformOperation()
        {
            // Arrange
            Func<double, double> func = x => x * 2;
            Vector a = MatrixVectorData.ArrangeVectorB_3_Column();

            // Act
            Vector result = Vector.PerformOperation(func, a);

            // Assert
            Assert.AreEqual(4, result.GetValue(0));
            Assert.AreEqual(-3.28, result.GetValue(1));
            Assert.AreEqual(60000, result.GetValue(2));
        }

        #endregion Perform Operation

        #region Outer Product

        [TestMethod]
        [ExpectedException(typeof(NullVectorException))]
        public void OuterProductNullVector()
        {
            // Arrange
            Vector a = MatrixVectorData.ArrangeVectorB_3_Column();

            // Act
            Vector.OuterProduct(null, a);
        }

        [TestMethod]
        [ExpectedException(typeof(NullVectorException))]
        public void OuterProductNullVector2()
        {
            // Arrange
            Vector a = MatrixVectorData.ArrangeVectorA_4_Column();

            // Act
            Vector.OuterProduct(a, null);
        }

        [TestMethod]
        [ExpectedException(typeof(VectorIncorrectAlignmentException))]
        public void OuterProductIncorrectAlignment()
        {
            // Arrange
            Vector a = MatrixVectorData.ArrangeVectorA_4_Column();
            Vector b = MatrixVectorData.ArrangeVectorA_4_Column();

            // Act
            Vector.OuterProduct(a, b);
        }

        [TestMethod]
        public void OuterProduct()
        {
            // Arrange
            Vector a = MatrixVectorData.ArrangeVectorA_4_Column();
            Vector b = MatrixVectorData.ArrangeVectorC_4_Row();

            // Act
            Matrix result = Vector.OuterProduct(a, b);

            // Assert
            Assert.AreEqual(46.7, result.GetValue(0, 0));
            Assert.AreEqual(1000, result.GetValue(1, 0));
            Assert.AreEqual(-.044, result.GetValue(2, 0));
            Assert.AreEqual(200.1, result.GetValue(3, 0));
            Assert.AreEqual((-2.335).ToString(), result.GetValue(0, 1).ToString());
            Assert.AreEqual(-50, result.GetValue(1, 1));
            Assert.AreEqual(.0022, result.GetValue(2, 1));
            Assert.AreEqual(-10.005, result.GetValue(3, 1));
            Assert.AreEqual((3138.24).ToString(), result.GetValue(0, 2).ToString());
            Assert.AreEqual(67200, result.GetValue(1, 2));
            Assert.AreEqual(-2.9568, result.GetValue(2, 2));
            Assert.AreEqual(13446.72, result.GetValue(3, 2));
            Assert.AreEqual(186800, result.GetValue(0, 3));
            Assert.AreEqual(4000000, result.GetValue(1, 3));
            Assert.AreEqual(-176, result.GetValue(2, 3));
            Assert.AreEqual(800400, result.GetValue(3, 3));
        }

        #endregion Outer Product

        #region Subtract Vectors

        [TestMethod]
        [ExpectedException(typeof(NullVectorException))]
        public void SubtractVectorNullVector()
        {
            // Arrange
            Vector a = MatrixVectorData.ArrangeVectorA_4_Column();

            // Act
            Vector.Subtract(null, a);
        }

        [TestMethod]
        [ExpectedException(typeof(NullVectorException))]
        public void SubtractVectorNullVector2()
        {
            // Arrange
            Vector a = MatrixVectorData.ArrangeVectorA_4_Column();

            // Act
            Vector.Subtract(a, null);
        }

        [TestMethod]
        [ExpectedException(typeof(UnevenMatricesException))]
        public void SubtractVectorUnevenVectors()
        {
            // Arrange
            Vector a = MatrixVectorData.ArrangeVectorA_4_Column();
            Vector b = MatrixVectorData.ArrangeVectorB_3_Column();

            // Act
            Vector.Subtract(a, b);
        }

        [TestMethod]
        [ExpectedException(typeof(VectorIncorrectAlignmentException))]
        public void SubtractVectorIncorrectAlignment()
        {
            // Arrange
            Vector a = MatrixVectorData.ArrangeVectorB_3_Column();
            Vector b = MatrixVectorData.ArrangeVectorD_3_Row();

            // Act
            Vector.Subtract(a, b);
        }

        [TestMethod]
        public void SubtractVector()
        {
            // Arrange
            Vector a = MatrixVectorData.ArrangeVectorA_4_Column();
            Vector b = MatrixVectorData.ArrangeVectorC_4_Row().Transpose();

            // Act
            Vector result = Vector.Subtract(a, b);

            // Assert
            Assert.AreEqual(45.7, result.GetValue(0));
            Assert.AreEqual((1000.05).ToString(), result.GetValue(1).ToString());
            Assert.AreEqual(-67.244, result.GetValue(2));
            Assert.AreEqual(-3799.9, result.GetValue(3));
        }

        #endregion Subtract Vectors

        #region Multiply

        [TestMethod]
        [ExpectedException(typeof(NullVectorException))]
        public void MultiplyVectorNullVector()
        {
            // Arrange
            Vector a = MatrixVectorData.ArrangeVectorC_4_Row();

            // Act
            Vector.Multiply(null, a);
        }

        [TestMethod]
        [ExpectedException(typeof(NullVectorException))]
        public void MultiplyVectorNullVector2()
        {
            // Arrange
            Vector a = MatrixVectorData.ArrangeVectorC_4_Row();

            // Act
            Vector.Multiply(a, null);
        }

        [TestMethod]
        [ExpectedException(typeof(UnevenMatricesException))]
        public void MultiplyVectorUnevenVector()
        {
            // Arrange
            Vector a = MatrixVectorData.ArrangeVectorA_4_Column();
            Vector b = MatrixVectorData.ArrangeVectorB_3_Column();

            // Act
            Vector.Multiply(a, b);
        }

        [TestMethod]
        [ExpectedException(typeof(VectorIncorrectAlignmentException))]
        public void MultiplyVectorIncorrectAlignment()
        {
            // Arrange
            Vector a = MatrixVectorData.ArrangeVectorB_3_Column();
            Vector b = MatrixVectorData.ArrangeVectorD_3_Row();

            // Act
            Vector.Multiply(a, b);
        }

        [TestMethod]
        public void MultiplyVector()
        {
            // Arrange
            Vector a = MatrixVectorData.ArrangeVectorA_4_Column();
            Vector b = MatrixVectorData.ArrangeVectorC_4_Row().Transpose();

            // Act
            Vector result = a.Multiply(b);

            // Assert
            Assert.AreEqual(46.7, result.GetValue(0));
            Assert.AreEqual(-50, result.GetValue(1));
            Assert.AreEqual(-2.9568, result.GetValue(2));
            Assert.AreEqual(800400, result.GetValue(3));
        }

        #endregion Multiply

        #region Get Value

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GetValueOutOfBounds()
        {
            // Arrange
            Vector a = MatrixVectorData.ArrangeVectorA_4_Column();

            // Act
            a.GetValue(5);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GetValueNegativeIndex()
        {
            // Arrange
            Vector a = MatrixVectorData.ArrangeVectorA_4_Column();

            // Act
            a.GetValue(-1);
        }

        [TestMethod]
        public void GetValue()
        {
            // Arrange
            Vector a = MatrixVectorData.ArrangeVectorC_4_Row();

            // Act
            double value = a.GetValue(2);

            // Assert
            Assert.AreEqual(67.2, value);
        }

        #endregion Get Value

        #region Set Value

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void SetValueOutOfBounds()
        {
            // Arrange
            Vector a = MatrixVectorData.ArrangeVectorD_3_Row();

            // Act
            a.SetValue(5, 2);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void SetValueNegativeIndex()
        {
            // Arrange
            Vector a = MatrixVectorData.ArrangeVectorD_3_Row();

            // Act
            a.SetValue(-2, 2);
        }

        [TestMethod]
        public void SetValue()
        {
            // Arrange
            Vector a = MatrixVectorData.ArrangeVectorD_3_Row();

            // Act
            a.SetValue(1, 4.34);

            // Assert
            Assert.AreEqual(4.34, a.GetValue(1));
        }

        #endregion Set Value

        #region Copy

        [TestMethod]
        public void Copy()
        {
            // Arrange
            Vector a = MatrixVectorData.ArrangeVectorC_4_Row();

            // Act
            Vector b = a.Copy();

            // Assert
            Assert.AreEqual(1, b.GetValue(0));
            Assert.AreEqual(-.05, b.GetValue(1));
            Assert.AreEqual(67.2, b.GetValue(2));
            Assert.AreEqual(4000, b.GetValue(3));
        }

        #endregion Copy

        #region Transpose

        [TestMethod]
        public void Transpose()
        {
            // Arrange
            Vector a = MatrixVectorData.ArrangeVectorB_3_Column();

            // Act
            Vector b = a.Transpose();

            // Assert
            Assert.AreEqual(2, b.GetValue(0));
            Assert.AreEqual(-1.64, b.GetValue(1));
            Assert.AreEqual(30000, b.GetValue(2));
            Assert.AreEqual(VectorType.Row, b.VectorType);
        }

        #endregion Transpose

        #region Operators

        [TestMethod]
        public void AddOperator()
        {
            // Arrange
            Vector a = MatrixVectorData.ArrangeVectorB_3_Column();
            Vector b = MatrixVectorData.ArrangeVectorB_3_Column();

            // Act
            Vector result = a.Add(b);

            // Assert
            Assert.AreEqual(4, result.GetValue(0));
            Assert.AreEqual(-3.28, result.GetValue(1));
            Assert.AreEqual(60000, result.GetValue(2));
        }

        [TestMethod]
        public void SubtractOperator()
        {
            // Arrange
            Vector a = MatrixVectorData.ArrangeVectorA_4_Column();
            Vector b = MatrixVectorData.ArrangeVectorC_4_Row().Transpose();

            // Act
            Vector result = Vector.Subtract(a, b);

            // Assert
            Assert.AreEqual(45.7, result.GetValue(0));
            Assert.AreEqual((1000.05).ToString(), result.GetValue(1).ToString());
            Assert.AreEqual(-67.244, result.GetValue(2));
            Assert.AreEqual(-3799.9, result.GetValue(3));
        }

        [TestMethod]
        public void MultiplyOperator()
        {
            // Arrange
            Vector a = MatrixVectorData.ArrangeVectorA_4_Column();
            Vector b = MatrixVectorData.ArrangeVectorC_4_Row().Transpose();

            // Act
            Vector result = a.Multiply(b);

            // Assert
            Assert.AreEqual(46.7, result.GetValue(0));
            Assert.AreEqual(-50, result.GetValue(1));
            Assert.AreEqual(-2.9568, result.GetValue(2));
            Assert.AreEqual(800400, result.GetValue(3));
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
