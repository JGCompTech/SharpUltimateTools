using System;

namespace Microsoft.CSharp.Tools.Objects.HW
{
    /// <summary>
    /// Processor Object
    /// </summary>
    public class ProcessorObject
    {
        /// <summary>
        /// Processor Name
        /// </summary>
        public String Name { get; internal set; } = String.Empty;
        /// <summary>
        /// Number Of Processor Cores
        /// </summary>
        public Int32 Cores { get; internal set; } = 0;
    }
}