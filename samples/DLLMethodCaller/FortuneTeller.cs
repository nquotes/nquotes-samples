using System;

namespace DLLMethodCaller
{
    class FortuneTeller
    {
        private int total;
        
        public void AcceptMoney(int amount)
        {
            total += amount;
        }

        public string PredictFuture(DateTime birthday, bool isMarried)
        {
            if (total <= 0)
                return "Pay me some money first.";
            total = 0;

            if (isMarried)
                return "You're broke!"; 

            var random = new Random(birthday.Year + birthday.Month + birthday.Day);
            bool willBeRich = (random.Next() % 2) == 0;

            if (willBeRich)
                return "You're going to be rich! Well done!";
            else
                return "You're not going to be rich, but also not broke. That's a good thing.";
        }
    }
}
