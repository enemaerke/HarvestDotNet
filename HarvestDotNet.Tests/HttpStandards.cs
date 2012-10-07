using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace HarvestDotNet.Tests
{
  [TestFixture]
  public class HttpStandards : ApiTestBase
  {
    [Test]
    public void UsesBasicAuthentication()
    {
      var settings = new HarvestApiSettings()
                       {
                         BaseUri = HttpServer.BaseUri,
                         Password = "MyPassword",
                         UserName = "MyUser"
                       };
      HarvestApi api = new HarvestApi(settings);

      var projectsTask = api.GetProjects();
      using(var req = HttpServer.HandleRequest(verbose:true))
      {
        var authenticationHeader = req.RawRequest.Headers["Authorization"];
        Assert.NotNull(authenticationHeader);
      }
    }

    [Test]
    public void UsesJson()
    {
      var settings = new HarvestApiSettings()
      {
        BaseUri = HttpServer.BaseUri,
        Password = "MyPassword",
        UserName = "MyUser"
      };
      HarvestApi api = new HarvestApi(settings);

      var projectsTask = api.GetProjects();
      using (var req = HttpServer.HandleRequest(verbose: true))
      {
        Assert.True(req.RawRequest.AcceptTypes.Contains("application/json"), "accept types: " + req.RawRequest.AcceptTypes.ToListString());
        req.ResponseBodyText = Encoding.UTF8.GetString(Properties.Resources.AllProjects);
      }
      
    }
  }
}
