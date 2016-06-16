using System;
using CultureInfo = System.Globalization.CultureInfo;

namespace Microsoft.CSharp.Tools
{
    /// <summary>
    /// Allows command prompt command to be run hidden.
    /// </summary>
    public static class CommandInfo
    {
        /// <summary>
        /// Output object that is returned after the command has completed
        /// </summary>
        public class Output
        {
            /// <summary>
            /// Returns the text result of the command.
            /// </summary>
            public String Result { get; set; } = String.Empty;

            /// <summary>
            /// Returns the error if an error occurred.
            /// </summary>
            public String Error { get; set; } = String.Empty;

            /// <summary>
            /// Returns the exit code. Returns 0 if no error occurred.
            /// </summary>
            public Int32 ExitCode { get; set; } = 0;

            /// <summary>
            /// Returns the exception if an exception occurred.
            /// </summary>
            public String Exception { get; set; } = String.Empty;
        }

        /// <summary>
        /// Runs a command prompt command.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="returnOutput"></param>
        /// <returns></returns>
        public static Output Run(Object command, Boolean returnOutput)
        {
            var output = new Output();
            try
            {
                // create the ProcessStartInfo using "cmd" as the program to be run,
                // and "/c " as the parameters.
                // Incidentally, /c tells cmd that we want it to execute the command that follows,
                // and then exit.
                var procStartInfo = new System.Diagnostics.ProcessStartInfo("cmd", "/c " + command)
                {

                    // The following commands are needed to redirect the Standard output.
                    // This means that it will be redirected to the Process.StandardOutput StreamReader.
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    // Do not create the black window.
                    CreateNoWindow = true
                };

                if (returnOutput)
                {
                    // Now we create a process, assign its ProcessStartInfo and start it
                    using (System.Diagnostics.Process proc = new System.Diagnostics.Process())
                    {
                        proc.StartInfo = procStartInfo;
                        proc.Start();
                        // Get the output into a strings
                        output.Result = proc.StandardOutput.ReadToEnd();
                        output.Error = proc.StandardError.ReadToEnd();
                        output.ExitCode = proc.ExitCode;
                    }
                }
                else
                {
                    // Now we create a process, assign its ProcessStartInfo and start it
                    using (System.Diagnostics.Process proc = new System.Diagnostics.Process())
                    {
                        proc.StartInfo = procStartInfo;
                        proc.Start();
                    }
                }
            }
            catch (Exception objException) { output.Exception = Convert.ToString(objException, CultureInfo.CurrentCulture); }

            return output;
        }
    }
}
