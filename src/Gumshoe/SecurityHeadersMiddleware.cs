using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gumshoe
{
  public static class SecurityHeadersMiddlewareExtensions
  {
    public static IApplicationBuilder UseGumshoe(
      this IApplicationBuilder builder,
      SecurityHeaders securityHeaders)
    {
      return builder.UseMiddleware<SecurityHeadersMiddleware>(securityHeaders);
    }
  }

  public sealed class SecurityHeadersMiddleware
  {
    private readonly RequestDelegate next;
    private readonly SecurityHeaders securityHeaders;

    public SecurityHeadersMiddleware(
      RequestDelegate next,
      SecurityHeaders securityHeaders)
    {
      this.next = next;
      this.securityHeaders = securityHeaders;
    }

    public async Task Invoke(HttpContext context)
    {
      context.Response.OnStarting(() =>
      {
        if (securityHeaders.AddHeaders.Any())
        {
          Parallel.ForEach(securityHeaders.AddHeaders, header => context.Response.Headers.Add(header.Key, header.Value));
        }

        if (securityHeaders.RemoveHeaders.Any())
        {
          Parallel.ForEach(securityHeaders.RemoveHeaders, header => context.Response.Headers.Remove(header));
        }

        return Task.CompletedTask;
      });

      await next(context);
    }
  }
}