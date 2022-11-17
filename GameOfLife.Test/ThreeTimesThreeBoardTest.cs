using GameOfLife.LifeRules;
using GameOfLife.Modals;
using GameOfLife.PieceGenerators;

namespace GameOfLife.Test;

public class ThreeTimesThreeBoardTest
{
    private const int TOTAL_ROWS = 3;
    private const int TOTAL_COLUMNS = 3;
    private readonly List<ILifeRule> _rules;
    public ThreeTimesThreeBoardTest()
    {
        _rules = new List<ILifeRule> {
            new LivingCellWithLessThanTwoNeighbourRule(),
            new LivingCellWithTwoOrThreeNeighbourRule(),
            new LivingCellWithMoreThanThreeNeighbourRule(),
            new DeadCellWithThreeNeighbourRule(),
        };
    }
    
    [Fact]
    public void SimulationExample1()
    {
        var boardValues = new List<LifeTypes>
        {
            LifeTypes.Skull, LifeTypes.EitherOr, LifeTypes.Skull,
            LifeTypes.Skull, LifeTypes.Heart, LifeTypes.Skull,
            LifeTypes.Skull, LifeTypes.Skull, LifeTypes.Skull,
        };

        var presetGenerator = new PresetPiecesGenerator(TOTAL_ROWS, TOTAL_COLUMNS, boardValues);

        var board = new Board(TOTAL_ROWS, TOTAL_COLUMNS);
        Assert.Equal(TOTAL_COLUMNS * TOTAL_COLUMNS, boardValues.Count);
        
        board.InitializeBoard(presetGenerator);

        var result = EvaluateLife.EvaluateOne(1, 1, board, _rules);
        Assert.Equal(LifeTypes.Skull, result);
    }
    
    [Fact]
    public void SimulationExample2()
    {
        var boardValues = new List<LifeTypes>
        {
            LifeTypes.Skull, LifeTypes.EitherOr, LifeTypes.Heart,
            LifeTypes.Skull, LifeTypes.Heart, LifeTypes.Heart,
            LifeTypes.Skull, LifeTypes.Skull, LifeTypes.Skull,
        };

        var presetGenerator = new PresetPiecesGenerator(TOTAL_ROWS, TOTAL_COLUMNS, boardValues);

        var board = new Board(TOTAL_ROWS, TOTAL_COLUMNS);
        Assert.Equal(TOTAL_COLUMNS * TOTAL_COLUMNS, boardValues.Count);
        
        board.InitializeBoard(presetGenerator);

        var result = EvaluateLife.EvaluateOne(1, 1, board, _rules);
        Assert.Equal(LifeTypes.Heart, result);
    }
    
    [Fact]
    public void SimulationExample3()
    {
        var boardValues = new List<LifeTypes>
        {
            LifeTypes.EitherOr, LifeTypes.EitherOr, LifeTypes.EitherOr,
            LifeTypes.EitherOr, LifeTypes.Heart, LifeTypes.Heart,
            LifeTypes.Heart, LifeTypes.Heart, LifeTypes.Heart,
        };

        var presetGenerator = new PresetPiecesGenerator(TOTAL_ROWS, TOTAL_COLUMNS, boardValues);

        var board = new Board(TOTAL_ROWS, TOTAL_COLUMNS);
        Assert.Equal(TOTAL_COLUMNS * TOTAL_COLUMNS, boardValues.Count);
        
        board.InitializeBoard(presetGenerator);

        var result = EvaluateLife.EvaluateOne(1, 1, board, _rules);
        Assert.Equal(LifeTypes.Skull, result);
    }
    
    [Fact]
    public void SimulationExample4()
    {
        var boardValues = new List<LifeTypes>
        {
            LifeTypes.Skull, LifeTypes.Skull, LifeTypes.Heart,
            LifeTypes.Skull, LifeTypes.Skull, LifeTypes.Heart,
            LifeTypes.Skull, LifeTypes.Skull, LifeTypes.Heart,
        };

        var presetGenerator = new PresetPiecesGenerator(TOTAL_ROWS, TOTAL_COLUMNS, boardValues);

        var board = new Board(TOTAL_ROWS, TOTAL_COLUMNS);
        Assert.Equal(TOTAL_COLUMNS * TOTAL_COLUMNS, boardValues.Count);
        
        board.InitializeBoard(presetGenerator);

        var result = EvaluateLife.EvaluateOne(1, 1, board, _rules);
        Assert.Equal(LifeTypes.Heart, result);
    }
}