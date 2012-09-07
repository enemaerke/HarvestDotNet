using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HarvestDotNet.TestHarness
{
  class Program
  {
    static void Main(string[] args)
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        using(var form = new MainForm())
        {
            Application.Run(form);
        }
    }
  }
}
