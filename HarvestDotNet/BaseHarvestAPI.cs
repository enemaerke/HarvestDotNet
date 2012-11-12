using System;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
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
    private readonly MediaTypeFormatter m_formatter;

    private string GetBasicAuthenticationToken()
    {
      return Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format(CultureInfo.InvariantCulture,
          "{0}:{1}", m_settings.UserName, m_settings.Password)));
    }

    protected HarvestApi(HarvestApiSettings settings)
    {
      m_client = new HttpClient();
      m_settings = settings;

      m_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
      m_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", GetBasicAuthenticationToken());
      m_formatter = new JsonMediaTypeFormatter();
    }

    protected Task<T> Request<T>(string relativePath) where T : class
    {
      Uri completeUri = new Uri(m_settings.BaseUri, relativePath);
      return m_client.GetAsync(completeUri)
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
      Uri completeUri = new Uri(m_settings.BaseUri, relativePath);
      return m_client.PostAsync(completeUri.ToString(), data, m_formatter)
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
      Uri completeUri = new Uri(m_settings.BaseUri, relativePath);
      return m_client.DeleteAsync(completeUri.ToString())
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
        throw new HarvestThrottleException("Throttle limit reached", task.Result.Headers.RetryAfter.Delta);
      }
    }
  }
}