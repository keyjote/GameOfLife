using GameOfLife.LifeRules;
using GameOfLife.Modals;

namespace GameOfLife.Test;

public class TestingRules
{
    private readonly List<LifeTypes> _neighboursAlive;
    private readonly List<LifeTypes> _neighboursTwoAlive;
    private readonly List<LifeTypes> _neighboursOneAlive;
    private readonly List<LifeTypes> _neighboursDead;
    private readonly List<LifeTypes> _neighboursThreeAlive;
    private readonly List<LifeTypes> _neighboursFourAlive;

    private readonly LivingCellWithLessThanTwoNeighbourRule _lessThanTwoLivingRule;
    private readonly LivingCellWithTwoOrThreeNeighbourRule _twoOrThreeLivingRule;
    private readonly LivingCellWithMoreThanThreeNeighbourRule _moreThenThreeLivingRule;
    private readonly DeadCellWithThreeNeighbourRule _deadWithThreeLivingRule;

    public TestingRules()
    {
        _neighboursAlive = new List<LifeTypes>
        {
            LifeTypes.Heart, LifeTypes.Heart, LifeTypes.Heart,
            LifeTypes.Heart, LifeTypes.Heart, LifeTypes.Heart,
            LifeTypes.Heart, LifeTypes.Heart, LifeTypes.Heart,
        };
        _neighboursTwoAlive = new List<LifeTypes>
        {
            LifeTypes.EitherOr, LifeTypes.Heart, LifeTypes.Skull,
            LifeTypes.EitherOr, LifeTypes.Heart, LifeTypes.Skull,
        };
        _neighboursOneAlive = new List<LifeTypes>
        {
            LifeTypes.EitherOr, LifeTypes.Heart, LifeTypes.EitherOr,
            LifeTypes.EitherOr, LifeTypes.Skull, LifeTypes.EitherOr,
        };
        _neighboursDead = new List<LifeTypes>()
        {
            LifeTypes.Skull, LifeTypes.Skull, LifeTypes.Skull,
        };
        _neighboursThreeAlive = new List<LifeTypes>
        {
            LifeTypes.EitherOr, LifeTypes.Heart, LifeTypes.Skull,
            LifeTypes.Heart, LifeTypes.Heart, LifeTypes.Skull,
        };
        _neighboursFourAlive = new List<LifeTypes>
        {
            LifeTypes.EitherOr, LifeTypes.Heart, LifeTypes.Skull,
            LifeTypes.Heart, LifeTypes.Heart, LifeTypes.Heart,
        };
        
        _lessThanTwoLivingRule = new LivingCellWithLessThanTwoNeighbourRule();
        _twoOrThreeLivingRule = new LivingCellWithTwoOrThreeNeighbourRule();
        _moreThenThreeLivingRule = new LivingCellWithMoreThanThreeNeighbourRule();
        _deadWithThreeLivingRule = new DeadCellWithThreeNeighbourRule();
    }

    [Fact]
    public void all_alive()
    {
        var r1 = _lessThanTwoLivingRule.Apply(LifeTypes.Heart, _neighboursAlive);
        Assert.Null(r1);

        var r2 = _twoOrThreeLivingRule.Apply(LifeTypes.Heart, _neighboursAlive);
        Assert.Null(r2);

        var r3 = _moreThenThreeLivingRule.Apply(LifeTypes.Heart, _neighboursAlive);
        Assert.Equal(LifeTypes.Skull, r3);

        var r4 = _deadWithThreeLivingRule.Apply(LifeTypes.Skull, _neighboursAlive);
        Assert.Null(r4);
        
        var r5 = _deadWithThreeLivingRule.Apply(LifeTypes.Heart, _neighboursAlive);
        Assert.Null(r5);
    }
    
    [Fact]
    public void all_dead()
    {
        var r1 = _lessThanTwoLivingRule.Apply(LifeTypes.Heart, _neighboursDead);
        Assert.Equal(LifeTypes.Skull, r1);
        
        var r2 = _twoOrThreeLivingRule.Apply(LifeTypes.Heart, _neighboursDead);
        Assert.Null(r2);

        var r3 = _moreThenThreeLivingRule.Apply(LifeTypes.Heart, _neighboursDead);
        Assert.Null(r3);

        var r4 = _deadWithThreeLivingRule.Apply(LifeTypes.Skull, _neighboursDead);
        Assert.Null(r4);
        
        var r5 = _deadWithThreeLivingRule.Apply(LifeTypes.Heart, _neighboursDead);
        Assert.Null(r5);
    }
    
    [Fact]
    public void one_alive()
    {
        var r1 = _lessThanTwoLivingRule.Apply(LifeTypes.Heart, _neighboursOneAlive);
        Assert.Equal(LifeTypes.Skull, r1);
        
        var r2 = _twoOrThreeLivingRule.Apply(LifeTypes.Heart, _neighboursOneAlive);
        Assert.Null(r2);

        var r3 = _moreThenThreeLivingRule.Apply(LifeTypes.Heart, _neighboursOneAlive);
        Assert.Null(r3);

        var r4 = _deadWithThreeLivingRule.Apply(LifeTypes.Skull, _neighboursOneAlive);
        Assert.Null(r4);
        
        var r5 = _deadWithThreeLivingRule.Apply(LifeTypes.Heart, _neighboursOneAlive);
        Assert.Null(r5);
    }
    
    [Fact]
    public void two_alive()
    {
        var r1 = _lessThanTwoLivingRule.Apply(LifeTypes.Heart, _neighboursTwoAlive);
        Assert.Null(r1);
        
        var r2 = _twoOrThreeLivingRule.Apply(LifeTypes.Heart, _neighboursTwoAlive);
        Assert.Equal(LifeTypes.Heart, r2);

        var r3 = _moreThenThreeLivingRule.Apply(LifeTypes.Heart, _neighboursTwoAlive);
        Assert.Null(r3);

        var r4 = _deadWithThreeLivingRule.Apply(LifeTypes.Skull, _neighboursTwoAlive);
        Assert.Null(r4);
        
        var r5 = _deadWithThreeLivingRule.Apply(LifeTypes.Heart, _neighboursTwoAlive);
        Assert.Null(r5);
    }
    
    [Fact]
    public void three_alive()
    {
        var r1 = _lessThanTwoLivingRule.Apply(LifeTypes.Heart, _neighboursThreeAlive);
        Assert.Null(r1);
        
        var r2 = _twoOrThreeLivingRule.Apply(LifeTypes.Heart, _neighboursThreeAlive);
        Assert.Equal(LifeTypes.Heart, r2);

        var r3 = _moreThenThreeLivingRule.Apply(LifeTypes.Heart, _neighboursThreeAlive);
        Assert.Null(r3);

        var r4 = _deadWithThreeLivingRule.Apply(LifeTypes.Skull, _neighboursThreeAlive);
        Assert.Equal(LifeTypes.Heart, r4);
        
        var r5 = _deadWithThreeLivingRule.Apply(LifeTypes.Heart, _neighboursThreeAlive);
        Assert.Null(r5);
    }
    
    [Fact]
    public void four_alive()
    {
        var r1 = _lessThanTwoLivingRule.Apply(LifeTypes.Heart, _neighboursFourAlive);
        Assert.Null(r1);
        
        var r2 = _twoOrThreeLivingRule.Apply(LifeTypes.Heart, _neighboursFourAlive);
        Assert.Null(r2);

        var r3 = _moreThenThreeLivingRule.Apply(LifeTypes.Heart, _neighboursFourAlive);
        Assert.Equal(LifeTypes.Skull, r3);

        var r4 = _deadWithThreeLivingRule.Apply(LifeTypes.Skull, _neighboursFourAlive);
        Assert.Null(r4);
        
        var r5 = _deadWithThreeLivingRule.Apply(LifeTypes.Heart, _neighboursFourAlive);
        Assert.Null(r5);
    }
}