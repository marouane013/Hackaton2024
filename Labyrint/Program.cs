using System;
using System.Collections.Generic;

public class MazeSolver
{
    public int SolveMaze(char[][] maze)
    {
        int rows = maze.Length;
        int cols = maze[0].Length;
        bool[,] visited = new bool[rows, cols];
        int startRow = -1, startCol = -1, endRow = -1, endCol = -1;

        // Vind de start- en eindposities
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (maze[i][j] == 'S')
                {
                    startRow = i;
                    startCol = j;
                }
                else if (maze[i][j] == 'E')
                {
                    endRow = i;
                    endCol = j;
                }
            }
        }

        // Controleer of start- en eindpunt zijn gevonden
        if (startRow == -1 || startCol == -1 || endRow == -1 || endCol == -1)
        {
            throw new Exception("Start- of eindpunt ontbreekt in het doolhof.");
        }

        return BFS(maze, visited, startRow, startCol, endRow, endCol);
    }

    private int BFS(char[][] maze, bool[,] visited, int startRow, int startCol, int endRow, int endCol)
    {
        int rows = maze.Length;
        int cols = maze[0].Length;
        Queue<(int, int, int)> queue = new Queue<(int, int, int)>();
        queue.Enqueue((startRow, startCol, 0));
        visited[startRow, startCol] = true;

        int[][] directions = new int[][]
        {
            new int[] {-1, 0}, // Up
            new int[] {1, 0},  // Down
            new int[] {0, -1}, // Left
            new int[] {0, 1}   // Right
        };

        while (queue.Count > 0)
        {
            var (row, col, steps) = queue.Dequeue();

            if (row == endRow && col == endCol)
                return steps;

            foreach (var direction in directions)
            {
                int newRow = row + direction[0];
                int newCol = col + direction[1];

                if (newRow >= 0 && newRow < rows && newCol >= 0 && newCol < cols &&
                    !visited[newRow, newCol] && maze[newRow][newCol] != '#' && maze[newRow][newCol] != 'B')
                {
                    visited[newRow, newCol] = true;
                    queue.Enqueue((newRow, newCol, steps + 1));
                }
            }
        }

        return int.MaxValue; // Geen pad gevonden
    }

    // Voorbeeldgebruik
    public static void Main(string[] args)
    {
        char[][] maze = new char[][]
        {
    new char[] {'S', '·', '·', '·', '#', '·', '#', '·', '·', '·'},
    new char[] {'#', '·', '#', '·', '·', '·', '#', '#', '·', '·'},
    new char[] {'·', '#', '#', '·', '#', '·', '·', '·', '·', '#'},
    new char[] {'·', '·', '·', '#', '·', '#', '#', '#', '·', '·'},
    new char[] {'#', '#', '·', '·', '·', '·', '·', '·', 'B', '·'},
    new char[] {'·', '·', '·', '#', '#', '·', '#', '·', '·', '·'},
    new char[] {'#', '#', '·', '#', '·', 'B', '·', '#', '#', '·'},
    new char[] {'·', '#', '·', '·', '·', 'B', '·', '·', '·', 'B'},
    new char[] {'·', '·', '·', '#', '·', '·', '·', '#', '·', 'E'}
        };


        var solver = new MazeSolver();
        int minSteps = solver.SolveMaze(maze);
        Console.WriteLine($"Minimum stappen om het doolhof op te lossen: {minSteps}");
    }
}
