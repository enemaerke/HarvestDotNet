using System;
using Caliburn.Micro;
using HarvestDotNet.Model;
using Newtonsoft.Json;

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
      HarvestApiSettings settings = GetSettings();
      HarvestApi api = new HarvestApi(settings);
      var projects = api.GetProjects();
      Output(projects.Result);
    }

    public void GetSpecificProject()
    {
      var settings = GetSettings();
      HarvestApi api = new HarvestApi(settings);
      var project = api.GetProjectById(SelectedNumber);
      Output(project.Result);
    }

    public void GetToday()
    {
      var settings = GetSettings();
      HarvestApi api = new HarvestApi(settings);
      var entry = api.GetDay();
      Output(entry.Result);
    }

    public void GetDayEntry()
    {
      var settings = GetSettings();
      HarvestApi api = new HarvestApi(settings);
      var entry = api.GetDay(SelectedDate);
      Output(entry.Result);
    }

    public void GetSpecificDayEntry()
    {
      var settings = GetSettings();
      HarvestApi api = new HarvestApi(settings);
      var entry = api.GetDay(SelectedNumber);
      Output(entry.Result);      
    }

    private void Output(object result)
    {
      string asJson = JsonConvert.SerializeObject(result, Formatting.Indented);
      OutputAsJson = asJson;

    }
  }
}