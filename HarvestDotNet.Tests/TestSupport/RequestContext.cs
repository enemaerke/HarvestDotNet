using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;

namespace HarvestDotNet.Tests.TestSupport
{
  public class RequestContext : IDisposable
  {
    private readonly HttpListenerContext m_context;
    public RequestContext(HttpListenerContext context)
    {
      ResponseHeaders = new Dictionary<string, string>();
      m_context = context;

      ResponseHeaders.Add("Server", "TestHttpServer/1.0");
      ResponseHeaders.Add("Content-Type", "application/json");

      ResponseBody = new MemoryStream();
      TimeReceived = DateTime.Now;
      StatusCode = HttpStatusCode.OK;

      using (StreamReader sr = new StreamReader(m_context.Request.InputStream, RequestEncoding))
      {
        RequestBodyText = sr.ReadToEnd();
      }
    }

    public void Dispose()
    {
      foreach (var h in ResponseHeaders)
        m_context.Response.Headers[h.Key] = h.Value;

      m_context.Response.StatusCode = (int)StatusCode;
      byte[] body = ResponseBody.ToArray();

      if (Verbose)
      {
        Console.WriteLine(RawRequest.Url.ToString());
        foreach (var h in RawRequest.Headers.AllKeys)
          Console.WriteLine("{0} : {1}", h, RawRequest.Headers[h]);
        Console.WriteLine();
        Console.WriteLine(Encoding.UTF8.GetString(body));
      }

      m_context.Response.OutputStream.Write(body, 0, body.Length);
      m_context.Response.OutputStream.Close();
      m_context.Response.Close();
    }

    public HttpStatusCode StatusCode { get; set; }
    public DateTime TimeReceived { get; private set; }
    public string RequestMethod { get { return m_context.Request.HttpMethod; } }
    public string RequestPath { get { return m_context.Request.Url.LocalPath; } }

    public Exception Exception { get; set; }

    public Encoding RequestEncoding { get { return Encoding.UTF8; } }

    public string RequestBodyText { get; private set; }

    private XmlDocument m_requestBodyXml;
    public XmlDocument RequestBodyXml
    {
      get
      {
        if (m_requestBodyXml == null)
        {
          m_requestBodyXml = new XmlDocument();
          try
          {
            m_requestBodyXml.LoadXml(RequestBodyText);
          }
          catch (XmlException exc)
          {
            var str = "Could not parse the request body to xml: " + exc.Message + ". (headercontentlength=" +
                      ContentLengthFromHeader + "): " + RequestBodyText;
            var temp = "";
            foreach (char t in str)
            {
              temp += t;
              if (temp.Length > 1024) { Debug.WriteLine(temp); temp = ""; }
            }
            if (temp.Length > 0) Debug.WriteLine(temp);

            throw new InvalidOperationException(str);
          }
        }
        return m_requestBodyXml;
      }
    }
    public Dictionary<string, string> ResponseHeaders { get; set; }
    public Encoding ResponseEncoding { get { return Encoding.UTF8; } }
    public MemoryStream ResponseBody
    {
      get;
      private set;
    }
    public string ResponseBodyText
    {
      set
      {
        var buffer = ResponseEncoding.GetBytes(value);
        ResponseBody.Write(buffer, 0, buffer.Length);
        ResponseBody.Flush();
      }
    }

    public int ContentLengthFromHeader { get; set; }

    public HttpListenerRequest RawRequest
    {
      get { return m_context.Request; }
    }

    public bool Verbose { get; set; }
  }
}