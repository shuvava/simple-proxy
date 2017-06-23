using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace web.api._01.Middlewares
{
    public class HttpRequestHeaderMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly HttpRequestHeaderMiddlewareOptions _options;

        public HttpRequestHeaderMiddleware(
            RequestDelegate next,
            IOptions<HttpRequestHeaderMiddlewareOptions> options)
        {
            _next = next;
            _options = options.Value;
        }

        public async Task Invoke(HttpContext context)
        {

            if (!IsHandlebale(context, out string url))
            {
                await _next.Invoke(context).ConfigureAwait(false);
            }

            await ProcessRequestAsync(url, context).ConfigureAwait(false);
        }

        private static string BuildUri(string url, HttpContext context)
        {
            var path = context.Request.Path.ToString().TrimStart('/');
            var querystring = string.IsNullOrWhiteSpace(context.Request.QueryString.ToString())
                                    ? string.Empty
                                    : $"?{context.Request.QueryString}";
            var uri = $"{url.TrimEnd('/')}/{path}{querystring}";
            return uri;
        }

        private bool IsHandlebale(HttpContext context, out string url)
        {
            var path = context.Request.Path.ToString().Trim('/');
            url = null;
            foreach (var header in _options.Headers)
            {
                var prefixed = !header.Prefixes.Any() || header.Prefixes
                                                               .Select(p => p.TrimStart('/'))
                                                               .Any(p => path.StartsWith(p, StringComparison.CurrentCultureIgnoreCase));
                var suffixed = !header.Suffixes.Any() || header.Suffixes
                                                               .Select(p => p.TrimEnd('/'))
                                                               .Any(p => path.EndsWith(p, StringComparison.CurrentCultureIgnoreCase));
                if (!prefixed || !suffixed) 
                {
                    continue;
                }
                url = header.Url;
                return true;
            }
            return false;
        }

        private async Task ProcessRequestAsync(string url, HttpContext context )
        {
            var uri = BuildUri(url, context);
        }
    }
}

