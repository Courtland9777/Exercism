using System.Collections.Generic;
using System.Linq;

public class BinTree
{
    public BinTree(int value, BinTree left, BinTree right)
    {
        Value = value;
        Left = left;
        Right = right;
    }

    public int Value { get; }
    public BinTree Left { get; }
    public BinTree Right { get; }

    public override bool Equals(object obj)
    {
        if (obj is not BinTree that || that.Value != Value)
        {
            return false;
        }

        var leftMatch = that.Left == null && Left == null || Left != null && Left.Equals(that.Left);
        var rightMatch = that.Right == null && Right == null || Right != null && Right.Equals(that.Right);
        return leftMatch && rightMatch;
    }

    public override int GetHashCode() => base.GetHashCode();
}

public class Zipper
{
    private BinTree _focus;
    private IEnumerable<(BinTree node, bool isLeft)> _parents;

    public int Value() => _focus.Value;

    public Zipper Left() => _focus.Left != null ? FromTree(_focus.Left, _parents.Prepend((_focus, true))) : null;

    public Zipper Right() => _focus.Right != null ? FromTree(_focus.Right, _parents.Prepend((_focus, false))) : null;

    public Zipper Up()
    {
        if (!_parents.Any())
        {
            return null;
        }

        var (node, isLeft) = _parents.First();
        var others = _parents.Skip(1);
        var newFocus = new BinTree(node.Value, isLeft ? _focus : node.Left, isLeft ? node.Right : _focus);
        return FromTree(newFocus, others);
    }

    public Zipper SetValue(int newValue) => FromTree(new BinTree(newValue, _focus.Left, _focus.Right), _parents);

    public Zipper SetLeft(BinTree binTree) => FromTree(new BinTree(_focus.Value, binTree, _focus.Right), _parents);

    public Zipper SetRight(BinTree binTree) => FromTree(new BinTree(_focus.Value, _focus.Left, binTree), _parents);

    public BinTree ToTree()
    {
        var top = this;
        while (top.Up() != null)
        {
            top = top.Up();
        }

        return top._focus;
    }

    public static Zipper FromTree(BinTree tree, IEnumerable<(BinTree, bool)> parents = null) => new()
        { _focus = tree, _parents = parents ?? Enumerable.Empty<(BinTree, bool)>() };

    public override bool Equals(object obj) => (obj as Zipper)?._focus.Equals(_focus) ?? false;

    public override int GetHashCode() => base.GetHashCode();
}