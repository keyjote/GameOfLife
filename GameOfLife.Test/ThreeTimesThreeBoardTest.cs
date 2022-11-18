using GameOfLife.LifeRules;
using GameOfLife.Modals;
using GameOfLife.PieceGenerators;
using Xunit.Abstractions;

namespace GameOfLife.Test;

public class ThreeTimesThreeBoardTest
{
    private readonly ITestOutputHelper _testOutputHelper;
    private const int TOTAL_ROWS = 3;
    private const int TOTAL_COLUMNS = 3;
    private readonly List<ILifeRule> _rules;
    public ThreeTimesThreeBoardTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
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

    
    [Fact]
    public void RunLifeOnce()
    {
        //using case #2
        var boardValues = new List<LifeTypes>
        {
            LifeTypes.Skull, LifeTypes.EitherOr, LifeTypes.Heart,
            LifeTypes.Skull, LifeTypes.Heart, LifeTypes.Heart,
            LifeTypes.Skull, LifeTypes.Skull, LifeTypes.Skull,
        };

        var presetGenerator = new PresetPiecesGenerator(TOTAL_ROWS, TOTAL_COLUMNS, boardValues);

        var board = new Board(TOTAL_ROWS, TOTAL_COLUMNS);
        board.InitializeBoard(presetGenerator);

        var resultValues = EvaluateLife.EvaluateBoard(board, _rules);
        Printing.PrettyPrintBoard(resultValues, TOTAL_COLUMNS, _testOutputHelper);
        
        Assert.Equal(LifeTypes.Heart, resultValues[4]); // (1,1)
    }
    
    [Fact]
    public void RunLifeThreeTimes()
    {
        //using case #2
        var boardValues = new List<LifeTypes>
        {
            LifeTypes.Skull, LifeTypes.EitherOr, LifeTypes.Heart,
            LifeTypes.Skull, LifeTypes.Heart, LifeTypes.Heart,
            LifeTypes.Skull, LifeTypes.Skull, LifeTypes.Skull,
        };

        var resultValues = boardValues.ToList();
        _testOutputHelper.WriteLine($"(Run #0");
        Printing.PrettyPrintBoard(resultValues, TOTAL_COLUMNS, _testOutputHelper);
        const int maxRuns = 3;
        var runNumber = 0;
        while (runNumber < maxRuns)
        {
            _testOutputHelper.WriteLine($"(Run #{runNumber + 1}");
            
            var presetGenerator = new PresetPiecesGenerator(TOTAL_ROWS, TOTAL_COLUMNS, resultValues);

            var board = new Board(TOTAL_ROWS, TOTAL_COLUMNS);
            board.InitializeBoard(presetGenerator);
            
            var newValues = EvaluateLife.EvaluateBoard(board, _rules);
            Printing.PrettyPrintBoard(newValues, TOTAL_COLUMNS, _testOutputHelper);

            resultValues = newValues.ToList();
            runNumber++;
        }
    }
    
    [Fact]
    public void RunLifeThreeTimes_e3()
    {
        //using case #3
        var boardValues = new List<LifeTypes>
        {
            LifeTypes.EitherOr, LifeTypes.EitherOr, LifeTypes.EitherOr,
            LifeTypes.EitherOr, LifeTypes.Heart, LifeTypes.Heart,
            LifeTypes.Heart, LifeTypes.Heart, LifeTypes.Heart,
        };

        var resultValues = boardValues.ToList();
        _testOutputHelper.WriteLine($"(Run #0");
        Printing.PrettyPrintBoard(resultValues, TOTAL_COLUMNS, _testOutputHelper);
        
        const int maxRuns = 3;
        var runNumber = 0;
        while (runNumber < maxRuns)
        {
            _testOutputHelper.WriteLine($"(Run #{runNumber + 1}");
            
            var presetGenerator = new PresetPiecesGenerator(TOTAL_ROWS, TOTAL_COLUMNS, resultValues);

            var board = new Board(TOTAL_ROWS, TOTAL_COLUMNS);
            board.InitializeBoard(presetGenerator);
            
            var newValues = EvaluateLife.EvaluateBoard(board, _rules);

            Printing.PrettyPrintBoard(newValues, TOTAL_COLUMNS, _testOutputHelper);

            resultValues = newValues.ToList();
            runNumber++;
        }
    }

}