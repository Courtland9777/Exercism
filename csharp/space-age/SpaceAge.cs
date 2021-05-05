using System;

public class SpaceAge
{
    private readonly int _seconds;
    private const double EarthYearConversionFactor = 31557600;

    public SpaceAge(int seconds) =>
        _seconds = seconds;

    private double Calculate(double conversionFromEarthFactor) =>
        _seconds / (EarthYearConversionFactor * conversionFromEarthFactor);

    public double OnEarth() =>
        Calculate(1);

    public double OnMercury() =>
        Calculate(0.2408467);

    public double OnVenus() =>
        Calculate(0.61519726);

    public double OnMars() =>
        Calculate(1.8808158);

    public double OnJupiter() =>
        Calculate(11.862615);

    public double OnSaturn() =>
        Calculate(29.447498);

    public double OnUranus() =>
        Calculate(84.016846);

    public double OnNeptune() =>
        Calculate(164.79132);
}