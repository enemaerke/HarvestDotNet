using System;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace HarvestDotNet.Model
{
  public class Client
  {
    [JsonProperty(PropertyName = "id")]
    public long ID { get; set; }
    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; }
    [JsonProperty(PropertyName = "created_at")]
    public DateTime DateCreated { get; set; }
    [JsonProperty(PropertyName = "updated_at")]
    public DateTime? DateUpdated { get; set; }
    [JsonProperty(PropertyName = "currency")]
    public string Currency { get; set; }
    [JsonProperty(PropertyName = "currency_symbol")]
    public string CurrencySymbol { get; set; }
    [JsonProperty(PropertyName = "details")]
    public string Details { get; set; }
    [JsonProperty(PropertyName = "active")]
    public bool Active { get; set; }
    [JsonProperty(PropertyName = "cache_version")]
    public long? CacheVersion { get; set; }
    [JsonProperty(PropertyName = "highrise_id")]
    public int? HighriseID { get; set; }
    [JsonProperty(PropertyName = "statement_key")]
    public string StatementKey { get; set; }
    [JsonProperty(PropertyName = "default_invoice_timeframe")]
    public string DefaultInvoiceTimeframe { get; set; }
    [JsonProperty(PropertyName = "last_invoice_kind")]
    public string LastInvoiceKind { get; set; }
  }
}
