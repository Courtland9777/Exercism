using System;
using System.Collections.Generic;
using System.Linq.Expressions;

public enum Plant
{
    Violets,
    Radishes,
    Clover,
    Grass
}

public enum StudentPosition
{
    Alice = 1,
    Bob = 2,
    Charlie = 3,
    David = 4,
    Eve = 5,
    Fred = 6,
    Ginny = 7,
    Harriet = 8,
    Ileana = 9,
    Joseph = 10,
    Kincaid = 11,
    Larry = 12
}

public class KindergartenGarden
{
    private readonly string _firstRow;
    private readonly string _secondRow;

    public KindergartenGarden(string diagram)
    {
         var rows = diagram.Split('\n');
         _firstRow = rows[0];
         _secondRow = rows[1];
    }

    public IEnumerable<Plant> Plants(string student)
    {
        var start = 2*(int)(StudentPosition)Enum.Parse(typeof(StudentPosition), student, false)-2;
        return new List<Plant>()
        {
            PlantSelector(_firstRow[start]),
            PlantSelector(_firstRow[start +1]),
            PlantSelector(_secondRow[start]),
            PlantSelector(_secondRow[start + 1]),
        };
    }

    private static Plant PlantSelector(char plant) => plant switch
    {
        'V' => Plant.Violets,
        'R' => Plant.Radishes,
        'C' => Plant.Clover,
        'G' => Plant.Grass,
        _ => throw new ArgumentException()
    };
}