using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace HarvestDotNet.Tests
{
  [TestFixture]
  public class ObjectResolverTests
  {
    [Test]
    public void CanResolveHarvestProject()
    {
      string payload = TestUtil.GetResourceAsString("Data.ExampleHarvestProject.xml");
      Project project = ObjectResolver.Resolve<Project>(payload);
      Assert.NotNull(project);

      Assert.AreEqual("Test project", project.Name);
    }
  }
}
