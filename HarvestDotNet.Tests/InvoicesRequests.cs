using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace HarvestDotNet.Tests
{
  [TestFixture]
  public class InvoicesRequests : ApiTestBase
  {
    [Test]
    public void CanReadInvoices()
    {
      InvoicesApi api = new InvoicesApi(GetSettings());
      var invoiceTask = api.GetInvoices();
      using(var req = HttpServer.HandleRequest())
      {
        req.ResponseBodyText = Encoding.UTF8.GetString(Properties.Resources.AllInvoices);
      }

      var invoices = invoiceTask.Result;
      Assert.AreEqual(5, invoices.Count, "expecting the 5 invoices");
      Assert.True(invoices.All(x => !string.IsNullOrEmpty(x.Invoice.InvoiceNumber)));
      Assert.True(invoices.All(x => x.Invoice.DateCreated > new DateTime(2000, 1, 1)));
    }

    [Test]
    public void CanSpecifyInvoiceFilters()
    {
        InvoicesApi api = new InvoicesApi(GetSettings());
        var invoiceTask = api.GetInvoices(new InvoiceFilter()
        {
            InvoiceStatus = InvoiceStatus.Open,
        });

        using (var req = HttpServer.HandleRequest())
        {
            Assert.IsNotNullOrEmpty(req.RawRequest.Url.Query);
            req.ResponseBodyText = Encoding.UTF8.GetString(Properties.Resources.AllInvoices);
        }

        var invoices = invoiceTask.Result;
    }
  }
}
