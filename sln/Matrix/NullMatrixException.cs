using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrices
{
    /// <summary>
    /// An exception caused by passing a null matrix into a method that requires not null matrices.
    /// </summary>
    public class NullMatrixException : Exception
    {
        /// <summary>
        /// A constructor that passes a message to the inner exception.
        /// </summary>
        /// <param name="message"></param>
        public NullMatrixException(string message)
            : base(message)
        {

        }
    }
}
