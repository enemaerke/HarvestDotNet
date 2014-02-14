using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace HarvestDotNet.Tests
{
  [TestFixture]
  public class ClientsRequests : ApiTestBase
  {
    [Test]
    public void CanReadClients()
    {
      ClientsApi api = new ClientsApi(GetSettings());
      var clientTask = api.GetClients();
      using(var req = HttpServer.HandleRequest())
      {
        req.ResponseBodyText = Encoding.UTF8.GetString(Properties.Resources.AllClients);
      }

      var clients = clientTask.Result;
      Assert.AreEqual(4, clients.Count, "expecting the 4 clients");
      Assert.True(clients.All(x => !string.IsNullOrEmpty(x.Client.Name)));
      Assert.True(clients.All(x => x.Client.DateCreated > new DateTime(2000, 1, 1)));
    }

    [Test]
    public void CanSpecifyClientFilters()
    {
        ClientsApi api = new ClientsApi(GetSettings());
        var clientTask = api.GetClients(new ClientFilter()
        {
            UpdatedSince = new DateTime(2000, 1, 1)
        });

        using (var req = HttpServer.HandleRequest())
        {
            Assert.IsNotNullOrEmpty(req.RawRequest.Url.Query);
            req.ResponseBodyText = Encoding.UTF8.GetString(Properties.Resources.AllClients);
        }

        var clients = clientTask.Result;
    }
  }
}
