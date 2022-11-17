using GameOfLife.Modals;

namespace GameOfLife.LifeRules;

public interface ILifeRule
{
    public LifeTypes? Apply(LifeTypes currentCell, IList<LifeTypes> neighbours);
}