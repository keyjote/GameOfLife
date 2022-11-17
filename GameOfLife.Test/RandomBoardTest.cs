using GameOfLife.LifeRules;
using GameOfLife.Modals;
using GameOfLife.PieceGenerators;
using Xunit.Abstractions;

namespace GameOfLife.Test;

public class RandomBoardTest
{
    
    private readonly ITestOutputHelper _testOutputHelper;
    private const int TOTAL_ROWS = 3;
    private const int TOTAL_COLUMNS = 3;
    private readonly List<ILifeRule> _rules;
    
    public RandomBoardTest(ITestOutputHelper testOutputHelper)
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
    public void RunLiveWithRandomizedInitialBoard()
    {
        var randomGenerator = new RandomPiecesGenerator(TOTAL_ROWS, TOTAL_COLUMNS, true);

        var baseBoard = new Board(TOTAL_ROWS, TOTAL_COLUMNS);
        
        baseBoard.InitializeBoard(randomGenerator);
        var resultValues = baseBoard.GetPieces().ToList();
        {
            var index = 0;
            _testOutputHelper.WriteLine($"(Base Run #0");
            foreach (var r in resultValues) 
            {
                var rowPos = index / TOTAL_ROWS;
                var colPos = index % TOTAL_COLUMNS;
                _testOutputHelper.WriteLine($"({rowPos}, {colPos}) == {r.ToString()}");
                index++;
            }
        }

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