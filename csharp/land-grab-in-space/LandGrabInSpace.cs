using System;
using System.Collections.Generic;
using System.Linq;

public struct Coord
{
    public Coord(ushort x, ushort y)
    {
        X = x;
        Y = y;
    }

    public ushort X { get; }
    public ushort Y { get; }
}

public struct Plot
{
    public static List<Plot> PlotRepo { get; private set; }
    public static Plot LastPlot { get; private set; }

    public Plot(Coord coordA, Coord coordB, Coord coordC, Coord coordD)
    {
        CoordA = coordA;
        CoordB = coordB;
        CoordC = coordC;
        CoordD = coordD;
        LongestSide = FindLongestSide(coordA, coordB, coordC, coordD);
        if (PlotRepo == null) PlotRepo = new();
    }

    public static void SetLastPlot(Plot plot) => LastPlot = plot;

    public Coord CoordA { get; set; }
    public Coord CoordB { get; set; }
    public Coord CoordC { get; set; }
    public Coord CoordD { get; set; }
    public int LongestSide { get; set; }

    public static int FindLongestSide(Coord coordA, Coord coordB, Coord coordC, Coord coordD) => new int[4]
        {
            Math.Abs(coordA.X - coordB.X),
            Math.Abs(coordC.X - coordD.X),
            Math.Abs(coordA.Y - coordC.Y),
            Math.Abs(coordB.Y - coordD.Y)
        }.Max(c => c);
}


public class ClaimsHandler
{
    public void StakeClaim(Plot plot)
    {
        Plot.PlotRepo.Add(plot);
        Plot.SetLastPlot(plot);
    }

    public bool IsClaimStaked(Plot plot) => Plot.PlotRepo.Contains(plot);

    public bool IsLastClaim(Plot plot) => Plot.LastPlot.Equals(plot);

    public Plot GetClaimWithLongestSide()
    {
        var largestside = 0;
        Plot plotValue = default;
        foreach (Plot plot in Plot.PlotRepo)
        {
            if (plot.LongestSide > largestside)
            {
                plotValue = plot;
                largestside = plot.LongestSide;
            }
        }
        return plotValue;
    }
}
