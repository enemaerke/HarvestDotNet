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
      HarvestProject harvestProject = ObjectResolver.Resolve<HarvestProject>(payload);
      Assert.NotNull(harvestProject);

      Assert.AreEqual("Test project", harvestProject.Name);
    }
  }
}
