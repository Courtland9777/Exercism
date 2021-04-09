using System;

public class SpaceAge
{
    private readonly int _seconds;
    private const double EarthYearConversionFactor = 31557600;

    public SpaceAge(int seconds) =>
        _seconds = seconds;

    public double OnEarth() =>
        _seconds / EarthYearConversionFactor;

    public double OnMercury() =>
        _seconds / (EarthYearConversionFactor * 0.2408467);

    public double OnVenus() =>
        _seconds / (EarthYearConversionFactor * 0.61519726);

    public double OnMars() =>
        _seconds / (EarthYearConversionFactor * 1.8808158);

    public double OnJupiter() =>
        _seconds / (EarthYearConversionFactor * 11.862615);

    public double OnSaturn() =>
        _seconds / (EarthYearConversionFactor * 29.447498);

    public double OnUranus() =>
        _seconds / (EarthYearConversionFactor * 84.016846);

    public double OnNeptune() =>
        _seconds / (EarthYearConversionFactor * 164.79132);
}