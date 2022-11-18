using GameOfLife.Modals;
using Xunit.Abstractions;

namespace GameOfLife.Test;

public static class Printing
{
    public static void PrettyPrintBoard(IEnumerable<LifeTypes> newValues, int totalColumns, ITestOutputHelper outputHelper)
    {
        var index = 0;
        // foreach (var r in newValues)
        // {
        //     var rowPos = index / TOTAL_ROWS;
        //     var colPos = index % TOTAL_COLUMNS;
        //     _testOutputHelper.WriteLine($"({rowPos}, {colPos}) == {r.ToString()}");
        //     index++;
        // }

        var line = string.Empty;
        foreach (var r in newValues)
        {
            var colPos = index % totalColumns;

            if (colPos == 0)
            {
                line = string.Empty;
            }
            line += "| ";
            if (r == LifeTypes.EitherOr) {
                line += "? ? ?";
            } else {
                line += r.ToString();
            }

            line += " ";

            if (colPos == totalColumns - 1)
            {
                line += "|";
                outputHelper.WriteLine(line);
            }
            
            index++;
        }
    }
    
}