using GameOfLife.Modals;

namespace GameOfLife.LifeRules;

public class DeadCellWithThreeNeighbourRule : ILifeRule
{
    // * A dead cell with exactly three (ALIVE???) neighbours: Becomes alive

    public LifeTypes? Apply(int row, int column, IList<LifeTypes> neighbours)
    {
        var aliveNeighbours = neighbours.Count(n => n == LifeTypes.Heart);
        if (aliveNeighbours == 3)
        {
            return LifeTypes.Heart;
        }

        return null;
    }
}