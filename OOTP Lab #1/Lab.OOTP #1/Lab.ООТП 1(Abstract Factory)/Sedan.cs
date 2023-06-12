using AbstractFactoryPattern;
using System;

public class Sedan : Car
{
    public override string GetCarInfo()
    {
        return "Седан";
    }

    public override int GetPrice()
    {
        return 20000;
    }

    public override string GetMark()
    {
        return "Honda";
    }
}
