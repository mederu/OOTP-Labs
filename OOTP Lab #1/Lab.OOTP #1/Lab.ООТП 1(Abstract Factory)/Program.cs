using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AbstractFactoryPattern
{
    public abstract class Car
    {
        public abstract string GetCarInfo();
        public abstract string GetMark();
        public abstract int GetPrice();
    }

    public abstract class CarFeatures
    {
        public abstract string GetCarFeaturesInfo();
    }

        public class LeatherSeats : CarFeatures
        {
            public override string GetCarFeaturesInfo()
            {
                return "Шкiряне сидiння";
            }
        }

        public class Sunroof : CarFeatures
        {
            public override string GetCarFeaturesInfo()
            {
                return "Потолочний люк";
            }
        }

        public class NavigationSystem : CarFeatures
        {
            public override string GetCarFeaturesInfo()
            {
                return "Навiгацiйна система";
            }
        }

        public interface ICarFactory
        {
            Car CreateCar();

            CarFeatures CreateCarFeatures();
        }

        public class SportsCarFactory : ICarFactory
        {
            public Car CreateCar()
            {
                return new SportsCar();
            }

            public CarFeatures CreateCarFeatures()
            {
                return new LeatherSeats();
            }
        }

        public class CrossoverFactory : ICarFactory
        {
            public Car CreateCar()
            {
                return new Crossover();
            }

            public CarFeatures CreateCarFeatures()
            {
                return new NavigationSystem();
            }
        }

        public class SedanFactory : ICarFactory
        {
            public Car CreateCar()
            {
                return new Sedan();
            }

            public CarFeatures CreateCarFeatures()
            {
                return new Sunroof();
            }
        }

        public class HatchbackFactory : ICarFactory
        {
            public Car CreateCar()
            {
                return new Hatchback();
            }

            public CarFeatures CreateCarFeatures()
            {
                return new Sunroof();
            }
        }

        public class CarClient
        {
            public void CreateCar(ICarFactory factory)
            {
                Car car = factory.CreateCar();
                CarFeatures features = factory.CreateCarFeatures();
                Console.WriteLine($"Продано автомобiль {car.GetMark()} типу {car.GetCarInfo()} з комплектацiєю {features.GetCarFeaturesInfo()} за ${car.GetPrice()} ");
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
