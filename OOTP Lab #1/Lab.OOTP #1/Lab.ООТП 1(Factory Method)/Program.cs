using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FacthoryMethod
{
    public abstract class Car
    {
        public abstract string GetCarInfo();
    }
    public class SportsCar : Car
    {
        public override string GetCarInfo()
        {
            return "Спортивний автомобiль";
        }

        public class Crossover : Car
        {
            public override string GetCarInfo()
            {
                return "Кросовер";
            }
        }

        public class Sedan : Car
        {
            public override string GetCarInfo()
            {
                return "Седан";
            }
        }

        public class Hatchback : Car
        {
            public override string GetCarInfo()
            {
                return "Хетчбєк";
            }
        }

        public interface ICarFactory
        {
            Car CreateCar();
        }

        public class SportsCarFactory : ICarFactory
        {
            public Car CreateCar()
            {
                return new SportsCar();
            }
        }

        public class CrossoverFactory : ICarFactory
        {
            public Car CreateCar()
            {
                return new Crossover();
            }
        }

        public class SedanFactory : ICarFactory
        {
            public Car CreateCar()
            {
                return new Sedan();
            }
        }

        public class HatchbackFactory : ICarFactory
        {
            public Car CreateCar()
            {
                return new Hatchback();
            }
        }

        public class CarClient
        {
            public void CreateCar(ICarFactory factory)
            {
                Car car = factory.CreateCar();
                Console.WriteLine($"Продано автомобiль типу {car.GetCarInfo()}");
            }
        }

        class Program
        {
            static void Main(string[] args)
            {
                CarClient client = new CarClient();

                ICarFactory factory = new SedanFactory();
                client.CreateCar(factory);

                factory = new CrossoverFactory();
                client.CreateCar(factory);

                factory = new HatchbackFactory();
                client.CreateCar(factory);

                factory = new SportsCarFactory();
                client.CreateCar(factory);
            }
        }

    }
}