using System;
using System.Collections.Generic;
using System.Linq;

public enum Owner
{
    None,
    Black,
    White,
    OffBoard
}

public class GoCounting
{
    private readonly IEnumerable<(Owner owner, (int x, int y) coord)> _gameBoard;

    public GoCounting(string input)
    {
        var lines = input.Split(Environment.NewLine.ToCharArray());
        _gameBoard = Enumerable.Range(0, NumRows(lines))
            .SelectMany(rowIndex => Enumerable.Range(0, NumColumns(lines))
                .Select(columnIndex => (OwnerFromChar(lines[rowIndex][columnIndex]), (columnIndex, rowIndex))
                )
            );
    }

    public Tuple<Owner, HashSet<(int x, int y)>> Territory((int x, int y) coordinate)
    {
        if (_gameBoard.All(i => i.coord != coordinate))
        {
            throw new ArgumentException($"coordinate ({coordinate.x},{coordinate.y}) is outside of the board");
        }

        var intersectionOwner = IntersectionStone(coordinate);
        if (intersectionOwner != Owner.None)
        {
            return new Tuple<Owner, HashSet<(int, int)>>(Owner.None, new HashSet<(int, int)>());
        }

        var territory = TerritoryIntersections(coordinate, new HashSet<(int x, int y)>());
        var owner = TerritoryOwner(territory);

        return new Tuple<Owner, HashSet<(int, int)>>(owner, territory);
    }

    public Dictionary<Owner, HashSet<(int, int)>> Territories()
    {
        var territories = new Dictionary<Owner, HashSet<(int, int)>>
        {
            { Owner.Black, new HashSet<(int, int)>() },
            { Owner.White, new HashSet<(int, int)>() },
            { Owner.None, new HashSet<(int, int)>() }
        };

        _gameBoard
            .Where(intersection => intersection.owner == Owner.None)
            .Select(intersection => Territory(intersection.coord))
            .ToList()
            .ForEach(territory => territories[territory.Item1].UnionWith(territory.Item2));
        return territories;
    }

    private static Owner OwnerFromChar(char c) =>
        c switch
        {
            'B' => Owner.Black,
            'W' => Owner.White,
            _ => Owner.None
        };

    private static int NumRows(IReadOnlyCollection<string> lines) =>
        lines.Count;

    private static int NumColumns(IReadOnlyList<string> lines) =>
        lines[0].Contains('\n')
            ? lines[0].Length - 1
            : lines[0].Length;

    private Owner IntersectionStone((int x, int y) coordinate) =>
        _gameBoard.Any(i => i.coord == coordinate)
            ? _gameBoard.First(i => i.coord == coordinate).owner
            : Owner.OffBoard;

    private HashSet<(int x, int y)> TerritoryIntersections((int x, int y) coordinate, HashSet<(int x, int y)> territory)
    {
        if (territory.Contains(coordinate))
        {
            return territory;
        }

        var intersectionOwner = IntersectionStone(coordinate);
        if (intersectionOwner != Owner.None)
        {
            return territory;
        }

        var current = new HashSet<(int x, int y)>(territory) { coordinate };
        Neighbors(coordinate).ForEach(n => current.UnionWith(TerritoryIntersections(n, current)));
        return current;
    }

    private Owner TerritoryOwner(IEnumerable<(int x, int y)> territory) =>
        DecideOwner(TerritoryOwners(territory));

    private static Owner DecideOwner(IEnumerable<Owner> owners)
    {
        var enumerable = owners as Owner[] ?? owners.ToArray();
        return enumerable.Contains(Owner.Black) && !enumerable.Contains(Owner.White) ? Owner.Black
            : enumerable.Contains(Owner.White) && !enumerable.Contains(Owner.Black) ? Owner.White
            : Owner.None;
    }

    private IEnumerable<Owner> TerritoryOwners(IEnumerable<(int x, int y)> territory) =>
        territory.SelectMany(IntersectionOwners);

    private IEnumerable<Owner> IntersectionOwners((int x, int y) coordinate) =>
        Neighbors(coordinate).Select(IntersectionStone);

    private static List<(int x, int y)> Neighbors((int x, int y) coordinate) =>
        new()
        {
            (coordinate.x, coordinate.y - 1),
            (coordinate.x, coordinate.y + 1),
            (coordinate.x - 1, coordinate.y),
            (coordinate.x + 1, coordinate.y)
        };
}