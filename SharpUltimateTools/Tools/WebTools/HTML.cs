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
            var newUri = new Uri(url);
            return GetHtml(newUri);
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
                var request = (HttpWebRequest)WebRequest.Create(url);

                //Create response-object
                var response = (HttpWebResponse)request.GetResponse();

                //Take response stream
                using (var sr = new StreamReader(response.GetResponseStream()))
                {
                    //Read response stream (html code)
                    var html = sr.ReadToEnd();

                    //return source
                    return html;
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
