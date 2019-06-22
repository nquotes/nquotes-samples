using System;
using NQuotes;

namespace ChartEventTest
{
    public class ChartEventTestEA : MqlApi
    {
        public override int init()
        {
            // enable receiving CHARTEVENT_OBJECT_CREATE in OnChartEvent
            ChartSetInteger(0, CHART_EVENT_OBJECT_CREATE, 0, 1);
            return 0;
        }

        // https://docs.mql4.com/basis/function/events#onchartevent
        public override void OnChartEvent(int id, long lparam, double dparam, string sparam)
        {
            string description;
            switch (id)
            {
                case CHARTEVENT_KEYDOWN:
                    long keyCode = lparam;
                    description = String.Format("key down - pressed a key with code {0}", keyCode);
                    break;
                case CHARTEVENT_CLICK:
                    int x = (int)lparam;
                    int y = (int)dparam;
                    description = String.Format("click - mouse clicked at position ({0}, {1})", x, y);
                    break;
                case CHARTEVENT_OBJECT_CREATE:
                    string objectName = sparam;
                    description = String.Format("object create - created an object '{0}'", objectName);
                    break;
                default:
                    // see https://docs.mql4.com/constants/chartconstants/enum_chartevents
                    description = String.Format("a chart event with id = {0}", id);
                    break;
            }
            Print("chart event: ", description);
        }

    }
}
