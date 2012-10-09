using System;
using System.Runtime.Serialization;

namespace HarvestDotNet
{
  [Serializable]
  public class HarvestThrottleException : Exception
  {
    public HarvestThrottleException(){}
    public HarvestThrottleException(string message):base(message){}
    public HarvestThrottleException(string message, Exception inner):base(message, inner){}
    protected HarvestThrottleException(SerializationInfo info, StreamingContext context):base(info,context){}
  }
}