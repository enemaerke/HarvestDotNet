using Newtonsoft.Json;

namespace HarvestDotNet.Model
{
  public class ProjectInfo
  {
    [JsonProperty(PropertyName = "project")]
    public Project Project { get; set; }
  }
}