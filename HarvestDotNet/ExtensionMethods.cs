using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HarvestDotNet
{
  internal static class ExtensionMethods
  {
    internal static string ToFormat(this string formatString, params object[] objs)
    {
      if (formatString == null)
        return null;
      return string.Format(formatString, objs);
    }
  }
}
