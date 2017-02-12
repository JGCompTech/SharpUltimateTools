using System;

namespace JGCompTech.CSharp.Tools.HWInfo
{
    /// <summary>
    /// Network Information
    /// </summary>
    public static class Network
    {
        /// <summary>
        /// Returns the Internal IP Address.
        /// </summary>
        public static String InternalIPAddress
        {
            get
            {
                try
                {
                    var IP = String.Empty;
                    foreach (System.Net.IPAddress ip in System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList)
                    {
                        if (ip.AddressFamily.ToString() == "InterNetwork") IP = ip.ToString();
                    }
                    return IP;
                }
                catch (NullReferenceException)
                {
                    return String.Empty;
                }
            }
        }

        /// <summary>
        /// Returns the External IP Address by connecting to "http://api.ipify.org".
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public static String ExternalIPAddress(out String error)
        {
            var IP = String.Empty;
            error = String.Empty;
            try
            {
                using (System.Net.WebClient ipclient = new System.Net.WebClient())
                {
                    IP = ipclient.DownloadString("http://api.ipify.org");
                    return IP;
                }
            }
            catch (System.Net.WebException ex)
            {
                if (ex.Message == "The remote name could not be resolved: 'http://api.ipify.org'") { return IP; }
            }
            catch (Exception ex) { error = ex.Message; return IP; }
            return IP;
        }

        /// <summary>
        /// Returns status of internet connection
        /// </summary>
        public static Boolean ConnectionStatus
        {
            get
            {
                try
                {
                    using (var client = new System.Net.WebClient())
                    using (var stream = client.OpenRead("http://www.google.com"))
                    {
                        return true;
                    }
                }
                catch (Exception) { return false; }
            }
        }
    }
}
