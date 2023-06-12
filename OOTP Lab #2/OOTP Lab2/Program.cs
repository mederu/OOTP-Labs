using System;
using System.Collections.Generic;

namespace ComputerShop
{
    public interface IComputer
    {
        string Make { get; set; }
        string Model { get; set; }
        string Type { get; }
        int Year { get; set; }
        string Process { get; set; }
        string VideoCard { get; set; }
        string Ram { get; set; }
        string Motherboard { get; set; }

        object Clone();
    }

    public class GamingComputer : IComputer
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public string Type { get; } = "Gaming computer";
        public int Year { get; set; }
        public string Process { get; set; }
        public string VideoCard { get; set; }
        public string Ram { get; set; }
        public string Motherboard { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }

    public class Workstation : IComputer
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public string Type { get; } = "Workstation";
        public int Year { get; set; }
        public string Process { get; set; }
        public string VideoCard { get; set; }
        public string Ram { get; set; }
        public string Motherboard { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }

    public interface IComputerBuilder
    {
        void SetMake(string make);
        void SetModel(string model);
        void SetYear(int year);
        void SetProcess(string process);
        void SetVideoCard(string videoCard);
        void SetRam(string ram);
        void SetMotherboard(string motherboard);
        IComputer GetComputer();
    }

    public class GamingComputerBuilder : IComputerBuilder
    {
        private readonly IComputer _computer = new GamingComputer();

        public void SetMake(string make)
        {
            _computer.Make = make;
        }

        public void SetModel(string model)
        {
            _computer.Model = model;
        }

        public void SetYear(int year)
        {
            _computer.Year = year;
        }

        public void SetProcess(string process)
        {
            _computer.Process = process;
        }

        public void SetVideoCard(string videoCard)
        {
            _computer.VideoCard = videoCard;
        }

        public void SetRam(string ram)
        {
            _computer.Ram = ram;
        }

        public void SetMotherboard(string motherboard)
        {
            _computer.Motherboard = motherboard;
        }

        public IComputer GetComputer()
        {
            return _computer.Clone() as IComputer;
        }
    }

    public class WorkstationBuilder : IComputerBuilder
    {
        private readonly IComputer _computer = new Workstation();

        public void SetMake(string make)
        {
            _computer.Make = make;
        }

        public void SetModel(string model)
        {
            _computer.Model = model;
        }
        public void SetYear(int year)
        {
            _computer.Year = year;
        }
        public void SetProcess(string process)
        {
            _computer.Process = process;
        }

        public void SetRam(string ram)
        {
            _computer.Ram = ram;
        }

        public void SetMotherboard(string motherboard)
        {
            _computer.Motherboard = motherboard;
        }

        public void SetVideoCard(string videoCard)
        {
            _computer.VideoCard = videoCard;
        }

        public IComputer GetComputer()
        {
            return _computer.Clone() as IComputer;
        }
    }
    public class ComputerWebsite
    {
        private readonly Dictionary<string, IComputerBuilder> _builders = new Dictionary<string, IComputerBuilder>();

        public ComputerWebsite()
        {
            _builders.Add("gaming", new GamingComputerBuilder());
            _builders.Add("gaming 1", new GamingComputerBuilder());
            _builders.Add("gaming 2", new GamingComputerBuilder());
            _builders.Add("workstation", new WorkstationBuilder());
            _builders.Add("workstation 1", new WorkstationBuilder());
            _builders.Add("workstation 2", new WorkstationBuilder());
        }

        public void CreateComputer(string type, string make, string model, string process, string videocard, string ram, string motherboard)
        {
            if (!_builders.ContainsKey(type))
            {
                throw new ArgumentException($"Invalid computer type: {type}");
            }

            var builder = _builders[type];
            builder.SetMake(make);
            builder.SetModel(model);
            builder.SetProcess(process);
            builder.SetVideoCard(videocard);
            builder.SetRam(ram);
            builder.SetMotherboard(motherboard);
        }

        public void ShowComputers()
        {
            Console.WriteLine("Available Computers:");
            foreach (var builder in _builders.Values)
            {
                var computer = builder.GetComputer();
                Console.WriteLine($"- {computer.Make} {computer.Model} {computer.Type}, Processor: {computer.Process}, Videocard: {computer.VideoCard}, RAM: {computer.Ram}, Motherboard: {computer.Motherboard}");
            }
        }
    }

    // Client code
    public class Program
    {
        static void Main(string[] args)
        {
            var website = new ComputerWebsite();
            website.CreateComputer("gaming", "Asus", "ROG Strix G15", "Intel Core i7-11800H", "NVIDIA GeForce RTX 3060", "16GB DDR4", "ASUS TUF Gaming B560M-PLUS WIFI");
            website.CreateComputer("workstation", "Dell", "Precision 5750", "Intel Core i7-11850H", "NVIDIA Quadro RTX 3000", "32GB DDR4", "Dell Precision 5750");
            website.CreateComputer("gaming 1", "Alienware", "m15 R5", "Intel Core i9-11900H", "NVIDIA GeForce RTX 3080", "32GB DDR4", "Alienware m15 R5");
            website.CreateComputer("workstation 1", "HP", "ZBook Studio G8", "Intel Core i9-11900H", "NVIDIA Quadro RTX A5000", "64GB DDR4", "HP ZBook Studio G8");
            website.CreateComputer("gaming 2", "MSI", "GE75 Raider", "Intel Core i7-11800H", "NVIDIA GeForce RTX 3070", "16GB DDR4", "MSI GE75 Raider");
            website.CreateComputer("workstation 2", "Lenovo", "ThinkPad P1", "Intel Xeon E-2276M", "NVIDIA Quadro T1000", "32GB DDR4", "Lenovo ThinkPad P1");
            website.ShowComputers();
        }
    }
}