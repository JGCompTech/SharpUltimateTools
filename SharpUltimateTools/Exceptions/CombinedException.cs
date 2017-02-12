using System;
using System.Linq;

namespace JGCompTech.CSharp.Exceptions
{
    /// <summary>
    /// 	Generic exception for combining several other exceptions
    /// </summary>
    [Serializable]
    public class CombinedException : Exception
    {
        /// <summary>
        /// 	Initializes a new instance of the <see cref = "CombinedException" /> class.
        /// </summary>
        /// <param name = "message">The message.</param>
        /// <param name = "innerExceptions">The inner exceptions.</param>
        public CombinedException(String message, Exception[] innerExceptions) : base(message)
        {
            InnerExceptions = innerExceptions;
        }

        /// <summary>
        /// 	Gets the inner exceptions.
        /// </summary>
        /// <value>The inner exceptions.</value>
        public Exception[] InnerExceptions { get; protected set; }

        /// <summary>
        /// Combines the specified exceptions.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerExceptions"></param>
        /// <returns></returns>
        public static Exception Combine(string message, params Exception[] innerExceptions)
        {
            if (innerExceptions.Length == 1)
                return innerExceptions[0];

            return new CombinedException(message, innerExceptions);
        }
        /// <summary>
        /// Combines the specified exceptions.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerExceptions">The inner exceptions.</param>
        /// <returns></returns>
        public static Exception Combine(string message, System.Collections.Generic.IEnumerable<Exception> innerExceptions)
        {
            return Combine(message, innerExceptions.ToArray());
        }
    }
}
