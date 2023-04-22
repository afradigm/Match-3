using UnityEngine;

public class Tile
{
    public int row;
    public int column;
    public TileType type;

    public Tile(int row, int column, TileType type)
    {
        this.row = row;
        this.column = column;
        this.type = type;
    }
}
