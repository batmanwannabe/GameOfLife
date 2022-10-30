using System.Text;

internal class Program
{
    const int Rows = 25;
    const int Columns = 25;

    static bool runGame = true;

    public static void Main()
    {
        var grid = new AliveOrDead[Rows, Columns];

        for (var row = 0; row < Rows; row++)
        {
            for (var column = 0; column < Columns; column++)
            {
                //Set all values to 0 initially
                grid[row, column] = (AliveOrDead)0;
            }
        }

        grid[12,12] = (AliveOrDead)1;
        grid[12, 13] = (AliveOrDead)1;
        grid[12, 14] = (AliveOrDead)1;
        grid[11, 14] = (AliveOrDead)1;
        grid[10, 13] = (AliveOrDead)1;

        Console.CancelKeyPress += (sender, args) =>
        {
            runGame = false;
        };
        while (runGame)
        {
            Print(grid);
            grid = GenerateNext(grid);
        }
    }

    private static AliveOrDead[,] GenerateNext(AliveOrDead[,] currGrid)
    {
        var nextGeneration = new AliveOrDead[Rows, Columns];

        for (var row = 1; row < Rows - 1; row++)
            for (var column = 1; column < Columns - 1; column++)
            {
                // find alive cells nearby
                var aliveNeighbors = 0;
                for (var i = -1; i <= 1; i++)
                {
                    for (var j = -1; j <= 1; j++)
                    {
                        aliveNeighbors += currGrid[row + i, column + j] == AliveOrDead.Alive ? 1 : 0;
                    }
                }

                var currCell = currGrid[row, column];

                // current cell needs to be subtracted from its neighbors as it was counted before 
                aliveNeighbors -= currCell == AliveOrDead.Alive ? 1 : 0;

                // Rules

                //1
                if (currCell == AliveOrDead.Alive && aliveNeighbors < 2)
                {
                    nextGeneration[row, column] = AliveOrDead.Dead;
                }

                //3
                else if (currCell == AliveOrDead.Alive && aliveNeighbors > 3)
                {
                    nextGeneration[row, column] = AliveOrDead.Dead;
                }

                //4
                else if (currCell == AliveOrDead.Dead && aliveNeighbors == 3)
                {
                    nextGeneration[row, column] = AliveOrDead.Alive;
                }
                //2
                else
                {
                    nextGeneration[row, column] = currCell;
                }
            }
        return nextGeneration;
    }

    private static void Print(AliveOrDead[,] future)
    {
        var stringBuilder = new StringBuilder();
        for (var row = 0; row < Rows; row++)
        {
            for (var column = 0; column < Columns; column++)
            {
                var cell = future[row, column];
                stringBuilder.Append(cell == AliveOrDead.Alive ? " A " : " D ");
            }
            stringBuilder.Append("\n");
        }
        Console.SetCursorPosition(0, 0);
        Console.Write(stringBuilder.ToString());
        Thread.Sleep(500);
    }
}

public enum AliveOrDead
{
    Dead,
    Alive,
}
