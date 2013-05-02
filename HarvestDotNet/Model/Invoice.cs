using System;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace HarvestDotNet.Model
{
  public class Invoice
  {
    [JsonProperty(PropertyName = "id")]
    public int ID { get; set; }
    [JsonProperty(PropertyName = "amount")]
    public decimal Amount { get; set; }
    [JsonProperty(PropertyName = "due_amount")]
    public decimal AmountDue { get; set; }
    [JsonProperty(PropertyName = "due_at")]
    public DateTime DueDate { get; set; }
    [JsonProperty(PropertyName = "due_at_human_format")]
    public string DueDateDescription { get; set; }
    [JsonProperty(PropertyName = "issued_at")]
    public DateTime DateIssued { get; set; }
    [JsonProperty(PropertyName = "created_at")]
    public DateTime DateCreated { get; set; }
    [JsonProperty(PropertyName = "period_start")]
    public DateTime? PeriodStart { get; set; }
    [JsonProperty(PropertyName = "period_end")]
    public DateTime? PeriodEnd { get; set; }
    [JsonProperty(PropertyName = "updated_at")]
    public DateTime DateUpdated { get; set; }
    [JsonProperty(PropertyName = "number")]
    public string InvoiceNumber { get; set; }
    [JsonProperty(PropertyName = "state")]
    public string Status { get; set; }
    [JsonProperty(PropertyName = "notes")]
    public string Notes { get; set; }
    [JsonProperty(PropertyName = "client_id")]
    public long ClientID { get; set; }
    [JsonProperty(PropertyName = "client_key")]
    public string ClientKey { get; set; }
    [JsonProperty(PropertyName = "created_by_id")]
    public int? CreatedByID { get; set; }
    [JsonProperty(PropertyName = "currency")]
    public string Currency { get; set; }
    [JsonProperty(PropertyName = "discount_amount")]
    public decimal DiscountAmount { get; set; }
    [JsonProperty(PropertyName = "discount")]
    public decimal? Discount { get; set; }
    [JsonProperty(PropertyName = "estimate_id")]
    public int? EstimateID { get; set; }
    [JsonProperty(PropertyName = "purchase_order")]
    public string PurchaseOrder { get; set; }
    [JsonProperty(PropertyName = "recurring_invoice_id")]
    public int? RecurringInvoiceID { get; set; }
    [JsonProperty(PropertyName = "retainer_id")]
    public int? RetainerID { get; set; }
    [JsonProperty(PropertyName = "subject")]
    public string Subject { get; set; }
    [JsonProperty(PropertyName = "tax")]
    public decimal? Tax { get; set; }
    [JsonProperty(PropertyName = "tax2")]
    public decimal? Tax2 { get; set; }
    [JsonProperty(PropertyName = "tax_amount")]
    public decimal TaxAmount { get; set; }
    [JsonProperty(PropertyName = "tax2_amount")]
    public decimal TaxAmount2 { get; set; }
  }
}
