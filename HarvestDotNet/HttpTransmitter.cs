using System;
using System.IO;
using System.Net;
using System.Text;

namespace HarvestDotNet
{
  internal class HttpTransmitter
  {
    public Result<string> ProcessRequest(string uri, Credentials credentials)
    {
      try
      {
        HttpWebRequest request = WebRequest.Create(uri) as HttpWebRequest;
        if (request == null)
          return Result<string>.Failure("The uri '{0}' is not in a valid http format".ToFormat(uri));

        request.AllowAutoRedirect = true;
        request.MaximumAutomaticRedirections = 1;

        // accept and contenttype set to signal api call to the server
        request.Accept = "application/xml";
        request.ContentType = "application/xml";
        request.UserAgent = "HarvestDotNet";

        //using basic authentication
        request.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(new ASCIIEncoding().GetBytes(credentials.BasicAuthenticationFormat)));

        using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
        {
          if (response != null && request.HaveResponse)
          {
            using (StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
              string payload = sr.ReadToEnd();
              return Result<string>.Success(payload);
            }
          }

          return Result<string>.Success(string.Empty);
        }
      }
      catch (WebException webEx)
      {
        if (webEx.Response != null && webEx.Response is HttpWebResponse)
        {
          using (HttpWebResponse errorResponse = (HttpWebResponse) webEx.Response)
          {
            return
              Result<string>.Failure("Failed to request data from '{0}'. HttpStatusCode={1}".ToFormat(uri,
                                                                                                      errorResponse.
                                                                                                        StatusDescription));
          }
        }
        return Result<string>.Failure("Failed to request data from '{0}: {1}".ToFormat(uri, webEx.Message));
      }
      catch (Exception ex)
      {
        return Result<string>.Failure("Failed to request data from '{0}': {1}".ToFormat(uri, ex.Message));
      }
    }
  }

  internal class Credentials
  {
    public string UserName { get; set; }
    public string Password { get; set; }

    public string BasicAuthenticationFormat { get { return UserName + ":" + Password; } }
  }

  internal class Result<T>
  {
    internal T Value {get; private set;} 
    internal bool IsSuccess {get; private set;}
    internal string Message {get; private set;}

    internal static Result<T> Success<T>(T value)
    {
      return new Result<T>
               {
                 IsSuccess = true,
                 Value = value,
                 Message = string.Empty,
               };
    }

    internal static Result<T> Failure(string errorMessage)
    {
      return new Result<T>
               {
                 IsSuccess = false,
                 Value = default(T),
                 Message = errorMessage,
               };
    }
  }
}
