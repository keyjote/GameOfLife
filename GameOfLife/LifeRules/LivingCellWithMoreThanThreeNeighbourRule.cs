using GameOfLife.Modals;

namespace GameOfLife.LifeRules;

public class LivingCellWithMoreThanThreeNeighbourRule : ILifeRule
{
    // * A living cell with more than three (ALIVE???) neighbours: Dies


    public LifeTypes? Apply(int row, int column, IList<LifeTypes> neighbours)
    {
        var aliveNeighbours = neighbours.Count(n => n == LifeTypes.Heart);
        if (aliveNeighbours > 3)
        {
            return LifeTypes.Skull;
        }

        return null;
    }
}