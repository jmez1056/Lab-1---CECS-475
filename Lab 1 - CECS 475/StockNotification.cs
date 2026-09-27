//    Stocknotification.cs
using System;

namespace Stock
{
    public class StockNotification : EventArgs
    {
        public string StockName { get; set; }
        public int CurrentValue { get; set; }
        public int NumChanges { get; set; }

        public StockNotification(string stockName, int currentValue, int numChanges)
        {
            // !NOTE!: Fill in below of what the notification will do using the comments above
            StockName = stockName;
            CurrentValue = currentValue;
            NumChanges = numChanges;
        }
    }
}


