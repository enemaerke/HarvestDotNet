using System;
using System.Threading.Tasks;
using HarvestDotNet.Model;

namespace HarvestDotNet
{
  public class TimeTrackingApi : HarvestApi
  {
    public TimeTrackingApi(HarvestApiSettings settings) : base(settings)
    {
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


    public Task<DayEntry> CreateDayEntry(DayEntryBrief entryBrief)
    {
      return Post<DayEntry, DayEntryBrief>("/daily/add", entryBrief);
    }

    public Task<bool> DeleteDayEntry(long dayEntryId)
    {
      return Delete("/daily/delete/{0}".ToFormat(dayEntryId));
    }
  }
}