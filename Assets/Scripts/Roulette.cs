using System;
using System.Collections.Generic;

public class Roulette
{
    private readonly List<Sector> _sectors;

    public Roulette(Dictionary<int, double> edges)
    {
        var sectors = new List<Sector>();

        double onePercentLength = 1.0d / 100;
        double sectorFrom = 0;

        foreach (var edge in edges)
        {
            var sectorTo = sectorFrom + edge.Value * onePercentLength;

            var sector = new Sector(edge.Key, sectorFrom, sectorTo);
            sectors.Add(sector);

            sectorFrom = sectorTo;
        }

        _sectors = sectors;
    }

    public int Roll()
    {
        Random random = new Random();
        var value = random.NextDouble();

        foreach (var sector in _sectors)
        {
            if (IsValueInSector(value, sector))
            {
                return sector.Id;
            }
        }
        
        return 0;
    }

    private static bool IsValueInSector(double value, Sector sector)
    {
        return value >= sector.From && value < sector.To;
    }
}
    
public class Sector
{
    public int Id { get; }
    public double From { get; }
    public double To { get; }

    public Sector(int id, double from, double to)
    {
        Id = id;
        From = from;
        To = to;
    }
}
