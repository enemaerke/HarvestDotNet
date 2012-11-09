using System;
using System.Runtime.Serialization;

namespace HarvestDotNet
{
  /// <summary>
  /// Exception signalling that the throttle limit for the Harvest API has been reached
  /// </summary>
  [Serializable]
  public class HarvestConnectionException : Exception
  {
    public HarvestConnectionException(){}
    public HarvestConnectionException(string message):base(message){}
    public HarvestConnectionException(string message, Exception inner):base(message, inner){}
    protected HarvestConnectionException(SerializationInfo info, StreamingContext context):base(info,context){}
  }

  
  /// <summary>
  /// Exception signalling that the Harvest service could not be reached
  /// </summary>
  [Serializable]
  public class HarvestThrottleException : Exception
  {
    public TimeSpan? ThrottleLiftedAfter { get; private set; }
    public HarvestThrottleException(){}
    public HarvestThrottleException(string message):base(message){}
    public HarvestThrottleException(string message, TimeSpan? throttleLifted) : base(message)
    {
      ThrottleLiftedAfter = throttleLifted;
    }
    protected HarvestThrottleException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
      long ticks = info.GetInt64("ThrottleLifted");
      this.ThrottleLiftedAfter = new TimeSpan(ticks);
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
      {
        throw new ArgumentNullException("info");
      }

      if (this.ThrottleLiftedAfter.HasValue)
        info.AddValue("ThrottleLifted", this.ThrottleLiftedAfter.Value.Ticks);

      // MUST call through to the base class to let it save its own state
      base.GetObjectData(info, context);
    }
  }
}