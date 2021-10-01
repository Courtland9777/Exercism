using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

public enum NumberTypes
{
    Long,
    Uint,
    Int,
    Ushort,
    Short,
    NegShort,
    NegInt,
    NegLong,
}

public static class TelemetryBuffer
{
    private static readonly Dictionary<NumberTypes, byte> prefixValuesDictionary;
    static TelemetryBuffer()
    {
        prefixValuesDictionary = PrefixValuesDictionary;
    }

    public static byte[] ToBuffer(long reading)
    {
        var optimizedType = ToOptimizedType(reading);
        var byteArray = BitConverter.GetBytes(reading);

        return reading >= 0 ? PositiveBuffers(optimizedType, byteArray) : NegativeBuffers(byteArray, optimizedType);
    }

    private static byte[] NegativeBuffers(byte[] byteArray, NumberTypes optimizedType)
    {
        var newArray = new byte[9];
        newArray[0] = prefixValuesDictionary[optimizedType];

        for (var i = 0; i < byteArray.Length; i++)
        {
            byteArray[i] = (unchecked((byte)((256 + byteArray[i]) % 256)));
        }

        switch (optimizedType)
        {
            case NumberTypes.NegShort:
            {
                for (var i = 2; i < byteArray.Length; i++)
                {
                    byteArray[i] = 0;
                }

                break;
            }
            case NumberTypes.NegInt:
            {
                for (var i = 4; i < byteArray.Length; i++)
                {
                    byteArray[i] = 0;
                }

                break;
            }
        }

        Array.Copy(byteArray, 0, newArray, 1, byteArray.Length);

        return newArray;
    }

    private static byte[] PositiveBuffers(NumberTypes optimizedType, byte[] byteArray)
    {
        var bufferArray = new byte[byteArray.Length+1];
        bufferArray[0] = prefixValuesDictionary[optimizedType];
        Array.Copy(byteArray, 0, bufferArray, 1, byteArray.Length);
        return bufferArray;
    }

    private static NumberTypes ToOptimizedType(long reading) =>
        reading switch
        {
            <= 9_223_372_036_854_775_807 and >= 4_294_967_296 => NumberTypes.Long,
            <= 4_294_967_295 and >= 2_147_483_648 => NumberTypes.Uint,
            <= 2_147_483_647 and >= 65_536 => NumberTypes.Int,
            <= 65_535 and >= 32_768 => NumberTypes.Ushort,
            <= 32_767 and >= 1 => NumberTypes.Short,
            <= 0 and >= -32_768 => NumberTypes.NegShort,
            <= -32_769 and >= -2_147_483_648 => NumberTypes.NegInt,
            <= -2_147_483_649 and >= -9_223_372_036_854_775_808 => NumberTypes.NegLong,
        };

    private static Dictionary<NumberTypes, byte> PrefixValuesDictionary =>
        new()
        {
            { NumberTypes.Long, 256 - 8 },
            { NumberTypes.Uint, 4 },
            { NumberTypes.Int, 256 - 4 },
            { NumberTypes.Ushort, 2 },
            { NumberTypes.Short, 256 - 2 },
            { NumberTypes.NegShort, 256 - 2 },
            { NumberTypes.NegInt, 256 - 4 },
            { NumberTypes.NegLong, 256 - 8 }
        };

    public static long FromBuffer(byte[] buffer) =>
        (new byte[] { 2, 4, 248, 252, 254 }).All(x => buffer[0] != x) ? 0 :
        buffer[0] == 254 ? BitConverter.ToInt16(buffer, 1) :
        buffer[0] == 252 ? BitConverter.ToInt32(buffer, 1) : 
        BitConverter.ToInt64(buffer, 1);
}
