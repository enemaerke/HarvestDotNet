using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HarvestDotNet.Model;

namespace HarvestDotNet
{
  public class InvoicesApi : HarvestApi
  {
    public InvoicesApi(HarvestApiSettings settings)
          : base(settings)
    {
    }

    public Task<InvoiceInfo> GetInvoiceById(int invoiceID)
    {
        return Request<InvoiceInfo>("/invoices/{0}".ToFormat(invoiceID));
    }
    public Task<List<InvoiceInfo>> GetInvoices(InvoiceFilter filter = null)
    {
        return Request<List<InvoiceInfo>>("/invoices" + (filter == null ? "" : filter.GenerateQueryString()));
    }
  }

  public class InvoiceFilter
  {
    public long? ClientID { get; set; }
    public InvoiceStatus InvoiceStatus { get; set; }
    public DateTime? UpdatedSince { get; set; }

    internal string GenerateQueryString()
    {
      return FilterHelper.ComposeQueryString<InvoiceFilter>(
        this,
        new[]{
          new FilterHelper.QueryPart<InvoiceFilter>("client", ClientID.HasValue, (f) => f.ClientID.ToString()),
          new FilterHelper.QueryPart<InvoiceFilter>("status", InvoiceStatus != HarvestDotNet.InvoiceStatus.None, (f) => f.InvoiceStatus.ToString().ToLower()),
          new FilterHelper.QueryPart<InvoiceFilter>("updated_since", UpdatedSince.HasValue, (f) => f.UpdatedSince.Value.ToString()), 
       });
    }
  }

  public enum InvoiceStatus
  {
    None,
    Open,
    Partial,
    Draft,
    Paid,
    Unpaid,
    PastDue
  }
}