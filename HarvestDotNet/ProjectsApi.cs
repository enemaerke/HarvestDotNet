using System.Collections.Generic;
using System.Threading.Tasks;
using HarvestDotNet.Model;

namespace HarvestDotNet
{
  public class ProjectsApi : HarvestApi
  {
    public ProjectsApi(HarvestApiSettings settings) : base(settings)
    {
    }

    public Task<ProjectInfo> GetProjectById(int projectID)
    {
      return Request<ProjectInfo>("/projects/{0}".ToFormat(projectID));
    }
    public Task<List<ProjectInfo>> GetProjects()
    {
      return Request<List<ProjectInfo>>("/projects");
    }
  }
}