 using System.Collections.Generic;
using System.Linq;

namespace _Main.Scripts._Base.GridSystem
{
    public static class FindNeighbours<T> where T : IGridItem<T>
    {
        private static void FindNeighboursTile(T[,] grid, int xSixe, int ySize)
        {
            var directions = new int[,]
            {
                { 0, 1 },
                { 1, 0 },
                { 0, -1 },
                { -1, 0 }
            };

            for (var x = 0; x < grid.GetLength(0); x++)
            {
                for (var y = 0; y < grid.GetLength(1); y++)
                {
                    var currentTile = grid[x, y];
                    foreach (var dir in Enumerable.Range(0, directions.GetLength(0)))
                    {
                        var newX = x + directions[dir, 0];
                        var newY = y + directions[dir, 1];

                        if (newX < 0 || newX >= xSixe || newY < 0 || newY >= ySize) continue;
                        var neighborTile = grid[newX, newY];
                        if (neighborTile != null)
                        {
                            currentTile.UpdateNeighbour(neighborTile);
                        }
                    }
                }
            }
        }
    }
}
public interface IGridItem<T>
{
    void UpdateNeighbour(T neighbor);
}

public class Tile : IGridItem<Tile>
{
    private List<Tile> neighbours = new List<Tile>();

    public void UpdateNeighbour(Tile neighbor)
    {
        neighbours.Add(neighbor);
    }

    public List<Tile> GetNeighbours()
    {
        return neighbours;
    }

}