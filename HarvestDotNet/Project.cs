using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace HarvestDotNet
{
  [XmlRoot("project")]
  public class Project
  {
    [XmlElement("name")]
    public string Name { get; set; }
  }
}
