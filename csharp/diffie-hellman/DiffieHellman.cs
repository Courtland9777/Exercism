using System.Numerics;
using System.Security.Cryptography;

public static class DiffieHellman
{
    private static readonly BigInteger One = new(1);

    public static BigInteger PrivateKey(BigInteger primeP)
    {
        BigInteger privateKey;
        do
        {
            var randomBytes = RandomNumberGenerator.GetBytes(primeP.GetByteCount());
            privateKey = new BigInteger(randomBytes, true);
        } while (privateKey <= One || privateKey >= primeP);

        return privateKey;
    }

    public static BigInteger PublicKey(BigInteger primeP, BigInteger primeG, BigInteger privateKey) =>
        BigInteger.ModPow(primeG, privateKey, primeP);

    public static BigInteger Secret(BigInteger primeP, BigInteger publicKey, BigInteger privateKey) =>
        BigInteger.ModPow(publicKey, privateKey, primeP);
}