using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gumshoe
{
  public class SecurityHeadersBuilder
  {
    private readonly SecurityHeaders securityHeaders;

    public SecurityHeadersBuilder()
    {
      securityHeaders = new SecurityHeaders();
    }

    public SecurityHeadersBuilder AddDefaultHeaders()
    {
      AddContentSecurityPolicySelf();
      AddContentTypeNoSniff();
      AddFrameOptionsDeny();
      AddStrictTransportPolicy();
      AddXssProtection();

      RemoveServerHeader();

      return this;
    }

    public SecurityHeadersBuilder AddHeader(string header, string value)
    {
      if (!securityHeaders.AddHeaders.ContainsKey(header))
      {
        securityHeaders.AddHeaders.Add(header, value);
      }

      return this;
    }

    public SecurityHeadersBuilder RemoveHeader(string header)
    {
      if (!securityHeaders.RemoveHeaders.Contains(header))
      {
        securityHeaders.RemoveHeaders.Add(header);
      }
      return this;
    }

    public SecurityHeadersBuilder AddContentSecurityPolicySelf()
    {
      return AddHeader("Content-Security-Policy", "default-src 'self'");
    }

    public SecurityHeadersBuilder AddContentTypeNoSniff()
    {
      return AddHeader("X-Content-Type", "nosniff");
    }

    public SecurityHeadersBuilder AddFrameOptionsDeny()
    {
      return AddHeader("X-Frame-Options", "DENY");
    }

    public SecurityHeadersBuilder AddFrameOptionsSameOrigin()
    {
      return AddHeader("X-Frame-Options", "SAMEORIGIN");
    }

    public SecurityHeadersBuilder AddFrameOptionsAllowFrom(string uri)
    {
      return AddHeader("X-Frame-Options", $"ALLOWFROM {uri}");
    }

    public SecurityHeadersBuilder AddStrictTransportPolicy(int maxAge = 31536000, bool includeSubdomains = true, bool preload = true)
    {
      return AddHeader("Strict-Transport-Security", $"max-age={maxAge}; {(includeSubdomains ? "includeSubdomains;" : "")} {(preload ? "preload;" : "")}");
    }

    public SecurityHeadersBuilder AddXssProtection()
    {
      return AddHeader("X-XSS-Protection", "1; mode=block");
    }

    public SecurityHeadersBuilder RemoveServerHeader()
    {
      return RemoveHeader("Server");
    }

    public SecurityHeaders Build()
    {
      return securityHeaders;
    }
  }
}