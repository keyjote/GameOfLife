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

    
    // TODO: fix following tests, they do not test anything, they just run things
    // Will have to run the epochs manually in order to be able to predict its end result
    // This is just for show that is can be done, but not a real test
    
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

        var resultValues = new List<LifeTypes>();
        var numberOfCells = TOTAL_ROWS * TOTAL_COLUMNS;

        for (var index = 0; index < numberOfCells; index++)
        {
            var rowPos = index / TOTAL_ROWS;
            var colPos = index % TOTAL_ROWS;
            
            var result = EvaluateLife.EvaluateOne(rowPos, colPos, board, _rules);
            resultValues.Add(result);
            _testOutputHelper.WriteLine($"({rowPos}, {colPos}) == {result.ToString()}");
        }
        
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
        const int maxRuns = 3;
        var runNumber = 0;
        while (runNumber < maxRuns)
        {
            _testOutputHelper.WriteLine($"(Run #{runNumber + 1}");
            
            var presetGenerator = new PresetPiecesGenerator(TOTAL_ROWS, TOTAL_COLUMNS, resultValues);

            var board = new Board(TOTAL_ROWS, TOTAL_COLUMNS);
            board.InitializeBoard(presetGenerator);
            
            var newValues = EvaluateLife.EvaluateBoard(board, _rules);

            var index = 0;
            foreach (var r in newValues) 
            {
                var rowPos = index / TOTAL_ROWS;
                var colPos = index % TOTAL_COLUMNS;
                _testOutputHelper.WriteLine($"({rowPos}, {colPos}) == {r.ToString()}");
                index++;
            }

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
        const int maxRuns = 3;
        var runNumber = 0;
        while (runNumber < maxRuns)
        {
            _testOutputHelper.WriteLine($"(Run #{runNumber + 1}");
            
            var presetGenerator = new PresetPiecesGenerator(TOTAL_ROWS, TOTAL_COLUMNS, resultValues);

            var board = new Board(TOTAL_ROWS, TOTAL_COLUMNS);
            board.InitializeBoard(presetGenerator);
            
            var newValues = EvaluateLife.EvaluateBoard(board, _rules);

            var index = 0;
            foreach (var r in newValues) 
            {
                var rowPos = index / TOTAL_ROWS;
                var colPos = index % TOTAL_COLUMNS;
                _testOutputHelper.WriteLine($"({rowPos}, {colPos}) == {r.ToString()}");
                index++;
            }

            resultValues = newValues.ToList();
            runNumber++;
        }
    }

}