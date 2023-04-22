using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GameBoardPresenter : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int amountOfTiles = 9;
    [SerializeField] private float referenceSize = 10.0f;
    [SerializeField] private float tileSpacing = 0.22f;
    [SerializeField] private Vector3 startPosition = Vector3.zero;
    [Header("References")]
    [Space(20)]
    [SerializeField] private TileView tileSample;
    [SerializeField] private GameBoardModel gameBoard;
    [SerializeField] private List<ArtStruct> arts = new List<ArtStruct>();


    private void Start()
    {
        Initialize(amountOfTiles);
    }

    public void Initialize(int boardSize)
    {
        gameBoard = new GameBoardModel(boardSize);
        GenerateBoard();
    }

    private void GenerateBoard()
    {
        for (int row = 0; row < gameBoard.Tiles.GetLength(0); row++)
        {
            for (int column = 0; column < gameBoard.Tiles.GetLength(1); column++)
            {
                Tile tile = gameBoard.Tiles[row, column];
                TileView newTile = Instantiate(tileSample, transform);
                int tileIndex = row * gameBoard.Tiles.GetLength(1) + column;
                float delay = tileIndex * 0.01f; // Change the delay time as you wish
                newTile.name = row + "-" + column;
                float tileScale = GetTileScale();
                newTile.Setup(tileData: tile, scale: tileScale, position: GetTilePosition(tileScale, row, column), sprite: GetArt(tile.type), sortingOrder: (gameBoard.Tiles.GetLength(0) - row), delay);
            }
        }
    }

    private Sprite GetArt(TileType type)
    {
        return arts.Find(x => x.type == type).sprite;
    }

    private float GetTileScale()
    {
        float tileScale = Mathf.Abs(referenceSize / ((tileSample.GetRealScale().x + tileSpacing) * amountOfTiles));
        return tileScale;
    }

    private Vector2 GetTilePosition(float tileScale, int row, int column)
    {
        Vector2 topLeftPosition = CalculateTopLeftPosition(tileScale);
        Vector2 tilePosition = new Vector2(topLeftPosition.x + column * (tileScale + tileSpacing), topLeftPosition.y - row * (tileScale + tileSpacing));
        return tilePosition;
    }

    private Vector2 CalculateTopLeftPosition(float tileScale)
    {
        Vector2 topLeftPosition = new Vector2(startPosition.x - referenceSize / 2.0f + tileScale / 2.0f, startPosition.y + referenceSize / 2.0f - tileScale / 2.0f);
        return topLeftPosition;
    }
}



[Serializable]
public struct ArtStruct
{
    public TileType type;
    public Sprite sprite;
}
