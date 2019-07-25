using System;
using System.Collections.Generic;
using System.Linq;
using NQuotes;

namespace DLLMethodCaller
{
    public class DLLMethodCallerEA : MqlApi
    {
        [ExternVariable]
        public string ActionName { get; set; }

        // GiveMoney action
        [ExternVariable]
        public int MoneyAmount { get; set; }
        // PredictFuture action
        [ExternVariable]
        public DateTime MyBirthday { get; set; }
        [ExternVariable]
        public bool AmMarried { get; set; }
        //[ExternVariable]
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
