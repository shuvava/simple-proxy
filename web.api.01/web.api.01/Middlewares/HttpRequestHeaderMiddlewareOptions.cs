using System;
using System.Collections.Generic;

namespace web.api._01.Middlewares
{
	public class HttpRequestHeaderMiddlewareOptions
    {
        public IList<HttpRequestHeader> Headers { get; } = new List<HttpRequestHeader>();

    }
}
