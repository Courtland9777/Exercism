using System;
using System.Collections.Generic;
using System.Linq;

public static class Dominoes
{
    public static bool CanChain(IEnumerable<(int, int)> dominoes)
    {
        var valueTuples = dominoes as (int, int)[] ?? dominoes.ToArray();
        return !valueTuples.Any() || CanChain(valueTuples.ToArray().AsSpan());

        static bool CanChain(Span<(int DotCount1, int DotCount2)> dominoes)
        {
            var firstDominoFace = dominoes[0];
            if (dominoes.Length == 1)
            {
                return firstDominoFace.DotCount1 == firstDominoFace.DotCount2;
            }

            for (var i = 1; i < dominoes.Length; ++i)
            {
                var currentDominoFace = dominoes[i];
                if (currentDominoFace.DotCount1 == firstDominoFace.DotCount1)
                {
                    dominoes[i].DotCount1 = firstDominoFace.DotCount2;
                    if (CanChain(dominoes[1..]))
                    {
                        return true;
                    }
                }

                if (currentDominoFace.DotCount1 == firstDominoFace.DotCount2)
                {
                    dominoes[i].DotCount1 = firstDominoFace.DotCount1;
                    if (CanChain(dominoes[1..]))
                    {
                        return true;
                    }
                }

                dominoes[i].DotCount1 = currentDominoFace.DotCount1;
            }

            return false;
        }
    }
}