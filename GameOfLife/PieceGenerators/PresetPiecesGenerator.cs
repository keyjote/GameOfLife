using GameOfLife.Modals;

namespace GameOfLife.PieceGenerators;

public class PresetPiecesGenerator: ILifePieceGenerator
{
    private readonly int _rows;
    private readonly int _columns;
    private readonly IList<LifeTypes> _values;

    public PresetPiecesGenerator(int rows, int columns, IList<LifeTypes> values)
    {
        if (rows == 0)
        {
            throw new ArgumentException("Rows cannot have the value 0 or less");
        }
        if (columns == 0)
        {
            throw new ArgumentException("Columns cannot have the value 0 or less");
        }
        if (values.Count < rows * columns)
        {
            throw new ArgumentException("There are less values in the list than is is required. (rows * columns IS MORE THAN values.Count)");
        }

        _rows = rows;
        _columns = columns;
        _values = values;
    }
    
    public LifeTypes GetSlotPiece(int rowPos, int colPos)
    {
        var index = rowPos * _columns + colPos;
        return _values[index];
    }
}