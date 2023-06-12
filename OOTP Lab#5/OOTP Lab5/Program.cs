using System;
using System.Collections.Generic;

public interface Iterator
{
    bool HasNext();
    Computer Next();
}

public class ComputerIterator : Iterator
{
    private List<Computer> computers;
    private int position;

    public ComputerIterator(List<Computer> computers)
    {
        this.computers = computers;
        position = 0;
    }

    public bool HasNext()
    {
        return position < computers.Count;
    }

    public Computer Next()
    {
        Computer computer = computers[position];
        position++;
        return computer;
    }
}

public interface ComputerState
{
    void Handle();
}

public class AvailableState : ComputerState
{
    public void Handle()
    {
        Console.WriteLine("This computer is available for purchase.");
    }
}

public class SoldState : ComputerState
{
    public void Handle()
    {
        Console.WriteLine("This computer has been sold.");
    }
}

public class OutOfStockState : ComputerState
{
    public void Handle()
    {
        Console.WriteLine("This computer is currently out of stock.");
    }
}

public abstract class ComputerHandler
{
    protected ComputerHandler successor;

    public void SetSuccessor(ComputerHandler successor)
    {
        this.successor = successor;
    }

    public abstract void HandleRequest(Computer computer);
}

public class SellComputerHandler : ComputerHandler
{
    public override void HandleRequest(Computer computer)
    {
        if (computer.State is AvailableState)
        {
            computer.State = new SoldState();
            Console.WriteLine("The computer has been sold.");
        }
        else if (successor != null)
        {
            successor.HandleRequest(computer);
        }
    }
}

public class RestockComputerHandler : ComputerHandler
{
    public override void HandleRequest(Computer computer)
    {
        if (computer.State is OutOfStockState)
        {
            computer.State = new AvailableState();
            Console.WriteLine("The computer has been restocked.");
        }
        else if (successor != null)
        {
            successor.HandleRequest(computer);
        }
    }
}

public class Computer
{
    public string Name { get; set; }
    public string Type { get; set; }
    public string Processor { get; set; }
    public int RAM { get; set; }
    public string Storage { get; set; }
    public string VideoCard { get; set; }
    public ComputerState State { get; set; }

    public Computer(string name, string type, string processor, int ram, string storage, string videocard)
    {
        Name = name;
        Type = type;
        Processor = processor;
        RAM = ram;
        Storage = storage;
        VideoCard = videocard;
        State = new AvailableState();
    }
}

public class ComputerStore
{
    private List<Computer> computers;
    private Iterator iterator;

    private ComputerHandler sellHandler;
    private ComputerHandler restockHandler;

    public ComputerStore()
    {
        computers = new List<Computer>();
        iterator = new ComputerIterator(computers);

        sellHandler = new SellComputerHandler();
        restockHandler = new RestockComputerHandler();
        sellHandler.SetSuccessor(restockHandler);
    }

    public void AddComputer(Computer computer)
    {
        computers.Add(computer);
    }

    public void SellComputer(string name)
    {
        foreach (Computer computer in computers)
        {
            if (computer.Name.Equals(name))
            {
                sellHandler.HandleRequest(computer);
                return;
            }
        }

        Console.WriteLine("Computer not found.");
    }
    public void RestockComputer(string name)
    {
        foreach (Computer computer in computers)
        {
            if (computer.Name.Equals(name))
            {
                restockHandler.HandleRequest(computer);
                return;
            }
        }
        Console.WriteLine("Computer not found.");
    }

    public void ListComputers()
    {
        Console.WriteLine("Available computers:");
        while (iterator.HasNext())
        {
            Computer computer = iterator.Next();
            Console.WriteLine("{0} ({1})", computer.Name, computer.Type);
            Console.WriteLine("Processor: {0}", computer.Processor);
            Console.WriteLine("RAM: {0} GB", computer.RAM);
            Console.WriteLine("Storage: {0}", computer.Storage);
            Console.WriteLine("Video Card: {0}", computer.VideoCard);
            computer.State.Handle();
            Console.WriteLine();
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        ComputerStore store = new ComputerStore();
        // Add some sample computers to the store
        store.AddComputer(new Computer("Gamer PC 1", "Gamer", "Intel Core i9-11900K", 32, "1TB SSD", "NVIDIA GeForce RTX 3090"));
        store.AddComputer(new Computer("Gamer PC 2", "Gamer", "AMD Ryzen 9 5900X", 16, "2TB HDD", "NVIDIA GeForce RTX 3080"));
        store.AddComputer(new Computer("Workstation 1", "Workstation", "Intel Xeon W-3275", 64, "2TB HDD", "AMD Radeon Pro W5500"));
        store.AddComputer(new Computer("Home PC 1", "Home", "Intel Core i5-11600K", 16, "512GB SSD", "NVIDIA GeForce GTX 1660"));

        // List the available computers
        store.ListComputers();

        // Sell a computer
        Console.WriteLine("Selling Gamer PC 1...");
        store.SellComputer("Gamer PC 1");

        // List the available computers again
        store.ListComputers();

        // Restock a computer
        Console.WriteLine("Restocking Workstation 1...");
        store.RestockComputer("Workstation 1");

        // List the available computers one last time
        store.ListComputers();
    }
}