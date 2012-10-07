using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace HarvestDotNet
{
  internal static class ObjectResolver
  {
    internal static readonly CultureInfo s_datetimeCulture = new CultureInfo("en-us");
    internal static readonly string s_longDateTimeFormat = "ddd, d MMM yyyy HH:mm:ss zzz";
    internal static readonly string s_shortDateTimeFormat = "ddd, d MMM yyyy"; 

    internal static DateTime ParseXmlDateTimeLong(string datetime)
    {
      datetime = datetime.Replace("\n", "").Trim();
      return DateTime.ParseExact(datetime, s_longDateTimeFormat, s_datetimeCulture);
    }

    internal static string ToXmlDateTimeLong(DateTime dateTime)
    {
      return dateTime.ToString(s_longDateTimeFormat, s_datetimeCulture);
    }

    internal static DateTime ParseXmlDateTimeShort(string datetime)
    {
      datetime = datetime.Replace("\n", "").Trim();
      return DateTime.ParseExact(datetime, s_shortDateTimeFormat, s_datetimeCulture);
    }

    internal static string ToXmlDateTimeShort(DateTime dateTime)
    {
      return dateTime.ToString(s_shortDateTimeFormat, s_datetimeCulture); 
    }


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
