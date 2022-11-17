using GameOfLife.Modals;

namespace GameOfLife.LifeRules;

public class LivingCellWithLessThanTwoNeighbourRule : ILifeRule
{
    // * A living cell with less than two (ALIVE???) neighbours: Dies
    public LifeTypes? Apply(int row, int column, IList<LifeTypes> neighbours)
    {
        var aliveNeighbours = neighbours.Count(n => n == LifeTypes.Heart);
        if (aliveNeighbours < 2)
        {
            return LifeTypes.Skull;
        }

        return null;
    }
}