using System;
using System.Collections.Generic;
using System.Text;
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
    public Task<List<ProjectInfo>> GetProjects(ProjectFilter filter = null)
    {
      return Request<List<ProjectInfo>>("/projects" + (filter == null ? "" : filter.GenerateQueryString()));
    }
  }

  public class ProjectFilter
  {
      public long? ClientID { get; set; }
      public DateTime? UpdatedSince { get; set; }

      internal string GenerateQueryString()
      {
          return FilterHelper.ComposeQueryString<ProjectFilter>(
              this,
              new[]{
              new FilterHelper.QueryPart<ProjectFilter>("client", ClientID.HasValue, (f) => f.ClientID.ToString()),
              new FilterHelper.QueryPart<ProjectFilter>("updated_since", UpdatedSince.HasValue, (f) => f.UpdatedSince.Value.ToString()), 
          });
      }
  }

  internal static class FilterHelper
  {
      internal class QueryPart<T>{
          internal string QueryStringName { get; private set; }
          internal bool IsSpecified { get; private set; }
          internal Func<T, string> GetValueCallback { get; private set; }

          internal QueryPart(string queryStringName, bool isSpecified, Func<T,string> getValueCallback){
              QueryStringName = queryStringName;
              IsSpecified = isSpecified;
              GetValueCallback = getValueCallback;
          }
      }

      internal static string ComposeQueryString<T>(T filter, IEnumerable<QueryPart<T>> parts)
      {
          StringBuilder sb = new StringBuilder();
          foreach (var p in parts)
          {
              if (p.IsSpecified)
              {
                  sb.Append(sb.Length == 0 ? "?" : "&");
                  string val = p.GetValueCallback(filter);
                  sb.Append(p.QueryStringName+"=" + Uri.EscapeDataString(val));
              }
          }
          return sb.ToString();
      }
  }
}