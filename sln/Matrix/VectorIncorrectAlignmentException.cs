using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrices
{
    /// <summary>
    /// An error that is thrown when you try to perform an operation with a vector that is aligned (column vs row) incorrectly.
    /// </summary>
    public class VectorIncorrectAlignmentException : Exception
    {
        /// <summary>
        /// A constructor that passes a message to the inner exception.
        /// </summary>
        /// <param name="message"></param>
        public VectorIncorrectAlignmentException(string message)
            : base(message)
        {

        }
    }
}
