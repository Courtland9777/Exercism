using System.Collections.Generic;
using System.Linq;

public class GradeSchool
{
    private readonly Dictionary<string, int> _roster;

    public GradeSchool() => _roster = new Dictionary<string, int>();

    public void Add(string student, int grade) => _roster.Add(student, grade);

    public IEnumerable<string> Roster() =>
        _roster.OrderBy(v => v.Value)
            .ThenBy(v => v.Key)
            .Select(x => x.Key);

    public IEnumerable<string> Grade(int grade) =>
        _roster.Where(y => y.Value == grade)
            .Select(x => x.Key)
            .OrderBy(n => n);
}