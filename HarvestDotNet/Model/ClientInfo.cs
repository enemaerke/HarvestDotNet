using Newtonsoft.Json;

namespace HarvestDotNet.Model
{
  public class ClientInfo
  {
    [JsonProperty(PropertyName = "client")]
    public Client Client { get; set; }
  }
}