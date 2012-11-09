using System;
using Newtonsoft.Json;

namespace HarvestDotNet.Model
{
  public class DayEntry : DayEntryBrief
  {
    [JsonProperty("id")]
    public int Id { get; set; }
    [JsonProperty("user_id")]
    public int UserId { get; set; }
    [JsonProperty("project")]
    public string Project { get; set; }
    [JsonProperty("client")]
    public string Client { get; set; }
    [JsonProperty("task")]
    public string Task { get; set; }
    [JsonProperty("timer_started_at")]
    public DateTime? TimerStartedAt { get; set; }
    [JsonProperty("created_at")]
    public DateTime CreatedAt { get; set; }
    [JsonProperty("updated_at")]
    public DateTime UpdatedAt { get; set; }
  }

  public class DayEntryBrief
  {
    [JsonProperty("notes")]
    public string Notes { get; set; }
    [JsonProperty("hours", NullValueHandling = NullValueHandling.Ignore)]
    public float? Hours { get; set; }
    [JsonProperty("project_id")]
    public int ProjectId { get; set; }
    [JsonProperty("task_id")]
    public string TaskId { get; set; }
    [JsonProperty("spent_at")]
    public DateTime SpentAt { get; set; }
  }
}