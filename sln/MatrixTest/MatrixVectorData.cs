using Matrices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixTest
{
    public static class MatrixVectorData
    {
        /// <summary>
        /// Obtains a specific 4 by 4 matrix called Matrix A.
        /// </summary>
        /// <returns></returns>
        internal static Matrix ArrangeMatrixA_4_4()
        {
            Matrix a = new Matrix(4, 4);
            a.SetValue(0, 0, 4.2);
            a.SetValue(1, 0, 8);
            a.SetValue(2, 0, double.PositiveInfinity);
            a.SetValue(3, 0, double.NegativeInfinity);
            a.SetValue(0, 1, 1.1);
            a.SetValue(1, 1, 9);
            a.SetValue(2, 1, 0);
            a.SetValue(3, 1, -0);
            a.SetValue(0, 2, 6.8);
            a.SetValue(1, 2, 12.4);
            a.SetValue(2, 2, -7);
            a.SetValue(3, 2, double.NaN);
            a.SetValue(0, 3, 17.14);
            a.SetValue(1, 3, 6.67);
            a.SetValue(2, 3, 14);
            a.SetValue(3, 3, 85);

            return a;
        }

        /// <summary>
        /// Obtains a specific 4 by 4 matrix called Matrix B.
        /// </summary>
        /// <returns></returns>
        internal static Matrix ArrangeMatrixB_4_4()
        {
            Matrix b = new Matrix(4, 4);
            b.SetValue(0, 0, -24);
            b.SetValue(1, 0, 3.45);
            b.SetValue(2, 0, 6);
            b.SetValue(3, 0, 9000);
            b.SetValue(0, 1, 0);
            b.SetValue(1, 1, 4.24);
            b.SetValue(2, 1, 2.34);
            b.SetValue(3, 1, 12);
            b.SetValue(0, 2, double.NaN);
            b.SetValue(1, 2, 2);
            b.SetValue(2, 2, .07);
            b.SetValue(3, 2, 14);
            b.SetValue(0, 3, double.NegativeInfinity);
            b.SetValue(1, 3, 1.12);
            b.SetValue(2, 3, 0.0000);
            b.SetValue(3, 3, 6.666666);

            return b;
        }

        /// <summary>
        /// Obtains a specific 4 by 2 matrix called Matrix C.
        /// </summary>
        /// <returns></returns>
        internal static Matrix ArrangeMatrixC_4_2()
        {
            Matrix c = new Matrix(4, 2);
            c.SetValue(0, 0, 0);
            c.SetValue(1, 0, -1.17);
            c.SetValue(2, 0, 2);
            c.SetValue(3, 0, 6);
            c.SetValue(0, 1, -34);
            c.SetValue(1, 1, 400);
            c.SetValue(2, 1, double.NaN);
            c.SetValue(3, 1, 1200);
            return c;
        }

        /// <summary>
        /// Obtains a specific 3 by 3 matrix called Matrix D.
        /// </summary>
        /// <returns></returns>
        internal static Matrix ArrangeMatrixD_3_3()
        {
            Matrix d = new Matrix(3, 3);
            d.SetValue(0, 0, .42);
            d.SetValue(1, 0, -100);
            d.SetValue(2, 0, 21.44);
            d.SetValue(0, 1, 0);
            d.SetValue(1, 1, 1.34);
            d.SetValue(2, 1, -56.7);
            d.SetValue(0, 2, .008);
            d.SetValue(1, 2, 13);
            d.SetValue(2, 2, 15.55);
            return d;
        }

        /// <summary>
        /// Obtains a specific 3 by 3 matrix called Matrix E.
        /// </summary>
        /// <returns></returns>
        internal static Matrix ArrangeMatrixE_3_3()
        {
            Matrix e = new Matrix(3, 3);
            e.SetValue(0, 0, .5666);
            e.SetValue(1, 0, 7667);
            e.SetValue(2, 0, 23);
            e.SetValue(0, 1, 12.09909);
            e.SetValue(1, 1, 34);
            e.SetValue(2, 1, -3);
            e.SetValue(0, 2, -42.04);
            e.SetValue(1, 2, 2);
            e.SetValue(2, 2, -.06);
            return e;
        }

        /// <summary>
        /// Obtains a specific vector (column) with height 4.
        /// </summary>
        /// <returns></returns>
        internal static Vector ArrangeVectorA_4_Column()
        {
            Vector a = new Vector(4, VectorType.Column);
            a.SetValue(0, 46.7);
            a.SetValue(1, 1000);
            a.SetValue(2, -.044);
            a.SetValue(3, 200.1);
            return a;
        }

        /// <summary>
        /// Obtains a specific vector (column) with height 3.
        /// </summary>
        /// <returns></returns>
        internal static Vector ArrangeVectorB_3_Column()
        {
            Vector b = new Vector(3, VectorType.Column);
            b.SetValue(0, 2);
            b.SetValue(1, -1.64);
            b.SetValue(2, 30000);
            return b;
        }

        /// <summary>
        /// Obtains a specific vector (row) with width 4.
        /// </summary>
        /// <returns></returns>
        internal static Vector ArrangeVectorC_4_Row()
        {
            Vector c = new Vector(4, VectorType.Row);
            c.SetValue(0, 1);
            c.SetValue(1, -.05);
            c.SetValue(2, 67.2);
            c.SetValue(3, 4000);
            return c;
        }

        /// <summary>
        /// Obtains a specific vector (row) with width 3.
        /// </summary>
        /// <returns></returns>
        internal static Vector ArrangeVectorD_3_Row()
        {
            Vector d = new Vector(3, VectorType.Row);
            d.SetValue(0, 0);
            d.SetValue(1, 0);
            d.SetValue(2, 0);
            return d;
        }
    }
}
