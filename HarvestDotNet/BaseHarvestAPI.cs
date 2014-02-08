using System;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HarvestDotNet
{
  public class HarvestApi
  {
    private readonly HttpClient m_client;

    private string GetBasicAuthenticationToken(string userName, string password)
    {
      return Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format(CultureInfo.InvariantCulture,
          "{0}:{1}", userName, password)));
    }

    protected HarvestApi(HarvestApiSettings settings)
    {
      m_client = new HttpClient();
      m_client.BaseAddress = settings.BaseUri;

      m_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
      m_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", GetBasicAuthenticationToken(settings.UserName, settings.Password));
    }


    protected Task<T> Request<T>(string relativePath) where T : class
    {
      var req = new HttpRequestMessage(HttpMethod.Get, relativePath);
      return m_client.SendAsync(req)
        .ContinueWith(t =>
                        {
                          RaiseExceptionIfUnableToReach(t);
                          RaiseThrottleExceptionIfNeeded(t);
                          t.Result.EnsureSuccessStatusCode();

                          return t.Result.Content.ReadAsAsync<T>();
                        })
        .Unwrap();
    }

    protected Task<TReply> Post<TReply, TData>(string relativePath, TData data)
      where TReply : class
      where TData : class
    {
      var request = new HttpRequestMessage(HttpMethod.Post, relativePath)
      {
          Content = new StringContent(JsonConvert.SerializeObject(data), new UTF8Encoding(), "application/json")
      };
      return m_client.SendAsync(request)
      .ContinueWith(t =>
                      {
                          RaiseExceptionIfUnableToReach(t);
                          RaiseThrottleExceptionIfNeeded(t);
                          t.Result.EnsureSuccessStatusCode();
                          
                          return t.Result.Content.ReadAsAsync<TReply>();
                      })
      .Unwrap();
    }

    protected Task<bool> Delete(string relativePath)
    {
      var request = new HttpRequestMessage(HttpMethod.Delete, relativePath);
      return m_client.SendAsync(request)
        .ContinueWith(t =>
        {
          RaiseExceptionIfUnableToReach(t);
          RaiseThrottleExceptionIfNeeded(t);
          bool success = t.Result.IsSuccessStatusCode;

          return success.CreateTaskFromResult();
        })
        .Unwrap();
    }

    protected static void RaiseExceptionIfUnableToReach(Task<HttpResponseMessage> task)
    {
      if (task.IsFaulted)
        throw new HarvestConnectionException("Unable to connect to service", task.Exception);
    }

    protected static void RaiseThrottleExceptionIfNeeded(Task<HttpResponseMessage> task)
    {
      if (task.Result.StatusCode == HttpStatusCode.ServiceUnavailable)
      {
        //try read the "Retry-After" header to look for the number of seconds to wait for throttle to be lifted
        if (task.Result.Headers.RetryAfter != null)
            throw new HarvestThrottleException("Throttle limit reached", task.Result.Headers.RetryAfter.Delta);
        throw new HarvestThrottleException("Throttle limit reached");
      }
    }
  }
}