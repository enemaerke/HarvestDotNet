using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HarvestDotNet.TestHarness
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void buttonRun_Click(object sender, EventArgs e)
        {
            RequestInfo info = new RequestInfo()
                                   {
                                       Username = m_textUsername.Text,
                                       Password = m_textPassword.Text,
                                       Method = m_comboMethod.SelectedText,
                                       Payload = m_textMessage.Text,
                                       Url = m_textUrl.Text
                                   };

            if (m_backgroundWorker.IsBusy)
                m_backgroundWorker.CancelAsync();

            ClearMessage();
            ShowMessage("Starting request:");
            m_backgroundWorker.RunWorkerAsync(info);
        }

        private class RequestInfo
        {
            public string Username { get; set; }
            public string Password { get; set; }
            public string Url { get; set; }
            public string Method { get; set; }
            public string Payload { get; set; }
        }

        private void ClearMessage()
        {
            if (m_textMessage.InvokeRequired)
            {
                m_textMessage.BeginInvoke(new Action(() => ClearMessage()));
                return;
            }

            m_textMessage.Text = string.Empty;
            
        }
        private void EnableRunButton(bool enabled)
        {
            if (m_textMessage.InvokeRequired)
            {
                m_textMessage.BeginInvoke(new Action(() => EnableRunButton(enabled)));
                return;
            }

            buttonRun.Enabled = enabled;

        }
        private void ShowMessage(string message)
        {
            if (m_textMessage.InvokeRequired)
            {
                m_textMessage.BeginInvoke(new Action(() => ShowMessage(message)));
                return;
            }

            m_textMessage.AppendText(message + Environment.NewLine);
        }

        private void m_backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            RequestInfo info = e.Argument as RequestInfo;
            try
            {
                EnableRunButton(false);
                HarvestDotNet.HttpTransmitter transmitter = new HttpTransmitter();
                Result<string> result = transmitter.ProcessRequest(info.Url, new Credentials()
                                                         {
                                                             Password = info.Password,
                                                             UserName = info.Username
                                                         });
                ShowMessage("Result: ");
                ShowMessage(result.Value);

            }
            catch (Exception ex)
            {
                ShowMessage("Exception: " + ex.Message);
            }
        }

        private void m_backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            EnableRunButton(true);
            if (e.Cancelled)
                ShowMessage("Cancelled");
            if (e.Error != null)
                ShowMessage("Exception:" + e.Error.Message);
        }
    }
}
