using System;
using System.Collections.Concurrent;
using System.Threading;
using NQuotes;

namespace TestExperts.WinFormsTest
{
    public class WinFormsTestEA : MqlApi, IMqlApiTaskRunner
    {
        private Thread _uiThread;
        private TestForm _form;
        private bool _isFormClosed;
        private BlockingCollection<Action<IMqlApi>> _taskRunnerQueue;

        void StartUI()
        {
            // show a form and wait until it's closed
            _form = new TestForm(this);
            _form.ShowDialog();
            _isFormClosed = true;
        }

        public override int init()
        {
            _taskRunnerQueue = new BlockingCollection<Action<IMqlApi>>();

            // create and start a thread for UI 
            // this is needed to avoid blocking the terminal interaction with the MQL API
            _uiThread = new Thread(this.StartUI);
            _uiThread.Start();
            return 0;
        }

        void IMqlApiTaskRunner.Post(Action<IMqlApi> action)
        {
            // add the task to the queue for later execution from the EA thread in start()
            _taskRunnerQueue.Add(action);
        }

        public override int start()
        {
            // waiting loop until the form is closed
            // the MQL API in the UI thread will work while we are waiting here
            // (without this loop the calls happening between ticks would be blocked waiting for the next tick)
            while (!IsStopped() && !_isFormClosed)
            {
                // execute a task posted from the UI
                Action<IMqlApi> action;
                if (_taskRunnerQueue.TryTake(out action, TimeSpan.FromSeconds(1)))
                    action(this);
            }

            // automatically remove EA from the chart when the form is closed
            if (!IsStopped())
                ExpertRemove();

            return 0;
        }

        public override int deinit()
        {
            // make sure that the form is closed
            // use Invoke(), because all UI calls must happen on the UI thread
            // after the form is closed the UI thread finishes
            if ((_form != null) && !_isFormClosed)
                _form.Invoke(new Action(_form.Close));
            return 0;
        }
    }
}
