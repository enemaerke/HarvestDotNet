using System;
using Newtonsoft.Json;

namespace HarvestDotNet.Model
{
  public class DayInformation
  {
    [JsonProperty("for_day")]
    public DateTime Day { get; set; }
    [JsonProperty("day_entries")]
    public DayEntry[] DayEntries { get; set; }
    [JsonProperty("projects")]
    public ProjectEntry[] ProjectEntries { get; set; }
  }
}