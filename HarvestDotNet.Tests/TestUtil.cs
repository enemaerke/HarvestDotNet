using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HarvestDotNet.Tests
{
  public static class TestUtil
  {
    public static string GetResourceAsString(string resourcePath)
    {
      string thisNamespace = typeof (TestUtil).Namespace;
      using(var stream = typeof (TestUtil).Assembly.GetManifestResourceStream(thisNamespace + "." + resourcePath))
      {
        if (stream == null)
          throw new ApplicationException("Unable to locate the embedded resource at resource path: " + resourcePath);

        using(StreamReader reader = new StreamReader(stream, Encoding.UTF8))
        {
          return reader.ReadToEnd();
        }
      }
    }
  }

  public static class Extensions
  {
    public static string ToListString<T>(this IEnumerable<T> list, string separator = ",", Func<T,string> stringifier = null)
    {
      StringBuilder sb = new StringBuilder();
      if (list != null)
      {
        foreach (T v in list)
        {
          if (sb.Length > 0)
            sb.Append(separator);
          if (v == null)
            sb.Append("");
          else if (stringifier != null)
            sb.Append(stringifier(v));
          else
          {
            sb.Append(v.ToString());
          }
        }
      }
      return sb.ToString();
    }
  }
}

namespace System
{
  public class EventArgs<T> : EventArgs
  {
    public T Value { get; private set; }

    public EventArgs(T value)
    {
      Value = value;
    }
  }
}