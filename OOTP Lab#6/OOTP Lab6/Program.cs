using System;
using System.Collections.Generic;
using System.Linq;
public class Computer
{
    public string Type { get; set; }
    public string Processor { get; set; }
    public int RAM { get; set; }
    public string Storage { get; set; }
    public string GraphicsCard { get; set; }
}

public class ComputerMediator
{
    private List<Computer> computers;

    public ComputerMediator()
    {
        computers = new List<Computer>();
        InitializeComputers();
    }

    private void InitializeComputers()
    {
        computers.Add(new Computer { Type = "Геймерський", Processor = "Intel Core i7", RAM = 16, Storage = "SSD", GraphicsCard = "NVIDIA GeForce RTX 3080" });
        computers.Add(new Computer { Type = "Геймерський", Processor = "AMD Ryzen 9", RAM = 32, Storage = "SSD", GraphicsCard = "NVIDIA GeForce RTX 3090" });
        computers.Add(new Computer { Type = "Геймерський", Processor = "Intel Core i9", RAM = 32, Storage = "SSD", GraphicsCard = "NVIDIA GeForce RTX 3070" });

        computers.Add(new Computer { Type = "Робоча станцiя", Processor = "Intel Xeon", RAM = 64, Storage = "HDD", GraphicsCard = "NVIDIA Quadro RTX 5000" });
        computers.Add(new Computer { Type = "Робоча станцiя", Processor = "Intel Xeon", RAM = 128, Storage = "HDD", GraphicsCard = "NVIDIA Quadro RTX 6000" });
        computers.Add(new Computer { Type = "Робоча станцiя", Processor = "Intel Xeon", RAM = 256, Storage = "HDD", GraphicsCard = "NVIDIA Quadro RTX 8000" });

        computers.Add(new Computer { Type = "Домашнiй", Processor = "Intel Core i5", RAM = 8, Storage = "SSD", GraphicsCard = "NVIDIA GeForce GTX 1660" });
        computers.Add(new Computer { Type = "Домашнiй", Processor = "AMD Ryzen 5", RAM = 16, Storage = "SSD", GraphicsCard = "NVIDIA GeForce GTX 1650" });
        computers.Add(new Computer { Type = "Домашнiй", Processor = "Intel Core i3", RAM = 8, Storage = "HDD", GraphicsCard = "NVIDIA GeForce GTX 1050 Ti" });
    }

    public void AddToCart(Computer computer)
    {
        Console.WriteLine($"Додано в кошик: {computer.Type} ПК, процесор: {computer.Processor}, RAM: {computer.RAM}GB, зберiгання: {computer.Storage}, вiдеокарта: {computer.GraphicsCard}");
    }

    public List<Computer> GetAvailableComputers()
    {
        return computers;
    }
}

public interface Expression
{
    bool Interpret(Computer computer);
}

public class GraphicsCardExpression : Expression
{
    private string desiredGraphicsCard;

    public GraphicsCardExpression(string graphicsCard)
    {
        desiredGraphicsCard = graphicsCard;
    }

    public bool Interpret(Computer computer)
    {
        return computer.GraphicsCard == desiredGraphicsCard;
    }
}

public class ProcessorExpression : Expression
{
    private string desiredProcessor;

    public ProcessorExpression(string processor)
    {
        desiredProcessor = processor;
    }

    public bool Interpret(Computer computer)
    {
        return computer.Processor == desiredProcessor;
    }
}

public class RAMExpression : Expression
{
    private int desiredRAM;

    public RAMExpression(int ram)
    {
        desiredRAM = ram;
    }

    public bool Interpret(Computer computer)
    {
        return computer.RAM == desiredRAM;
    }
}

public class ComputerFilter
{
    private List<Computer> computers;

    public ComputerFilter(List<Computer> computerList)
    {
        computers = computerList;
    }

    public List<Computer> Filter(Expression expression)
    {
        List<Computer> filteredComputers = new List<Computer>();

        foreach (Computer computer in computers)
        {
            if (expression.Interpret(computer))
            {
                filteredComputers.Add(computer);
            }
        }

        return filteredComputers;
    }
}

class Program
{
    static void Main()
    {
        ComputerMediator mediator = new ComputerMediator();

        List<Computer> availableComputers = mediator.GetAvailableComputers();

        ComputerFilter computerFilter = new ComputerFilter(availableComputers);

        string desiredGraphicsCard = "NVIDIA GeForce GTX 1660";
        string desiredProcessor = "Intel Core i5";
        int desiredRAM = 8;

        Expression graphicsCardExpression = new GraphicsCardExpression(desiredGraphicsCard);
        Expression processorExpression = new ProcessorExpression(desiredProcessor);
        Expression ramExpression = new RAMExpression(desiredRAM);

        List<Computer> filteredComputers = computerFilter.Filter(graphicsCardExpression)
            .Intersect(computerFilter.Filter(processorExpression))
            .Intersect(computerFilter.Filter(ramExpression))
            .ToList();

        Console.WriteLine("Доступнi комп'ютери:");
        foreach (Computer computer in filteredComputers)
        {
            Console.WriteLine($"- {computer.Type} ПК, процесор: {computer.Processor}, RAM: {computer.RAM}GB, зберігання: {computer.Storage}, відеокарта: {computer.GraphicsCard}");
        }

        if (filteredComputers.Count > 0)
        {
            Computer selectedComputer = filteredComputers[0];
            mediator.AddToCart(selectedComputer);
        }

        Console.ReadLine();
    }
}