using System;
using System.IO;
using System.Net;

namespace JGCompTech.CSharp.Tools.WebTools
{
    /// <summary>
    /// Allows download of JSON from Websites.
    /// </summary>
    public static class Json
    {
        /// <summary>
        /// Returns a string of the JSON request
        /// </summary>
        /// <param name="url">API URL</param>
        /// <param name="CurrentToken">API Security Token</param>
        /// <returns></returns>
        public static String GetJson(String url, String CurrentToken)
        {
            if (url.IsNotNullOrEmpty())
            {
                if (url.IsValidUrl())
                {
                    return GetJson(new Uri(url), CurrentToken);
                }
            }
            throw new ArgumentException("URL string is invalid!");
        }

        /// <summary>
        /// Returns a string of the JSON request
        /// </summary>
        /// <param name="url">API URL</param>
        /// <param name="currentToken">API Security Token</param>
        /// <returns></returns>
        public static String GetJson(Uri url, String currentToken)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + currentToken);
                request.ContentType = "application/json";
                request.Method = "GET";

                var httpResponse = (HttpWebResponse)request.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    return streamReader.ReadToEnd();
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
