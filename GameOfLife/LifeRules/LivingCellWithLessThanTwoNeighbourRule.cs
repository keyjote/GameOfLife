using GameOfLife.Modals;

namespace GameOfLife.LifeRules;

public class LivingCellWithLessThanTwoNeighbourRule : ILifeRule
{
    // * A living cell with less than two (ALIVE???) neighbours: Dies
    public LifeTypes? Apply(LifeTypes currentCell, IList<LifeTypes> neighbours)
    {
        if (currentCell != LifeTypes.Heart)
        {
            return null;
        }
        
        var aliveNeighbours = neighbours.Count(n => n == LifeTypes.Heart);
        if (aliveNeighbours < 2)
        {
            return LifeTypes.Skull;
        }

        return null;
    }
}