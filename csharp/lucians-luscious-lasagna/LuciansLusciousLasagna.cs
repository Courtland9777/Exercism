using System;
using System.Collections.Generic;

public class Lasagna
{
    private const int ExpectedOvenTime = 40;
    private const int TimeToPrepareLayer = 2;

    internal int ExpectedMinutesInOven() => ExpectedOvenTime;

    internal int RemainingMinutesInOven(int timeCooked) => ExpectedOvenTime - timeCooked;

    internal int PreparationTimeInMinutes(int numberOfLayers) => numberOfLayers * TimeToPrepareLayer;

    internal int ElapsedTimeInMinutes(int numberOfLayers, int timeAlreadyInOven) =>
        PreparationTimeInMinutes(numberOfLayers) + timeAlreadyInOven;
}
