using System;
using System.Collections.Generic;

namespace web.api._01.Middlewares
{
    /// <summary>
    /// This represents the option item entity for HTTP request header.
    /// </summary>
	public class HttpRequestHeader
    {
        /// <summary>
        /// Gets or sets the URL to send the request.
        /// </summary>
        public string Url { get; set; }
        public IList<string> Prefixes { get; } = new List<string>();
        public IList<string> Suffixes { get; } = new List<string>();
        public IList<(string Key, string Value)> Headers = new List<(string Key, string Value)>();



    }
}
