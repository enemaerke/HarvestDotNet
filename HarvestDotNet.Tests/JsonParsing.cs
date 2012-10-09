﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HarvestDotNet.Model;
using Newtonsoft.Json;
using NUnit.Framework;

namespace HarvestDotNet.Tests
{
  [TestFixture]
  public class JsonParsing
  {
    [Test]
    public void CanParseProjects()
    {
      byte[] data = Properties.Resources.AllProjects;
      var projects = JsonConvert.DeserializeObject<List<ProjectInfo>>(Encoding.UTF8.GetString(data));
      Assert.AreEqual(9, projects.Count);

      Assert.True(projects.Any((x => x.Project.Active)));
      Assert.True(projects.All(x => !string.IsNullOrEmpty(x.Project.Name)));
      Assert.True(projects.All(x => x.Project.CreatedAt > new DateTime(2000,1,1)));
    }
  }
}
