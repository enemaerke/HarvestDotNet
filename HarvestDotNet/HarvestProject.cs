using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace HarvestDotNet
{
  [XmlRoot("project")]
  public class HarvestProject
  {
    [XmlElement("name")]
    public string Name { get; set; }
  }
}
