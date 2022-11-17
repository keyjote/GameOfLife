using GameOfLife.Modals;

namespace GameOfLife.LifeRules;

public class LivingCellWithTwoOrThreeNeighbourRule : ILifeRule
{
    // * A living cell with two or three (ALIVE???) neighbours: Survives

    public LifeTypes? Apply(int row, int column, IList<LifeTypes> neighbours)
    {
        var aliveNeighbours = neighbours.Count(n => n == LifeTypes.Heart);
        if (aliveNeighbours is >= 2 and <= 3)
        {
            return LifeTypes.Heart;
        }

        return null;
    }
}