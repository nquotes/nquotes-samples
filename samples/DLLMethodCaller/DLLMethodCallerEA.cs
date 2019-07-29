using System;
using NQuotes;

namespace DLLMethodCaller
{
    public class DLLMethodCallerEA : MqlApi
    {
        public string ActionName { get; set; }

        // GiveMoney action parameters
        public int MoneyAmount { get; set; }

        // PredictFuture action parameters
        public DateTime MyBirthday { get; set; }
        public bool AmMarried { get; set; }
        public string FuturePrediction { get; set; }

        private FortuneTeller fortuneTeller;
        
        public override int init()
        {
            fortuneTeller = new FortuneTeller();
            FuturePrediction = "Unknown";
            return 0;
        }

        public override int start()
        {
            switch (ActionName)
            {
                case "GiveMoney":
                    fortuneTeller.AcceptMoney(MoneyAmount);
                    break;
                case "PredictFuture":
                    FuturePrediction = fortuneTeller.PredictFuture(MyBirthday, AmMarried);
                    break;
            }
            return 0;
        }
    }
}
