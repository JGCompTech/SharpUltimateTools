using System;

namespace JGCompTech.CSharp.Tools.HWInfo.Objects
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
        public int Cores { get; internal set; } = 0;
    }
}