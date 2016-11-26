using System;
using System.Diagnostics;
using System.Windows.Forms;
using NQuotes;

namespace TestExperts.WinFormsTest
{
    public interface IMqlApiTaskRunner
    {
        void Post(Action<IMqlApi> action);
    }

    public partial class TestForm : Form
    {
        private readonly IMqlApiTaskRunner _apiRunner;
        private readonly Stopwatch _stopwatch;

        public TestForm(IMqlApiTaskRunner apiRunner)
        {
            _apiRunner = apiRunner;
            _stopwatch = Stopwatch.StartNew();
            InitializeComponent();
        }

        private void getInfoButton_Click(object sender, EventArgs e)
        {
            _apiRunner.Post((IMqlApi api) =>
                {
                    // call MQL API functions
                    string symbol = api.Symbol();
                    string terminalCompany = api.TerminalCompany();

                    // perform a UI update
                    // use Invoke(), because all UI calls must happen on the UI thread
                    this.Invoke(new Action(() =>
                        {
                            symbolLabel.Text = symbol;
                            brokerCompanyLabel.Text = terminalCompany;
                        }));
                });
        }

        private void statusTimer_Tick(object sender, EventArgs e)
        {
            _apiRunner.Post((IMqlApi api) =>
                {
                    // call MQL API "TimeLocal" function
                    DateTime localTime = api.TimeLocal();

                    // perform a UI update
                    // use Invoke(), because all UI calls must happen on the UI thread
                    this.Invoke(new Action(() =>
                        {
                            localTimeLabel.Text = localTime.ToLongTimeString();
                            statusLabel.Text = String.Format("Running for {0:mm\\:ss} sec", _stopwatch.Elapsed);
                        }));
                });
        }
    }
}
