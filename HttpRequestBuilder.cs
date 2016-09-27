﻿using System;
using System.Net;
using System.Text;

namespace Mmosoft
{
    public class HttpRequestBuilder
    {
        private static HttpWebRequest CreateRequest(Uri uri, CookieContainer cookies = null)
        {
            var request = WebRequest.Create(uri) as HttpWebRequest;
            request.Accept = "*/*";
            request.AllowAutoRedirect = false;
            if (cookies != null) request.CookieContainer = cookies;
            request.Headers.Add("Accept-Language", "en-US,en;q=0.5");
            request.Headers.Add("Accept-Encoding", "gzip");
            request.KeepAlive = true;
            request.Host = "m.facebook.com";
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64; rv:46.0) Gecko/20100101 Firefox/46.0";
            request.ProtocolVersion = HttpVersion.Version11;
            request.Timeout = 10000;            
            return request;
        }

        public static HttpWebRequest RequestPost(string url, CookieContainer cookies = null)
        {
            var request = CreateRequest(new Uri(url), cookies);
            request.Method = WebRequestMethods.Http.Post;
            request.ContentType = "application/x-www-form-urlencoded";
            return request;
        }

        public static HttpWebRequest RequestGet(string url, CookieContainer cookies = null)
        {
            var request = CreateRequest(new Uri(url), cookies);
            request.Method = WebRequestMethods.Http.Get;
            return request;
        }

        public static HttpWebResponse PostData(string url, string data, CookieContainer cookies = null)
        {            
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            var request = RequestPost(url, cookies);
            request.ContentLength = buffer.Length;
            using(var requestStream = request.GetRequestStream())
            {
                requestStream.Write(buffer, 0, buffer.Length);                
            }
            return request.GetResponse() as HttpWebResponse;
        }
    }
}
