using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace HarvestDotNet.Tests.TestSupport
{
  /// <summary>
  /// Class hosting an internal loopback webserver used for testing
  /// in-memory
  /// </summary>
  public class TestHttpServer : IDisposable
  {
    private bool m_isRunning;
    private readonly object m_lock = new object();
    private readonly ManualResetEvent m_requestEvent = new ManualResetEvent(false);
    private readonly Queue<RequestContext> m_requestQueue = new Queue<RequestContext>();
    private Exception m_requestException;
    public int ReceiveTimeout { get; set; }
    public int SendTimeout { get; set; }
    public int RequestBeginCount { get; private set; }
    public int RequestEndCount { get; private set; }
    public event EventHandler<EventArgs<RequestContext>> RequestHandler;
    private readonly HttpListener m_listener;
    private readonly int m_port;

    public TestHttpServer(int minPort = 3025, int maxPort = 40000)
    {
      ReceiveTimeout = 10000;
      SendTimeout = 10000;
      RequestBeginCount = 0;
      RequestEndCount = 0;

      if (minPort > maxPort)
        throw new ArgumentException("minPort can not be larger than maxPort");
      if (minPort < IPEndPoint.MinPort || minPort > IPEndPoint.MaxPort)
        throw new ArgumentException(string.Format("Must be in the interval from {0} to {1}", IPEndPoint.MinPort, IPEndPoint.MaxPort), "minPort");
      if (maxPort < IPEndPoint.MinPort || maxPort > IPEndPoint.MaxPort)
        throw new ArgumentException(string.Format("Must be in the interval from {0} to {1}", IPEndPoint.MinPort, IPEndPoint.MaxPort), "maxPort");

      var rnd = new Random();
      for (var i = 0; i < 10; i++)
      {
        try
        {
          int port = rnd.Next(minPort, maxPort);
          m_listener = new HttpListener();
          m_listener.Prefixes.Add(string.Format("http://*:{0}/", port));
          m_listener.Start();

          m_port = port;
          break;
        }
        catch (Exception ex)
        {
          m_listener = null;
        }
      }

      m_isRunning = true;
      ThreadPool.QueueUserWorkItem(o => AsyncAcceptContexts());
      // cannot use Threads, must use ThreadPool to avoid error on XP:
      // http://www.alexendris.com/Blog/index.php/2011/03/the-io-operation-has-been-aborted-because-of-either-a-thread-exit-or-an-application-request-exception-in-windows-xp-workaround/
    }

    private static IPAddress s_localIp;
    private static IPAddress GetLocalIp()
    {
      if (s_localIp == null)
      {
        try
        {
          s_localIp = IPAddress.Loopback;
          var result = Dns.GetHostEntry(s_localIp);
          s_localIp = result.AddressList.First(x => x.AddressFamily == AddressFamily.InterNetwork); //just testing ipv4 here
        }
        catch (Exception)
        {
          s_localIp = IPAddress.Loopback;
        }
      }
      return s_localIp;
    }

    public Uri BaseUri
    {
      get
      {
        return new Uri(string.Format("http://{0}:{1}", GetLocalIp(), m_port));
      }
    }

    private void AsyncAcceptContexts()
    {
      if (m_isRunning)
      {
        try
        {
          m_listener.BeginGetContext(r =>
          {
            try
            {
              //exist early
              if (!m_isRunning)
                return;

              HttpListenerContext ctx = m_listener.EndGetContext(r);
              RequestContext reqContext = new RequestContext(ctx);

              if (RequestHandler != null)
              {
                using (reqContext)
                {
                  RequestHandler(this, new EventArgs<RequestContext>(reqContext));
                }
              }
              else
              {
                lock (m_lock)
                {
                  m_requestQueue.Enqueue(reqContext);
                }
                RequestEndCount++;
                m_requestEvent.Set();
              }
            }
            catch (Exception exc)
            {
              if (m_isRunning)
              {
                Console.WriteLine("HttpListener exception (not part of disposing):" +
                                  exc.Message);
                if (m_requestException == null)
                  m_requestException = exc;
              }
            }

            if (m_isRunning)
              AsyncAcceptContexts(); //run loop again
          }, null);
        }
        catch (Exception exc)
        {
          Console.WriteLine("Failed to begin async http listening:" + exc.Message);
        }
      }
    }

    public void Dispose()
    {
      m_isRunning = false;
      if (m_listener != null)
      {
        m_listener.Stop(); //stop receiving requests...
        m_listener.Close(); //close the listener completely
      }
    }

    public bool WaitForRequests(int count)
    {
      return WaitForRequests(count, 1000);
    }

    public bool WaitForRequests(int count, int timeout)
    {
      var endTime = DateTime.Now.AddMilliseconds(timeout);
      while (RequestEndCount < count)
      {
        var time = DateTime.Now;
        if (time >= endTime)
          return false;
        if (!m_requestEvent.WaitOne((int)(endTime - time).TotalMilliseconds))
          return false;
      }
      if (m_requestException != null)
        throw RethrowWithNoStackTraceLoss(m_requestException);
      return true;
    }

    private static Exception RethrowWithNoStackTraceLoss(Exception ex)
    {
      var f = typeof(Exception).GetField("_remoteStackTraceString", BindingFlags.Instance | BindingFlags.NonPublic);
      if (f != null)
        f.SetValue(ex, ex.StackTrace);
      return ex;
    }

    public RequestContext HandleRequest(int timeout = 20000, bool verbose = false)
    {
      var endTime = DateTime.Now.AddMilliseconds(timeout);
      RequestContext req = null;
      while (true)
      {
        lock (m_lock)
        {
          if (m_requestQueue.Count > 0)
          {
            req = m_requestQueue.Dequeue();
            break;
          }
          m_requestEvent.Reset();
        }
        var time = DateTime.Now;
        if (time >= endTime || !m_requestEvent.WaitOne((int)(endTime - time).TotalMilliseconds))
          break;
      }
      if (m_requestException != null)
        throw RethrowWithNoStackTraceLoss(m_requestException);
      if (req != null)
      {
        req.Verbose = verbose;
        return req;
      }

      throw new TimeoutException("Waited for " + timeout + " milliseconds for a request");
    }
  }
}
