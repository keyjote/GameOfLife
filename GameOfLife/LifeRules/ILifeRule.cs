using GameOfLife.Modals;

namespace GameOfLife.LifeRules;

public interface ILifeRule
{
    public LifeTypes? Apply(int row, int column, IList<LifeTypes> neighbours);
}