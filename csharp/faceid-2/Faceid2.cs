using System.Collections.Generic;

public record FacialFeatures
{
    public string EyeColor { get; }
    public decimal PhiltrumWidth { get; }

    public FacialFeatures(string eyeColor, decimal philtrumWidth)
    {
        EyeColor = eyeColor;
        PhiltrumWidth = philtrumWidth;
    }
}

public record Identity
{
    public string Email { get; }
    public FacialFeatures FacialFeatures { get; }

    public Identity(string email, FacialFeatures facialFeatures)
    {
        Email = email;
        FacialFeatures = facialFeatures;
    }
}

public class Authenticator
{
    private static HashSet<Identity> RegistryList;

    public Authenticator()
    {
        if (RegistryList == null) RegistryList = new();
    }

    public static bool AreSameFace(FacialFeatures faceA, FacialFeatures faceB) =>
        faceA == faceB;

    public bool IsAdmin(Identity identity) =>
        identity == BuildAdminIdentity();

    public bool Register(Identity identity)
    {
        if (IsRegistered(identity)) return false;
        RegistryList.Add(identity);
        return true;
    }

    public bool IsRegistered(Identity identity) =>
        RegistryList.Contains(identity);

    public static bool AreSameObject(Identity identityA, Identity identityB) =>
        ReferenceEquals(identityA, identityB);

    private static Identity BuildAdminIdentity() =>
        new("admin@exerc.ism", new FacialFeatures("green", 0.9m));
}