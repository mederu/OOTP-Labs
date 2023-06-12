using AbstractFactoryPattern;
using System;

public class Hatchback : Car
{
    public override string GetCarInfo()
    {
        return "Хєтчбек";
    }

    public override int GetPrice()
    {
        return 15000;
    }

    public override string GetMark()
    {
        return "Ford";
    }
}
