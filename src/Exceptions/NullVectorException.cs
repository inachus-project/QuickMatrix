using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrices
{
    /// <summary>
    /// An error thrown when a null vector is inputted in a spot where a vector with a value is expected.
    /// </summary>
    public class NullVectorException : Exception
    {
        /// <summary>
        /// A constructor that passes a message to the inner exception.
        /// </summary>
        /// <param name="message"></param>
        public NullVectorException(string message)
            : base(message)
        {

        }
    }
}
