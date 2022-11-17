using GameOfLife.LifeRules;
using GameOfLife.PieceGenerators;
using Microsoft.VisualBasic.CompilerServices;

namespace GameOfLife.Modals;

public class Board
{
    public int Rows { get; }
    public int Columns { get; }

    private readonly IList<IList<LifeTypes>> _board;

    public Board(int rows, int columns)
    {
        Rows = rows;
        Columns = columns;
        _board = new List<IList<LifeTypes>>();
    }

    public void InitializeBoard(ILifePieceGenerator pieceGenerator)
    {
        for (var rowIndex = 0; rowIndex < Rows; rowIndex++)
        {
            var row = new List<LifeTypes>();
            _board.Add(row);

            for (var columnIndex = 0; columnIndex < Columns; columnIndex++)
            {
                row.Add(pieceGenerator.GetSlotPiece(rowIndex, columnIndex));
            }
        }
    }

    public IList<LifeTypes> GetNeighbours(int row, int col)
    {
        CheckRowColInput(row, col);

        var leftLimit = (col == 0) ? row : col - 1;
        var rightLimit = (col == Columns - 1) ? Columns - 1 : col + 1;
        
        var topLimit = (row == 0) ? row : row - 1;
        var bottomLimit = (row == Rows - 1) ? Rows - 1 : row + 1;

        var neighbours = new List<LifeTypes>();
        for (var rowIndex = topLimit; rowIndex <= bottomLimit; rowIndex++)
        {
            for (var colIndex = leftLimit; colIndex <= rightLimit; colIndex++)
            {
                if (row == rowIndex && col == colIndex)
                {
                    continue;
                }
                
                neighbours.Add(_board[rowIndex][colIndex]);
            }
        }

        return neighbours;
    }

    // public LifeTypes GetPiece(int row, int col)
    // {
    //     CheckRowColInput(row, col);
    //
    //     return _board[row][col];
    // }

    private void CheckRowColInput(int row, int col)
    {
        if (_board == null)
        {
            throw new IncompleteInitialization();
        }

        if (row < 0 || row >= Rows)
        {
            throw new AggregateException("The row value is out of bounds");
        }

        if (col < 0 || col >= Columns)
        {
            throw new AggregateException("The column value is out of bounds");
        }
    }
}