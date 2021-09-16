using System;
using Xunit;
using Exercism.Tests;

public class TuplesTest
{
    [Fact]
    public void AnalyzeOnField_1()
    {
        Assert.Equal("goalie", PlayAnalyzer.AnalyzeOnField(1));
    }

    [Fact]
    public void AnalyzeOnField_8()
    {
        Assert.Equal("midfielder", PlayAnalyzer.AnalyzeOnField(8));
    }

        [Fact]
    public void AnalyzeOnField_10()
    {
        Assert.Equal("striker", PlayAnalyzer.AnalyzeOnField(10));
    }

        [Fact]
    public void AnalyzeOnField_11()
    {
        Assert.Equal("right wing", PlayAnalyzer.AnalyzeOnField(11));
    }

        [Fact]
    public void AnalyzeOnField_throws_unknown_shirt_number()
    {
        Assert.Throws<ArgumentException>(() => PlayAnalyzer.AnalyzeOnField(1729));
    }

        [Fact]
    public void AnalyzeOffField_text()
    {
        Assert.Equal("They think it's all over!", PlayAnalyzer.AnalyzeOffField("They think it's all over!"));
    }

        [Fact]
    public void AnalyzeOffField_incident()
    {
        Assert.Equal("An incident happened.", PlayAnalyzer.AnalyzeOffField(new Incident()));
    }

        [Fact]
    public void AnalyzeOffField_foul()
    {
        Assert.Equal("The referee deemed a foul.", PlayAnalyzer.AnalyzeOffField(new Foul()));
    }

        [Fact]
    public void AnalyzeOffField_injury()
    {
        Assert.Equal("A player is injured. Medics are on the field.", PlayAnalyzer.AnalyzeOffField(new Injury()));
    }

        [Fact]
    public void AnalyzeOffField_anonymous_manager()
    {
        Assert.Equal("the manager", PlayAnalyzer.AnalyzeOffField(new Manager(string.Empty, string.Empty)));
    }

        [Fact]
    public void AnalyzeOffField_named_manager()
    {
        Assert.Equal("José Mário dos Santos Mourinho Félix",
            PlayAnalyzer.AnalyzeOffField(new Manager("José Mário dos Santos Mourinho Félix", string.Empty)));
    }

        [Fact]
    public void AnalyzeOffField_throws_type_single()
    {
        Assert.Throws<ArgumentException>(() => PlayAnalyzer.AnalyzeOffField(90.0f));
    }
}
