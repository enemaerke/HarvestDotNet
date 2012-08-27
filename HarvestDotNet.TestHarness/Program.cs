using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HarvestDotNet.TestHarness
{
  class Program
  {
    static void Main(string[] args)
    {
      string uri = GetFromConsole("Enter the uri (e.g. https://yoursubdomain.harvestapp.com/projects):");
      string username = GetFromConsole("Enter your username (email):");
      string password = GetFromConsole("Enter your password:");


      //SampleFromHarvest.RunSample(uri, username, password);
      HttpTransmitter transmitter = new HttpTransmitter();
      Result<string> result = transmitter.ProcessRequest(uri, new Credentials {UserName = username, Password = password});

      Console.WriteLine(result.Value);
      Console.ReadKey();
    }

    private static string GetFromConsole(string prompt)
    {
      Console.WriteLine(prompt);
      string response = Console.ReadLine();
      Console.WriteLine();
      return response;
    }
  }
}
