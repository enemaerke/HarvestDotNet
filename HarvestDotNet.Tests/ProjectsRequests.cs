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
      ProjectsApi api = new ProjectsApi(GetSettings());
      var projectsTask = api.GetProjects();
      using(var req = HttpServer.HandleRequest())
      {
        req.ResponseBodyText = Encoding.UTF8.GetString(Properties.Resources.AllProjects);
      }

      var projects = projectsTask.Result;
      Assert.AreEqual(9, projects.Count, "expecting the 9 projects");
      Assert.True(projects.All(x => !string.IsNullOrEmpty(x.Project.Name)));
      Assert.True(projects.All(x => x.Project.CreatedAt > new DateTime(2000, 1, 1)));
    }

    [Test]
    public void CanSpecifyProjectFilters()
    {
        ProjectsApi api = new ProjectsApi(GetSettings());
        var projectsTask = api.GetProjects(new ProjectFilter()
        {
            ClientID = 12,
        });

        using (var req = HttpServer.HandleRequest())
        {
            Assert.IsNotNullOrEmpty(req.RawRequest.Url.Query);
            req.ResponseBodyText = Encoding.UTF8.GetString(Properties.Resources.AllProjects);
        }
        var projects = projectsTask.Result;

    }
  }
}
