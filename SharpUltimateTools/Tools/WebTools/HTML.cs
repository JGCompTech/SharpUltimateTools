using System;
using System.IO;
using System.Net;

namespace JGCompTech.CSharp.Tools.WebTools
{
    /// <summary>
    /// Allows download of HTML from Websites.
    /// </summary>
    public static class Html
    {
        /// <summary>
        /// Returns a string of the HTML of the specified URL String.
        /// </summary>
        /// <param name="url"></param>
        public static String GetHtml(String url)
        {
            if (url.IsNotNullOrEmpty() && url.IsValidUrl())
            {
                return GetHtml(new Uri(url));
            }
            throw new ArgumentException("URL string is invalid!");
        }

        /// <summary>
        /// Returns a string of the HTML of the specified Uri object.
        /// </summary>
        /// <param name="url"></param>
        public static String GetHtml(Uri url)
        {
            try
            {
                //Create request For given url
                var response = ((HttpWebRequest)WebRequest.Create(url)).GetResponse();

                //Take response stream
                using (var sr = new StreamReader(response.GetResponseStream()))
                {
                    //Read response stream (html code)
                    return sr.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("The remote Server returned an error: (404) Not Found")) { return "404"; }
                return ex.Message;
            }
        }
    }
}
