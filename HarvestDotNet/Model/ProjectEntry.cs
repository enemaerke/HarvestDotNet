using Newtonsoft.Json;

namespace HarvestDotNet.Model
{
  public class ProjectEntry
  {
    [JsonProperty("name")]
    public string Name { get; set; }
    [JsonProperty("id")]
    public int ID { get; set; }
    [JsonProperty("client")]
    public string Client { get; set; }
    [JsonProperty("client_id")]
    public string ClientID { get; set; }
    [JsonProperty("tasks")]
    public TaskEntry[] Tasks { get; set; }
  }
}