using GameOfLife.Modals;

namespace GameOfLife.PieceGenerators;

public interface ILifePieceGenerator
{
    public LifeTypes GetSlotPiece(int rowPos, int colPos);
}