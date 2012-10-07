using System;
using System.ComponentModel;
using System.Windows;
using Newtonsoft.Json;

namespace HarvestDotNet.TestApp
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    private MainWindowModel m_model;
    public MainWindow()
    {
      m_model = new MainWindowModel();
      InitializeComponent();
      this.DataContext = m_model;
    }

    private HarvestApi m_api;
    private void DoWithApi(Action<HarvestApi> callback)
    {
      try
      {
        if (m_api == null)
          m_api = new HarvestApi(new HarvestApiSettings()
                    {
                      BaseUri = new Uri(m_model.BaseUri),
                      UserName = m_model.UserName,
                      Password = m_model.Password
                    });

        callback(m_api);
      }
      catch (Exception exc)
      {
        output.Text = "Exception: " + exc.Message;
      }
    }

    private void GetProjectsClick(object sender, RoutedEventArgs e)
    {
      DoWithApi(api =>
                  {
                    var projects = api.GetProjects();
                    output.Text = JsonConvert.SerializeObject(projects.Result, Formatting.Indented);
                  });
    }
  }

  public class MainWindowModel : INotifyPropertyChanged
  {
    private const string RegistryPath = @"SOFTWARE\HarvestDotNet\TestApp";
    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged(string propertyName)
    {
      var evt = PropertyChanged;
      if (evt != null)
        evt(this, new PropertyChangedEventArgs(propertyName));
    }

    private RegistryProperty<string> m_baseUri = new RegistryProperty<string>(RegistryPath, "BaseUri", "", s => s, s => s);
    private RegistryProperty<string> m_userName = new RegistryProperty<string>(RegistryPath, "UserName", "", s => s, s => s);
    private RegistryProperty<string> m_password = new RegistryProperty<string>(RegistryPath, "Password", "", s => s, s => s);

    public string BaseUri
    {
      get { return m_baseUri.Value; }
      set { m_baseUri.Value = value; OnPropertyChanged("BaseUri"); }
    }
    public string UserName
    {
      get { return m_userName.Value; }
      set { m_userName.Value = value; OnPropertyChanged("UserName"); }
    }
    public string Password
    {
      get { return m_password.Value; }
      set { m_password.Value = value; OnPropertyChanged("Password"); }
    }
    
  }
}
