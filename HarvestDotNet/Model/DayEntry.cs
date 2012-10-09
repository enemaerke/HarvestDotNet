using System;
using System.Xml.Serialization;

namespace HarvestDotNet.Model
{
  [XmlRoot("day_entry")]
  public class DayEntry
  {
    [XmlElement("id")]
    public int Id { get; set; }
    [XmlElement("spent_at", DataType = "date")]
    public DateTime SpentAt { get; set; }
    [XmlElement("user_id")]
    public int UserId { get; set; }
    [XmlElement("project_id")]
    public int ProjectId { get; set; }
    [XmlElement("project")]
    public string Project { get; set; }
    [XmlElement("client")]
    public string Client { get; set; }
    [XmlElement("task_id")]
    public string TaskId { get; set; }
    [XmlElement("task")]
    public string Task { get; set; }
    [XmlElement("hours", DataType = "float")]
    public float Hours { get; set; }
    [XmlElement("notes")]
    public string Notes { get; set; }

    [XmlElement("timer_started_at", IsNullable = true)]
    public string _innerTimerStarted
    {
      get { return TimerStarted == null ? null : ObjectResolver.ToXmlDateTimeLong(TimerStarted.Value); }
      set { TimerStarted = value == null ? (DateTime?)null : ObjectResolver.ParseXmlDateTimeLong(value); }
    }
    
    [XmlIgnore]
    public DateTime? TimerStarted { get; set; }
  }

  [XmlRoot("daily")]
  public class DayInformation
  {
    [XmlElement("for_day")]
    public string _forDay
    {
      get { return ObjectResolver.ToXmlDateTimeShort(Day); }
      set { Day = ObjectResolver.ParseXmlDateTimeShort(value); }
    }

    [XmlIgnore]
    public DateTime Day { get; set; }

    [XmlArray("day_entries")]
    [XmlArrayItem(ElementName = "day_entry", Type = typeof(DayEntry))]
    public DayEntry[] DayEntries { get; set; }
  }

  [XmlRoot("day_entries", IsNullable = false)]
  public class DayEntries
  {
    [XmlArray("day_entries")]
    [XmlArrayItem(ElementName = "day_entry", Type = typeof(DayEntry))]
    public DayEntry[] Entries { get; set; }
  }
}