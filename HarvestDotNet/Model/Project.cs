using System;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace HarvestDotNet.Model
{
  public class Project
  {
    [JsonProperty(PropertyName = "active")]
    public bool Active { get; set; }
    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; }
    [JsonProperty(PropertyName = "created_at")]
    public DateTime CreatedAt { get; set; }
    [JsonProperty(PropertyName = "latest_record_at")]
    public DateTime LatestRecordAt { get; set; }
  }

  public class ProjectInfo
  {
    [JsonProperty(PropertyName = "project")]
    public Project Project { get; set; }
  }
}
