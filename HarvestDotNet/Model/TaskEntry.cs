using Newtonsoft.Json;

namespace HarvestDotNet.Model
{
  public class TaskEntry
  {
    [JsonProperty("id")]
    public int ID { get; set; }
    [JsonProperty("name")]
    public string Name { get; set; }
  }
}