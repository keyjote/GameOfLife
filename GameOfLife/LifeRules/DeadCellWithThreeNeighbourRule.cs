using GameOfLife.Modals;

namespace GameOfLife.LifeRules;

public class DeadCellWithThreeNeighbourRule : ILifeRule
{
    // * A dead cell with exactly three (ALIVE???) neighbours: Becomes alive

    public LifeTypes? Apply(LifeTypes currentCell, IList<LifeTypes> neighbours)
    {
        if (currentCell != LifeTypes.Skull)
        {
            return null;
        }
        
        var aliveNeighbours = neighbours.Count(n => n == LifeTypes.Heart);
        if (aliveNeighbours == 3)
        {
            return LifeTypes.Heart;
        }

        return null;
    }
}