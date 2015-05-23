using System;
using System.Windows.Forms;
using NQuotes;

namespace TestExperts.WinFormsTest
{
    public partial class TestForm : Form
    {
        private readonly IMqlApi _api;

        public TestForm(IMqlApi api)
        {
            _api = api;
            InitializeComponent();
        }

        private void getInfoButton_Click(object sender, EventArgs e)
        {
            // call MQL API functions
            symbolLabel.Text = _api.Symbol();
            brokerCompanyLabel.Text = _api.TerminalCompany();
        }

        public void SetLocalTime(DateTime serverTime)
        {
            localTimeLabel.Text = serverTime.ToLongTimeString();
        }

        public void SetStatus(string status)
        {
            statusLabel.Text = status;
        }
    }
}
