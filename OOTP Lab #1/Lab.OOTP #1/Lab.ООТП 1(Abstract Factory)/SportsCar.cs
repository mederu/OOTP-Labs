using AbstractFactoryPattern;
using System;

public class SportsCar : Car
{
    public override string GetCarInfo()
    {
        return "Sports Car";
    }

    public override int GetPrice()
    {
        return 50000;
    }

    public override string GetMark()
    {
        return "Ferrari";
    }
}
