

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
namespace Stock
{
    //-----------------------------------------------------------------------------------
    public class Stock
    {
        public event EventHandler<StockNotification>? StockEvent;

        // Private fields
        private string _name;
        private int _initialValue;
        private int _maxChange;
        private int _threshold;
        private int _numChanges;
        private int _currentValue;

        private readonly Thread _thread;

        // Properties
        public string StockName
        {
            get => _name;
            set => _name = value;
        }

        public int InitialValue
        {
            get => _initialValue;
            private set => _initialValue = value;
        }

        public int CurrentValue
        {
            get => _currentValue;
            private set => _currentValue = value;
        }

        public int MaxChange
        {
            get => _maxChange;
            private set => _maxChange = value;
        }

        public int Threshold
        {
            get => _threshold;
            private set => _threshold = value;
        }

        public int NumChanges
        {
            get => _numChanges;
            private set => _numChanges = value;
        }

      
        public Stock(string name, int startingValue, int maxChange, int threshold)
        {
            StockName = name;
            InitialValue = startingValue;
            CurrentValue = startingValue;
            MaxChange = maxChange;
            Threshold = threshold;
            NumChanges = 0;

            _thread = new Thread(Activate);
            _thread.Start();
        }
        //-----------------------------------------------------------------------------------
        /// <summary>
        /// Activates the threads synchronizations
        /// </summary>
        public void Activate()
        {
            for (int i = 0; i < 25; i++)
            {
                Thread.Sleep(500); // 1/2 second
                ChangeStockValue();
            }
        }
    
   
        /// Changes the stock value and also raising the event of stock value changes
    
        public void ChangeStockValue()
        {
            var rand = new Random();
            CurrentValue += rand.Next(1, MaxChange);
            NumChanges++;
            if ((CurrentValue - InitialValue) > Threshold)
            { //RAISE THE EVENT
                StockEvent?.Invoke(this, new StockNotification(StockName, CurrentValue, NumChanges));
            }
        }
        //------------------------------------------------------------------------------------------------
    }
}






