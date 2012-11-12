using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using Caliburn.Micro;
using HarvestDotNet.Model;
using Newtonsoft.Json;

namespace HarvestDotNet.TestApp
{
  [Export(typeof(ShellViewModel))]
  public class ShellViewModel : PropertyChangedBase
  {
    private const string RegistryPath = @"SOFTWARE\HarvestDotNet\TestApp";

    public RegistryProperty<string> BaseUri { get; private set; }
    public RegistryProperty<string> UserName { get; private set; }
    public RegistryProperty<string> Password { get; private set; }

    public ShellViewModel()
    {
      BaseUri = new RegistryProperty<string>(RegistryPath, "BaseUri", "", s => s, s => s);
      UserName = new RegistryProperty<string>(RegistryPath, "UserName", "", s => s, s => s);
      Password = new RegistryProperty<string>(RegistryPath, "Password", "", s => s, s => s);
      SelectedDate = DateTime.Now;

      DayEntryBriefInstance = new DayEntryBrief()
                                {
                                  SpentAt = DateTime.Now,
                                  Notes = string.Empty,
                                };
      TaskEntryInstance = new TaskEntry();
      Properties = new object[]
      {
        DayEntryBriefInstance,
        TaskEntryInstance
      };
      SelectedPropertyIndex = 0;
      IsBusy = false;
    }

    public int SelectedPropertyIndex { get; set; }

    public object[] Properties { get; private set; }

    private bool m_isBusy;
    public bool IsBusy
    {
      get { return m_isBusy; }
      set { m_isBusy = value; NotifyOfPropertyChange(() => IsBusy); }
    }

    private DateTime m_selectedDate;
    public DateTime SelectedDate
    {
      get { return m_selectedDate; }
      set { m_selectedDate = value; NotifyOfPropertyChange(() => SelectedDate); }
    }

    private int m_selectedNumber;
    public int SelectedNumber
    {
      get { return m_selectedNumber; }
      set { m_selectedNumber = value; NotifyOfPropertyChange(() => SelectedNumber); }
    }

    public DayEntryBrief DayEntryBriefInstance { get; set; }
    public TaskEntry TaskEntryInstance { get; set; }

    private string m_outputAsJson;
    public string OutputAsJson
    {
      get { return m_outputAsJson; }
      set { m_outputAsJson = value; NotifyOfPropertyChange(() => OutputAsJson); }
    }

    private HarvestApiSettings GetSettings()
    {
      return new HarvestApiSettings()
               {
                 BaseUri = new Uri(BaseUri.Value),
                 UserName = UserName.Value,
                 Password = Password.Value
               };
    }

    public void GetProjects()
    {
      Do<ProjectsApi,List<ProjectInfo>>(api => api.GetProjects());
    }

    public void GetSpecificProject()
    {
      Do<ProjectsApi, ProjectInfo>(api => api.GetProjectById(SelectedNumber));
    }

    public void GetToday()
    {
      Do<TimeTrackingApi, DayInformation>(api => api.GetDay());
    }

    public void GetDayEntry()
    {
      Do<TimeTrackingApi, DayInformation>(api => api.GetDay(SelectedDate));
    }

    public void GetSpecificDayEntry()
    {
      Do<TimeTrackingApi, DayEntry>(api => api.GetDay(SelectedNumber));
    }

    public void ToggleTimer()
    {
      Do<TimeTrackingApi, DayEntry>(api => api.ToggleTimer(SelectedNumber));
    }

    public void GetAccountStatus()
    {
      Do<AccountApi, AccountRateStatus>(api => api.GetAccountRateStatus());
    }
    public void WhoAmI()
    {
      Do<AccountApi, AccountRateStatus>(api => api.WhoAmI());
    }

    public void CreateDayEntry()
    {
      Do<TimeTrackingApi, DayEntry>(api => api.CreateDayEntry(DayEntryBriefInstance));
    }

    public void DeleteDayEntry()
    {
      Do<TimeTrackingApi, bool>(api => api.DeleteDayEntry(SelectedNumber));
    }

    private void Do<TApiType, TOutput>(Func<TApiType, Task<TOutput>> action) where TApiType : class
    {
      try
      {
        IsBusy = true;
        var settings = GetSettings();
        TApiType api = Activator.CreateInstance(typeof (TApiType), settings) as TApiType;
        var result = action(api);
        Output(result.Result);
      }
      catch (Exception exception)
      {
        OutputAsJson = exception.Message + Environment.NewLine + exception.StackTrace;
      }
      finally
      {
        IsBusy = false;
      }
    }

    private void Output(object result)
    {
      string asJson = JsonConvert.SerializeObject(result, Formatting.Indented);
      OutputAsJson = asJson;
    }
  }
}