using System;
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
}