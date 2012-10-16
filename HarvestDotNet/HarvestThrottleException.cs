using System;
using System.Runtime.Serialization;

namespace HarvestDotNet
{
  /// <summary>
  /// Exception signalling that the throttle limit for the Harvest API has been reached
  /// </summary>
  [Serializable]
  public class HarvestThrottleException : Exception
  {
    public HarvestThrottleException(){}
    public HarvestThrottleException(string message):base(message){}
    public HarvestThrottleException(string message, Exception inner):base(message, inner){}
    protected HarvestThrottleException(SerializationInfo info, StreamingContext context):base(info,context){}
  }
}