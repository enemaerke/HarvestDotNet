using System;
using Microsoft.Win32;

namespace HarvestDotNet.TestApp
{
  public class RegistryProperty<T>
  {
    private readonly string m_regKeyPath;
    private readonly string m_propertyName;
    private readonly T m_defaultValue;
    private readonly Func<T, string> m_toString;
    private readonly Func<string, T> m_parseString;

    public RegistryProperty(
      string regKeyPath, 
      string propertyName,
      T defaultValue,
      Func<T,string> toString, 
      Func<string,T> parseString)
    {
      m_regKeyPath = regKeyPath;
      m_propertyName = propertyName;
      m_defaultValue = defaultValue;
      m_toString = toString;
      m_parseString = parseString;
    }

    public T Value
    {
      get
      {
        using(var key = GetKey())
        {
          return m_parseString(key.GetValue(m_propertyName, m_toString(m_defaultValue)) as string);
        }
      }
      set
      {
        using(var key = GetKey())
        {
          key.SetValue(m_propertyName, m_toString(value));
        }
      }
    }

    private RegistryKey GetKey()
    {
      return Registry.CurrentUser.CreateSubKey(m_regKeyPath);
    }
  }
}