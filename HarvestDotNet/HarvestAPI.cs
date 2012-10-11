using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using HarvestDotNet.Model;

namespace HarvestDotNet
{
  public class HarvestApi
  {
    private readonly HttpClient m_client;
    private readonly HarvestApiSettings m_settings;

    private string GetBasicAuthenticationToken()
    {
      return Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format(CultureInfo.InvariantCulture,
          "{0}:{1}", m_settings.UserName, m_settings.Password)));
    }

    public HarvestApi(HarvestApiSettings settings)
    {
      m_client = new HttpClient();
      m_settings = settings;

      m_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
      m_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", GetBasicAuthenticationToken());
    }

    public Task<ProjectInfo> GetProjectById(int projectID)
    {
      return Request<ProjectInfo>("/projects/{0}".ToFormat(projectID));
    }
    public Task<List<ProjectInfo>> GetProjects()
    {
      return Request<List<ProjectInfo>>("/projects");
    }

    public Task<DayInformation> GetDay()
    {
      return Request<DayInformation>("/daily");
    }

    public Task<DayInformation> GetDay(DateTime day)
    {
      return Request<DayInformation>("/daily/{0}/{1}".ToFormat(day.DayOfYear, day.Year));
    }

    public Task<DayEntry> GetDay(int dayEntryId)
    {
      return Request<DayEntry>("/daily/show/{0}".ToFormat(dayEntryId));
    }

    public Task<DayEntry> ToggleTimer(int dayEntryId)
    {
      return Request<DayEntry>("/daily/timer/{0}".ToFormat(dayEntryId));
    }

    private Task<T> Request<T>(string relativePath) where T:class
    {
      Uri completeUri = new Uri(m_settings.BaseUri, relativePath);
      return m_client.GetAsync(completeUri)
        .ContinueWith(t =>
                        {
                          RaiseThrottleExceptionIfNeeded(t);
                          t.Result.EnsureSuccessStatusCode();

                          return t.Result.Content.ReadAsAsync<T>();
                        })
        .Unwrap();
    }

    private static void RaiseThrottleExceptionIfNeeded(Task<HttpResponseMessage> task)
    {
      if (task.Result.StatusCode == HttpStatusCode.ServiceUnavailable)
            throw new HarvestThrottleException("Throttle limit reached");
    }
  }
}