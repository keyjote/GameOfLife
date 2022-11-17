using GameOfLife.Modals;

namespace GameOfLife.PieceGenerators;

public class RandomPiecesGenerator: ILifePieceGenerator
{
    private readonly int _rows;
    private readonly int _cols;
    private readonly bool _memoryEnabled;
    private readonly Dictionary<string, LifeTypes> _memory;
    private readonly Random _random;

    public RandomPiecesGenerator(int rows, int cols, bool memoryEnabled)
    {
        _rows = rows;
        _cols = cols;
        _memoryEnabled = memoryEnabled;
        _memory = new Dictionary<string, LifeTypes>();
        _random = new Random();
    }
    public LifeTypes GetSlotPiece(int rowPos, int colPos)
    {
        if (rowPos < 0 || rowPos >= _rows)
        {
            throw new AggregateException("The row value is out of bounds");
        }
        if (colPos < 0 || colPos >= _cols)
        {
            throw new AggregateException("The column value is out of bounds");
        }

        var key = $"{rowPos}-{colPos}";
        if (_memoryEnabled)
        {
            if (_memory.ContainsKey(key))
            {
                return _memory[key];
            }
        }

        var value = (LifeTypes)_random.Next(0, 3);
        if (_memoryEnabled)
        {
            _memory[key] = value;
        }

        return value;
    }
}