using System;
using System.Linq;
using System.Runtime.Serialization;

namespace JGCompTech.CSharp.Exceptions
{
    /// <summary>
    /// Returns an exception if the database file is not found.
    /// </summary>
    [Serializable]
    public class DatabaseFileNotFoundException : Exception
    {
        // The default constructor needs to be defined explicitly now since it would be gone otherwise.
        /// <summary>
        /// Database Not Found Exception
        /// </summary>
        public DatabaseFileNotFoundException() { }
        /// <summary>
        /// Database Not Found Exception
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public DatabaseFileNotFoundException(String message, Exception ex) { }
        /// <summary>
        /// Database Not Found Exception
        /// </summary>
        /// <param name="message"></param>
        public DatabaseFileNotFoundException(String message) : base(message) { }
        /// <summary>
        /// Database Not Found Exception
        /// </summary>
        /// <param name="info"></param>
        /// <param name="content"></param>
        protected DatabaseFileNotFoundException(SerializationInfo info, StreamingContext content) { }
    }
}
