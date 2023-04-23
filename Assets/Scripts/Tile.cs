using UnityEngine;

public class Tile
{
    public int row;
    public int column;
    public TileType type;
    public bool isActive;

    public Tile(int row, int column, TileType type, bool isActive = true)
    {
        this.row = row;
        this.column = column;
        this.type = type;
        this.isActive = isActive;
    }
}
