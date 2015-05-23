using System;
using System.Threading;
using NQuotes;

namespace TestExperts.WinFormsTest
{
    public class WinFormsTestEA : MqlApi
    {
        private Thread _uiThread;
        private TestForm _form;
        private bool _isFormClosed;

        void StartUI()
        {
            // show a form and wait until it's closed
            _form = new TestForm(this);
            _form.ShowDialog();
            _isFormClosed = true;
        }

        public override int init()
        {
            // create and start a thread for UI 
            // this is needed to avoid blocking the terminal interaction with the MQL API
            _uiThread = new Thread(this.StartUI);
            _uiThread.Start();
            return 0;
        }

        // A helper function to do UI updates.
        // It's needed, because all UI calls must happen on the UI thread.
        // Invoke() runs the action on that thread.
        void InvokeUI(Action action)
        {
            if ((_form != null) && !_isFormClosed)
                _form.Invoke(action);
        }

        public override int start()
        {
            int elapsedSeconds = 0;

            // waiting loop until the form is closed
            // the MQL API in the UI thread will work while we are waiting here
            // (without this loop the calls happening between ticks would be blocked waiting for the next tick)
            while (!IsStopped() && !_isFormClosed)
            {
                Sleep(1000);
                elapsedSeconds++;

                // call MQL API "TimeLocal" function
                DateTime localTime = TimeLocal();
                string status = String.Format("Waiting for {0} sec", elapsedSeconds);

                // perform a UI update
                // use Invoke(), because all UI calls must happen on the UI thread
                InvokeUI(() =>
                {
                    _form.SetLocalTime(localTime);
                    _form.SetStatus(status);
                });
            }
            return 0;
        }

        public override int deinit()
        {
            // make sure that the form is closed
            // use Invoke(), because all UI calls must happen on the UI thread
            // after the form is closed the UI thread finishes
            InvokeUI(() => _form.Close());
            return 0;
        }
    }
}
