using System;
using Newtonsoft.Json;

namespace HarvestDotNet.Model
{
  public class AccountRateStatus
  {
    [JsonProperty("count")]
    public int Count { get; set; }
    [JsonProperty("last_access_at")]
    public DateTime LastAccessedAt { get; set; }
    [JsonProperty("lockout_seconds")]
    public int LockoutSeconds { get; set; }
    [JsonProperty("max_calls")]
    public int MaxCalls { get; set; }
    [JsonProperty("timeframe_limit")]
    public int TimeFrameLimit { get; set; }
  }
}