using Newtonsoft.Json;

namespace HarvestDotNet.Model
{
  public class InvoiceInfo
  {
    [JsonProperty(PropertyName = "invoices")]
    public Invoice Invoice { get; set; }
  }
}