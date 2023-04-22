using System;

public class GameBoardModel
{
    private TileType tileType;
    private Tile[,] tiles;
    public Tile[,] Tiles { get { return tiles; } }
    public int BoardSize { get; private set; }
    public GameBoardModel(int boardSize)
    {
        BoardSize = boardSize;
        tiles = new Tile[boardSize, boardSize];
        InitializeTiles();
    }

    private void InitializeTiles()
    {
        for (int row = 0; row < BoardSize; row++)
        {
            for (int column = 0; column < BoardSize; column++)
            {
                tiles[row, column] = new Tile(row, column, GetRandomTileType());
            }
        }
    }

    private TileType GetRandomTileType()
    {
        TileType[] types = (TileType[])Enum.GetValues(typeof(TileType));
        int randomIndex = UnityEngine.Random.Range(0, types.Length);
        return types[randomIndex];
    }
}
