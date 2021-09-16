using System;

static class Badge
{
    public static string Print(int? id, string name, string? department)
    {
        if (department == null) department = "OWNER";
        return id == null ? $"{name} - {department.ToUpper()}" : $"[{id}] {name} - {department.ToUpper()}";
    }
}
