using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace HarvestDotNet.Tests
{
  [TestFixture]
  public class ProjectsRequests : ApiTestBase
  {
    [Test]
    public void CanReadProjects()
    {
      HarvestApi api = GetStandardApi();
      var projectsTask = api.GetProjects();
      using(var req = HttpServer.HandleRequest())
      {
        req.ResponseBodyText = Encoding.UTF8.GetString(Properties.Resources.AllProjects);
      }

      var projects = projectsTask.Result;
      Assert.AreEqual(9, projects.Count, "expecting the 9 projects");
    }
  }
}
