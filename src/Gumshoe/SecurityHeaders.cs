using System;
using System.Collections.Generic;

namespace Gumshoe
{
  public class SecurityHeaders
  {
    public IDictionary<string, string> AddHeaders { get; } = new Dictionary<string, string>();

    public ISet<string> RemoveHeaders { get; } = new HashSet<string>();
  }
}