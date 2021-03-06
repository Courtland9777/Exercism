using System.Collections.Generic;
using Xunit;
using Exercism.Tests;

public class ListsTests
{
    [Fact]
    public void NewList()
    {
        Assert.Empty(Languages.NewList());
    }

    [Fact]
    public void ExistingList()
    {
        var expected = new List<string>();
        expected.Add("C#");
        expected.Add("Clojure");
        expected.Add("Elm");
        Assert.Equal(expected, Languages.GetExistingLanguages());
    }

    [Fact]
    public void AddLanguage()
    {
        var expected = new List<string>();
        expected.Add("C#");
        expected.Add("Clojure");
        expected.Add("Elm");
        expected.Add("Bash");
        Assert.Equal(expected,
            Languages.AddLanguage(Languages.GetExistingLanguages(), "Bash"));
    }

    [Fact]
    public void CountLanguages()
    {
        Assert.Equal(3,
            Languages.CountLanguages(Languages.GetExistingLanguages()));
    }

    [Fact]
    public void HasLanguage_yes()
    {
        Assert.True(Languages.HasLanguage(Languages.GetExistingLanguages(), "Elm"));
    }

    [Fact]
    public void HasLanguage_no()
    {
        Assert.False(Languages.HasLanguage(Languages.GetExistingLanguages(), "D"));
    }

    [Fact]
    public void ReverseList()
    {
        var expected = new List<string>();
        expected.Add("Elm");
        expected.Add("Clojure");
        expected.Add("C#");
        Assert.Equal(expected,
            Languages.ReverseList(Languages.GetExistingLanguages()));
    }

    [Fact]
    public void ContainsStar_yes()
    {
        Assert.True(Languages.ContainsStar(Languages.GetExistingLanguages()));
    }

    [Fact]
    public void ContainsStar_too_many()
    {
        var languages = Languages.GetExistingLanguages();
        languages.Insert(0, "VBA");
        Assert.False(Languages.ContainsStar(languages));
    }

    [Fact]
    public void ContainsStar_empty()
    {
        var languages = Languages.NewList();
        Assert.False(Languages.ContainsStar(languages));
    }

    [Fact]
    public void ContainsStar_single_star()
    {
        var languages = Languages.GetExistingLanguages();
        languages.RemoveAt(2);
        languages.RemoveAt(1);
        Assert.True(Languages.ContainsStar(languages));
    }

    [Fact]
    public void RemoveLanguage_yes()
    {
        var expected = new List<string>();
        expected.Add("C#");
        expected.Add("Elm");
        var languages = Languages.GetExistingLanguages();
        Assert.Equal(expected, Languages.RemoveLanguage(languages, "Clojure"));
    }

    [Fact]
    public void RemoveLanguage_no()
    {
        var expected = new List<string>();
        expected.Add("C#");
        expected.Add("Clojure");
        expected.Add("Elm");
        var languages = Languages.GetExistingLanguages();
        Assert.Equal(expected, Languages.RemoveLanguage(languages, "English"));
    }

    [Fact]
    public void EnsureUnique_yes()
    {
        var languages = Languages.GetExistingLanguages();
        Assert.True(Languages.EnsureUnique(languages));
    }

    [Fact]
    public void EnsureUnique_no()
    {
        var languages = Languages.GetExistingLanguages();
        languages.Add("C#");
        Assert.False(Languages.EnsureUnique(languages));
    }
}
