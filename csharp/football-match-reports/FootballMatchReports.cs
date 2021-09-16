using System;
using System.Linq;
public static class PlayAnalyzer
{
    public static string AnalyzeOnField(int shirtNum) => shirtNum switch
    {
        1 => "goalie",
        2 => "left back",
        3 or 4 => "center back",
        5 => "right back",
        <= 8 and >= 6 => "midfielder",
        9 => "left wing",
        10 => "striker",
        11 => "right wing",
        _ => throw new ArgumentException("Shirt number is not valid", nameof(shirtNum))
    };

    public static string AnalyzeOffField(object report) =>
        report switch
        {
            int i => AnalyzeOnField(i),
            string s => s,
            Foul f => f.GetDescription(),
            Injury j => $"{j.GetDescription()} Medics are on the field.",
            Incident x => x.GetDescription(),
            Manager m => m.Name.Equals(string.Empty) ? "the manager" : m.Name,
            _ => throw new ArgumentException("Invalid type.", nameof(report))
        };
}

// **** please do not modify the Manager class ****
public class Manager
{
    public string Name { get; }
    public string Activity { get; }

    public Manager(string name, string activity)
    {
        this.Name = name;
        this.Activity = activity;
    }
}

// **** please do not modify the Incident class or any subclasses ****
public class Incident
{
    public virtual string GetDescription() => "An incident happened.";
}

// **** please do not modify the Foul class ****
public class Foul : Incident
{
    public override string GetDescription() => "The referee deemed a foul.";
}

// **** please do not modify the Injury class ****
public class Injury : Incident
{
    public override string GetDescription() => "A player is injured.";
}
