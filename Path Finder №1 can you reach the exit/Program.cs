using System.Collections.Generic;

public class Finder
{
    public static void Main()
    {
        string str = ".W.\n" +
                     ".W.\n" +
                     "...";

        // Test
        var t = PathFinder(str);
        // ...should return true
    }

    public static bool PathFinder(string maze)
    {
        if (maze.Length == 1)
            return true;

        int mapSize = maze.IndexOf('\n');
        int[,] map = new int[mapSize, mapSize];
        (int, int) index = (0, 0);
        Queue<(int, int)> cells = new();
        cells.Enqueue(index);
        map[0, 0] = 1;

        foreach (char c in maze)
        {
            if (c == '.')
                index.Item2++;

            else if (c == 'W')
                map[index.Item1, index.Item2++] = -1;

            else index = (index.Item1 + 1, 0);
        }

        while (cells.Count != 0 && map[mapSize - 1, mapSize - 1] != 1)
        {
            var cell = cells.Dequeue();
            TryAddCell((cell.Item1 - 1, cell.Item2), map, cells);
            TryAddCell((cell.Item1 + 1, cell.Item2), map, cells);
            TryAddCell((cell.Item1, cell.Item2 - 1), map, cells);
            TryAddCell((cell.Item1, cell.Item2 + 1), map, cells);
        }

        return map[mapSize - 1, mapSize - 1] == 1;
    }

    public static void TryAddCell((int, int) index, int[,] map, Queue<(int, int)> cells)
    {
        if (index.Item1 < 0 || index.Item1 >= map.GetLength(0) ||
            index.Item2 < 0 || index.Item2 >= map.GetLength(0) ||
            map[index.Item1, index.Item2] != 0)
            return;

        cells.Enqueue((index.Item1, index.Item2));
        map[index.Item1, index.Item2] = 1;
    }
}