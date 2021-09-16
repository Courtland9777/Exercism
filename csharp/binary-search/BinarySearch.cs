using System;

public static class BinarySearch
{
    public static int Find(int[] input, int value)
    {
        var result = Array.BinarySearch(input, value);
        // It would be nice if the tests also accepted the negative bitwise complement of
        // the index of the first element that is larger than value either in addition
        // to -1 or in place of it. 
        return result < 0 ? -1 : result;
    }
}