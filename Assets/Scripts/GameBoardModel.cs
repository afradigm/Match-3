using System;
using System.Collections.Generic;
using System.Linq;

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
        CheckForMatches(selectedTile);

        return matcheTiles;
    }

    private void CheckForMatches(Tile selectedTile)
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
    }

    public void RefillBoard(List<Tile> matches)
    {
        List<Tile> uniqueColumns = matches.GroupBy(x => x.column).Select(x => x.First()).ToList();

        for (int i = 0; i < uniqueColumns.Count; i++)
        {
            List<Tile> uniqueRowsInEachColumn = matches.FindAll(x => x.column == uniqueColumns[i].column);
            if (uniqueRowsInEachColumn.Count > 1)
            {
                uniqueRowsInEachColumn.Sort((x, y) => y.row.CompareTo(x.row));  //Sort from large to small
            }

            for (int j = 0; j < uniqueRowsInEachColumn.Count; j++)
            {
                //from buttom to top.
            }




        }
    }
}


//just Add to sameList active tiles. or remove active tiles from tiles. 
//Read levels from json file 
// every level has a array of deactive tiles + amount of pop to win 