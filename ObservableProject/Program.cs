Stock stock=new Stock();
Bank bank = new Bank("Юнит",stock);
Broker broker = new Broker("Петр Петрович",stock);
stock.Market();
broker.StopTrade();
stock.Market();

interface IObserver
{
    void Update(Object ob);
}

interface IObservable
{
    void RegisterObserver(IObserver o);
    void RemoveObserver(IObserver o);
    void NotifyObservers();
}
class StockInfo
{
    public int USD { get; set; }
    public int Euro { get; set; }
}

class Stock : IObservable
{
    StockInfo sInfo;
    List<IObserver> observers;
    public void NotifyObservers()
    {
        foreach(IObserver o in observers)
        {
            o.Update(sInfo);
        }
    }
    public Stock()
    {
        observers = new List<IObserver>();
        sInfo = new StockInfo();
    }
    public void RegisterObserver(IObserver o)
    {
        observers.Add(o);
    }
    public void RemoveObserver(IObserver o)
    {
        observers.Remove(o);
    }
    public void Market()
    {
        Random random = new Random();
        sInfo.USD = random.Next(40, 60);
        sInfo.Euro = random.Next(60, 80);
        NotifyObservers();
    }
}

class Broker : IObserver
{
    public string Name { get; set; }
    IObservable stock;
    public Broker(string name, IObservable stock)
    {
        Name = name;
        this.stock = stock;
        stock.RegisterObserver(this);
    }
    public void Update(object ob)
    {
        StockInfo sInfo = (StockInfo)ob;
        if (sInfo.USD > 50) Console.WriteLine($"Брокер {this.Name} продает доллары по {sInfo.USD}");
        else Console.WriteLine($"Брокер {this.Name} продает доллары по {sInfo.USD}");
    }
    public void StopTrade()
    {
        stock.RemoveObserver(this);
        stock = null;
    }
}
class Bank : IObserver
{
    public string Name { get; set; }
    IObservable stock;

    public Bank(string name, IObservable stock)
    {
        Name = name;
        this.stock = stock;
        stock.RegisterObserver(this);
    }

    public void Update(object ob)
    {
        StockInfo sInfo = (StockInfo)ob;
        if (sInfo.USD > 70) Console.WriteLine($"Банк {this.Name} продает евро по {sInfo.Euro}");
        else Console.WriteLine($"Банк {this.Name} продает евро по {sInfo.Euro}");

    }
}

