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
    [JsonProperty(PropertyName = "earlisest_record_at")]
    public DateTime EarliestRecordAt { get; set; }
    [JsonProperty(PropertyName = "updated_at")]
    public DateTime UpdatedAt { get; set; }
    [JsonProperty(PropertyName = "client_id")]
    public int ClientID { get; set; }
    [JsonProperty(PropertyName = "id")]
    public int ID { get; set; }
    [JsonProperty(PropertyName = "notes")]
    public string Note { get; set; }
  }
}
