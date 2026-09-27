//stockbroker.cs
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Stock
{
    public class StockBroker
    {
        public string BrokerName { get; set; }
        // The broker holds a list of Stocks (though not strictly needed in this lab)
        public List<Stock> stocks = new List<Stock>();
        // We'll write to "Lab1_output.txt" in the same folder as the .exe
        private readonly string destPath =
        System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Lab1_Output.txt");
        // We’ll print this header in both console & file
        public string titles =
        "Broker".PadRight(10) +
        "Stock".PadRight(15) +
        "Value".PadRight(10) +
        "Changes".PadRight(10) +
        "Date and Time";


        private static readonly SemaphoreSlim outputLock = new SemaphoreSlim(1, 1);



        private static bool headerWritten = false;
        private static readonly object headerlock = new object();
        public StockBroker(string brokerName)
        {
            BrokerName = brokerName;

            lock (headerlock)
            {
                if (!headerWritten)
                {
                    Console.WriteLine(titles);

                    using (StreamWriter outputFile =
                           new StreamWriter(destPath, false))
                    {
                        outputFile.WriteLine(titles);
                    }

                    headerWritten = true;
                }
            }
        }
        public void AddStock(Stock stock)
        {
            stocks.Add(stock);
            // Subscribe to the stock’s event using our event handler
            stock.StockEvent += EventHandler;
        }
        private async void EventHandler(object? sender, StockNotification e)
        {
            if (sender is null)
                return;
            // The second parameter needs to be cast to StockNotification
            await Helper(sender, e);
        }
        public async Task Helper(object sender, StockNotification e)
        {
            // We could cast the sender back to Stock if we needed more info
            Stock newStock = (Stock)sender;
            // Construct the output line
            string message =
            $"{BrokerName.PadRight(10)}" +
            $"{e.StockName.PadRight(15)}" +
            $"{e.CurrentValue.ToString().PadRight(10)}" +
            $"{e.NumChanges.ToString().PadRight(10)}" +
            $"{DateTime.Now}";
            try
            {
                // Prevent two event handlers from writing
                // at the same time.
                await outputLock.WaitAsync();

                try
                {
                    // Write to file
                    using (StreamWriter outputFile =
                           new StreamWriter(destPath, true))
                    {
                        await outputFile.WriteLineAsync(message);
                    }

                    // Write the exact same message to console
                    Console.WriteLine(message);
                }
                finally
                {
                    outputLock.Release();
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine(
                    $"Error writing to file: {ex.Message}");
            }
        }
    }
}


