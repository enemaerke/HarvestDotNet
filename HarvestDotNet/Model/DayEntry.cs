using System;
using Newtonsoft.Json;

namespace HarvestDotNet.Model
{
  public class DayEntry
  {
    [JsonProperty("id")]
    public int Id { get; set; }
    [JsonProperty("spent_at")]
    public DateTime SpentAt { get; set; }
    [JsonProperty("user_id")]
    public int UserId { get; set; }
    [JsonProperty("project_id")]
    public int ProjectId { get; set; }
    [JsonProperty("project")]
    public string Project { get; set; }
    [JsonProperty("client")]
    public string Client { get; set; }
    [JsonProperty("task_id")]
    public string TaskId { get; set; }
    [JsonProperty("task")]
    public string Task { get; set; }
    [JsonProperty("hours")]
    public float Hours { get; set; }
    [JsonProperty("notes")]
    public string Notes { get; set; }
    [JsonProperty("timer_started_at")]
    public DateTime? TimerStartedAt { get; set; }
    [JsonProperty("created_at")]
    public DateTime CreatedAt { get; set; }
    [JsonProperty("updated_at")]
    public DateTime UpdatedAt { get; set; }
  }
}