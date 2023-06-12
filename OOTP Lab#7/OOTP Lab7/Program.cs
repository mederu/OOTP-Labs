using System;
using System.Collections.Generic;
abstract class Computer
{
    public abstract void Accept(ComputerVisitor visitor);
}

class GamingPC : Computer
{
    public string Processor { get; set; }
    public int RAM { get; set; }
    public string VideoCard { get; set; }
    public string Storage { get; set; }

    public override void Accept(ComputerVisitor visitor)
    {
        visitor.Visit(this);
    }
}

class Workstation : Computer
{
    public string Processor { get; set; }
    public int RAM { get; set; }
    public string VideoCard { get; set; }
    public string Storage { get; set; }

    public override void Accept(ComputerVisitor visitor)
    {
        visitor.Visit(this);
    }
}

class HomePC : Computer
{
    public string Processor { get; set; }
    public int RAM { get; set; }
    public string VideoCard { get; set; }
    public string Storage { get; set; }

    public override void Accept(ComputerVisitor visitor)
    {
        visitor.Visit(this);
    }
}

interface ComputerVisitor
{
    void Visit(GamingPC computer);
    void Visit(Workstation computer);
    void Visit(HomePC computer);
}

class ComputerCharacteristicsVisitor : ComputerVisitor
{
    public void Visit(GamingPC computer)
    {
        Console.WriteLine($"Gaming PC: Processor - {computer.Processor}, RAM - {computer.RAM}GB, Video Card - {computer.VideoCard}, Storage - {computer.Storage}");
    }

    public void Visit(Workstation computer)
    {
        Console.WriteLine($"Workstation: Processor - {computer.Processor}, RAM - {computer.RAM}GB, Video Card - {computer.VideoCard}, Storage - {computer.Storage}");
    }

    public void Visit(HomePC computer)
    {
        Console.WriteLine($"Home PC: Processor - {computer.Processor}, RAM - {computer.RAM}GB, Video Card - {computer.VideoCard}, Storage - {computer.Storage}");
    }
}

class Memento
{
    public List<Computer> SelectedComputers { get; }

    public Memento(List<Computer> selectedComputers)
    {
        SelectedComputers = new List<Computer>(selectedComputers);
    }
}

class Caretaker
{
    private Memento _memento;

    public Memento GetMemento()
    {
        return _memento;
    }

    public void SetMemento(Memento memento)
    {
        _memento = memento;
    }
}

class ComputerStore
{
    private List<Computer> _availableComputers = new List<Computer>();

    public void AddComputer(Computer computer)
    {
        _availableComputers.Add(computer);
    }

    public void RemoveComputer(Computer computer)
    {
        _availableComputers.Remove(computer);
    }

    public void DisplayAvailableComputers()
    {
        Console.WriteLine("Available Computers:");
        foreach (var computer in _availableComputers)
        {
            computer.Accept(new ComputerCharacteristicsVisitor());
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        var computerStore = new ComputerStore();

        var gamingPC = new GamingPC
        {
            Processor = "Intel Core i7",
            RAM = 16,
            VideoCard = "NVIDIA GeForce RTX 2080",
            Storage = "1TB SSD"
        };
        computerStore.AddComputer(gamingPC);

        var workstation = new Workstation
        {
            Processor = "Intel Xeon E5",
            RAM = 32,
            VideoCard = "NVIDIA Quadro RTX 5000",
            Storage = "2TB HDD"
        };
        computerStore.AddComputer(workstation);

        var homePC = new HomePC
        {
            Processor = "AMD Ryzen 5",
            RAM = 8,
            VideoCard = "AMD Radeon RX 580",
            Storage = "512GB SSD"
        };
        computerStore.AddComputer(homePC);

        computerStore.DisplayAvailableComputers();

        var caretaker = new Caretaker();

        caretaker.SetMemento(new Memento(new List<Computer>()));

        var selectedComputers = caretaker.GetMemento().SelectedComputers;
        selectedComputers.Add(gamingPC);
        selectedComputers.Add(workstation);

        Console.WriteLine("\nSelected Computers:");
        foreach (var computer in selectedComputers)
        {
            computer.Accept(new ComputerCharacteristicsVisitor());
        }

        selectedComputers.RemoveAt(1);

        Console.WriteLine("\nUpdated Selected Computers:");
        foreach (var computer in selectedComputers)
        {
            computer.Accept(new ComputerCharacteristicsVisitor());
        }

        selectedComputers = caretaker.GetMemento().SelectedComputers;

        Console.WriteLine("\nRestored Selected Computers:");
        foreach (var computer in selectedComputers)
        {
            computer.Accept(new ComputerCharacteristicsVisitor());
        }
    }
}