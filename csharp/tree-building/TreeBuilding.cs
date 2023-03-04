using System;
using System.Collections.Generic;
using System.Linq;

public class TreeBuildingRecord
{
    public int ParentId { get; set; }
    public int RecordId { get; set; }
}

public class Tree
{
    public int Id { get; set; }
    public int ParentId { get; set; }
    public List<Tree> Children { get; set; } = new();
    public bool IsLeaf => Children.Count == 0;
}

public static class TreeBuilder
{
    public static Tree BuildTree(IEnumerable<TreeBuildingRecord> records)
    {
        // Sort records by record id
        var orderedRecords = records.OrderBy(r => r.RecordId).ToList();

        // Validate records
        ValidateRecords(orderedRecords);

        // Build trees from records
        var trees = BuildTrees(orderedRecords);

        // Connect children to parents
        ConnectChildrenToParents(trees);

        // Return root node
        return trees.First(t => t.Id == 0);
    }

    private static void ValidateRecords(IReadOnlyList<TreeBuildingRecord> records)
    {
        if (records.Count == 0)
        {
            throw new ArgumentException("Records cannot be empty.");
        }

        if (records[0].RecordId != 0)
        {
            throw new ArgumentException("Root node must have a record id of 0.");
        }

        if (records[0].ParentId != 0)
        {
            throw new ArgumentException("Root node must have a parent id of 0.");
        }

        for (var i = 1; i < records.Count; i++)
        {
            var record = records[i];

            if (record.RecordId != i)
            {
                throw new ArgumentException(
                    $"Record id must be consecutive starting from 0. Invalid record id: {record.RecordId}.");
            }

            if (record.ParentId >= i)
            {
                throw new ArgumentException(
                    $"Parent id cannot be greater than or equal to record id. Invalid parent id: {record.ParentId}.");
            }
        }
    }


    private static List<Tree> BuildTrees(IEnumerable<TreeBuildingRecord> records) => records
        .Select(record => new Tree { Id = record.RecordId, ParentId = record.ParentId }).ToList();

    private static void ConnectChildrenToParents(IReadOnlyList<Tree> trees)
    {
        for (var i = 1; i < trees.Count; i++)
        {
            var child = trees[i];
            var parent = trees[child.ParentId];
            parent.Children.Add(child);
        }
    }
}