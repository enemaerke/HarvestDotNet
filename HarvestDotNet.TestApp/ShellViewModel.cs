using System;
using System.Threading.Tasks;
using Caliburn.Micro;
using HarvestDotNet.Model;
using Newtonsoft.Json;
using Action = Caliburn.Micro.Action;

namespace HarvestDotNet.TestApp
{
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

      DayEntryBriefInstance = new DayEntryBrief();
      TaskEntryInstance = new TaskEntry();
      Properties = new object[]
      {
        DayEntryBriefInstance,
        TaskEntryInstance
      };
      SelectedPropertyIndex = 0;

    }

    public int SelectedPropertyIndex { get; set; }

    public object[] Properties { get; private set; }

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
      set { m_outputAsJson = value; NotifyOfPropertyChange(()=> OutputAsJson); }
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
      Do(api => api.GetProjects());
    }

    public void GetSpecificProject()
    {
      Do(api =>api.GetProjectById(SelectedNumber));
    }

    public void GetToday()
    {
      Do(api => api.GetDay());
    }

    public void GetDayEntry()
    {
      Do(api =>api.GetDay(SelectedDate));
    }

    public void GetSpecificDayEntry()
    {
      Do(api =>api.GetDay(SelectedNumber));
    }

    public void ToggleTimer()
    {
      Do(api => api.ToggleTimer(SelectedNumber));
    }

    public void GetAccountStatus()
    {
      Do(api => api.GetAccountRateStatus());
    }

    private void Do<TOutput>(Func<HarvestApi,Task<TOutput>> action)
    {
      try
      {
        var settings = GetSettings();
        HarvestApi api = new HarvestApi(settings);
        var result = action(api);
        Output(result.Result); 
      }
      catch(Exception exception)
      {
        OutputAsJson = exception.Message + Environment.NewLine + exception.StackTrace;
      }
    }

    private void Output(object result)
    {
      string asJson = JsonConvert.SerializeObject(result, Formatting.Indented);
      OutputAsJson = asJson;

    }
  }
}