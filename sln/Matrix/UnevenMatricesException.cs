using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrices
{
    /// <summary>
    /// An exception caused by performing an operation that requires equivelantly sized matrices on inquevelant matrices.
    /// </summary>
    public class UnevenMatricesException : Exception
    {
        /// <summary>
        /// A constructor that passes a message to the inner exception.
        /// </summary>
        /// <param name="message"></param>
        public UnevenMatricesException(string message)
            : base(message)
        {

        }
    }
}
