using UnityEngine;

public class Tile
{
    public int row;
    public int column;
    public TileType type;
    public bool isActive;
    public Vector2 position;

    public Tile(int row, int column, TileType type, bool isActive = true)
    {
        this.row = row;
        this.column = column;
        this.type = type;
        this.isActive = isActive;
    }

    public void SetPosition(Vector2 position)
    {
        this.position = position;
    }

    public void ReplaceTile(TileType type)
    {
        this.type = type;
    }
}
