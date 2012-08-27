using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace HarvestDotNet
{
  internal static class ObjectResolver
  {
    internal static TType Resolve<TType>(string input)
    {
      byte[] bytes = Encoding.UTF8.GetBytes(input);
      MemoryStream mem = new MemoryStream(bytes);
      XmlSerializer ser = new XmlSerializer(typeof(TType));
      return (TType)ser.Deserialize(mem);
    }

    internal static bool TryResolve<TType>(string input, out TType value)
    {
      value = default(TType);
      try
      {
        value = Resolve<TType>(input);
        return true;
      }
      catch
      {
        return false;
      }
    }
  }
}
