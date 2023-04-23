using System;
using System.Collections.Generic;

public class GameBoardModel
{
    public GameBoardModel(int boardSize)
    {
        BoardSize = boardSize;
        tiles = new Tile[boardSize, boardSize];
        InitializeTiles();
    }

    private Tile[,] tiles;
    public Tile[,] Tiles { get { return tiles; } }
    public int BoardSize { get; private set; }
    List<Tile> matcheTiles = new List<Tile>();


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

    public List<Tile> GetMatches(Tile selectedTile)
    {
        matcheTiles.Clear();

        return CheckForMatches(selectedTile);
    }

    private List<Tile> CheckForMatches(Tile selectedTile)
    {
        if (!matcheTiles.Contains(selectedTile))
        {
            matcheTiles.Add(selectedTile);
        }

        for (int i = selectedTile.column + 1; i < tiles.GetLength(0); i++)
        {
            var currentTile = tiles[selectedTile.row, i];
            if (currentTile.isActive && currentTile.type == selectedTile.type)
            {
                if (matcheTiles.Contains(currentTile)) continue;

                matcheTiles.Add(currentTile);
                CheckForMatches(currentTile);
            }
            else
            {
                break;
            }
        }

        for (int i = selectedTile.column - 1; i >= 0; i--)
        {
            var currentTile = tiles[selectedTile.row, i];
            if (currentTile.isActive && currentTile.type == selectedTile.type)
            {
                if (matcheTiles.Contains(currentTile)) continue;

                matcheTiles.Add(currentTile);
                CheckForMatches(currentTile);
            }
            else
            {
                break;
            }
        }

        for (int i = selectedTile.row + 1; i < tiles.GetLength(1); i++)
        {
            var currentTile = tiles[i, selectedTile.column];
            if (currentTile.isActive && currentTile.type == selectedTile.type)
            {
                if (matcheTiles.Contains(currentTile)) continue;

                matcheTiles.Add(currentTile);
                CheckForMatches(currentTile);
            }
            else
            {
                break;
            }
        }

        for (int i = selectedTile.row - 1; i >= 0; i--)
        {
            var currentTile = tiles[i, selectedTile.column];
            if (currentTile.isActive && currentTile.type == selectedTile.type)
            {
                if (matcheTiles.Contains(currentTile)) continue;

                matcheTiles.Add(currentTile);
                CheckForMatches(currentTile);
            }
            else
            {
                break;
            }
        }

        return matcheTiles;
    }
}


//just Add to sameList active tiles. or remove active tiles from tiles. 
//Read levels from json file 
// every level has a array of deactive tiles + amount of pop to win 