using System;
using System.Collections.Generic;

public class Authenticator
{
    public Authenticator()
    {
        Admin = BuildAdmin();
        Developers = BuildDevelopersDict();
    }

    public Identity Admin { get; }

    public IDictionary<string, Identity> Developers { get; }

    private static Identity BuildAdmin() => new()
    {
        Email = "admin@ex.ism",
        FacialFeatures = new FacialFeatures()
        {
            EyeColor = "green",
            PhiltrumWidth = 0.9m
        },
        NameAndAddress = new List<string>()
        {
            { "Chanakya" },
            { "Mombai" },
            { "India" }
        }
    };

    private static IDictionary<string, Identity> BuildDevelopersDict() =>
        new Dictionary<string, Identity>()
        {
            {"Bertrand", new Identity()
                {
                    Email = "bert@ex.ism",
                    FacialFeatures = new FacialFeatures()
                    {
                        EyeColor = "blue",
                        PhiltrumWidth = 0.8m
                    },
                    NameAndAddress = new List<string>()
                    {
                        { "Bertrand" },
                        { "Paris" },
                        { "France" }
                    }
                }
            },
            { "Anders", new Identity()
                {
                    Email = "bert@ex.ism",
                    FacialFeatures = new FacialFeatures()
                    {
                        EyeColor = "brown",
                        PhiltrumWidth = 0.85m
                    },
                    NameAndAddress = new List<string>()
                    {
                        { "Anders" },
                        { "Redmond" },
                        { "USA" }
                    }
                }
            }
        };
}

//**** please do not modify the FacialFeatures class ****
public class FacialFeatures
{
    public string EyeColor { get; set; }
    public decimal PhiltrumWidth { get; set; }
}

//**** please do not modify the Identity class ****
public class Identity
{
    public string Email { get; set; }
    public FacialFeatures FacialFeatures { get; set; }
    public IList<string> NameAndAddress { get; set; }
}
