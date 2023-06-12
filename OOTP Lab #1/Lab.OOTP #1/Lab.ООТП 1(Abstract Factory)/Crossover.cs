using AbstractFactoryPattern;
using System;

public class Crossover : Car
{
    public override string GetCarInfo()
    {
        return "Кросовер";
    }

    public override int GetPrice()
    {
        return 30000;
    }

    public override string GetMark()
    {
        return "Toyota";
    }
}
