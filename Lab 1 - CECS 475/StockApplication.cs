using System;
using Stock;


class StockApplication
{
    static void Main(string[] args)
    {
        Stock.Stock stock1 = new Stock.Stock("Technology", 160, 5, 15);
        Stock.Stock stock2 = new Stock.Stock("Retail", 30, 2, 6);
        Stock.Stock stock3 = new Stock.Stock("Banking", 90, 4, 10);
        Stock.Stock stock4 = new Stock.Stock("Commodity", 500, 20, 50);
        StockBroker b1 = new StockBroker("Broker 1");
        b1.AddStock(stock1);
        b1.AddStock(stock2);
        StockBroker b2 = new StockBroker("Broker 2");
        b2.AddStock(stock1);
        b2.AddStock(stock3);
        b2.AddStock(stock4);
        StockBroker b3 = new StockBroker("Broker 3");
        b3.AddStock(stock1);
        b3.AddStock(stock3);
        StockBroker b4 = new StockBroker("Broker 4");
        b4.AddStock(stock1);
        b4.AddStock(stock2);
        b4.AddStock(stock3);
        b4.AddStock(stock4);
    }

}
