using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace HarvestDotNet
{
  [XmlRoot("project")]
  public class Project
  {
    [XmlElement("name")]
    [JsonProperty("name")]
    public string Name { get; set; }
  }
}
