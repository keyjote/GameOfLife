using GameOfLife.LifeRules;
using GameOfLife.Modals;

namespace GameOfLife;

public static class EvaluateLife
{
    public static LifeTypes EvaluateOne(int rowPos, int colPos, Board board, IList<ILifeRule> rules)
    {
        var neighbours = board.GetNeighbours(rowPos, colPos);

        foreach (var rule in rules)
        {
            var result = rule.Apply(rowPos, colPos, neighbours);
            if (result != null)
            {
                return result.Value;
            }
        }
        
        // if nothing was found => it is unknown
        return LifeTypes.EitherOr;
    }
}