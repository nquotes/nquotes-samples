using System;
using System.Diagnostics;
using System.Timers;
using System.Windows;
using NQuotes;

namespace WPFFormTest
{
    public interface IMqlApiTaskRunner
    {
        void Post(Action<IMqlApi> action);
    }

    public partial class TestForm : Window
    {
        private readonly IMqlApiTaskRunner _apiRunner;
        private readonly Stopwatch _stopwatch;
        private readonly Timer _statusTimer;

        public TestForm(IMqlApiTaskRunner apiRunner)
        {
            _apiRunner = apiRunner;
            _stopwatch = Stopwatch.StartNew();
            InitializeComponent();

            _statusTimer = new Timer(1000);
            _statusTimer.Elapsed += statusTimer_Tick;
            _statusTimer.Start();
        }

        private void getInfoButton_Click(object sender, EventArgs e)
        {
            _apiRunner.Post((IMqlApi api) =>
                {
                    // call MQL API functions
                    string symbol = api.Symbol();
                    string terminalCompany = api.TerminalCompany();

                    // perform a UI update
                    // use InvokeAsync(), because all UI calls must happen on the UI thread
                    Dispatcher.InvokeAsync(new Action(() =>
                        {
                            symbolLabel.Content = symbol;
                            brokerCompanyLabel.Content = terminalCompany;
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
                    // use InvokeAsync(), because all UI calls must happen on the UI thread
                    Dispatcher.InvokeAsync(new Action(() =>
                        {
                            localTimeLabel.Content = localTime.ToLongTimeString();
                            statusLabel.Content = String.Format("Running for {0:mm\\:ss} sec", _stopwatch.Elapsed);
                        }));
                });
        }

        private void window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _statusTimer.Stop();
            _stopwatch.Stop();
        }
    }
}
