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
    [SerializeField] private GameBoardModel logic;
    [SerializeField] private List<ArtStruct> arts = new List<ArtStruct>();


    private void Start()
    {
        Initialize(amountOfTiles);
    }

    public void Initialize(int boardSize)
    {
        logic = new GameBoardModel(boardSize);
        GenerateBoard();
    }

    private void GenerateBoard()
    {
        for (int row = 0; row < logic.Tiles.GetLength(0); row++)
        {
            for (int column = 0; column < logic.Tiles.GetLength(1); column++)
            {
                Tile tile = logic.Tiles[row, column];
                TileView newTile = Instantiate(tileSample, transform);
                int tileIndex = row * logic.Tiles.GetLength(1) + column;
                float delay = tileIndex * 0.01f; // Change the delay time as you wish
                float tileScale = GetTileScale();
                newTile.Setup(tileData: tile, scale: tileScale, position: GetTilePosition(tileScale, row, column),
                    sprite: GetArt(tile.type), sortingOrder: (logic.Tiles.GetLength(0) - row), delay, OnTileClick);
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
        float xPos = ((referenceSize / (amountOfTiles)) * column) + (startPosition.x + tileScale);
        float yPos = (((referenceSize / (amountOfTiles)) * row) - (startPosition.y + tileScale)) * -1;
        Vector2 tilePosition = new Vector2(xPos, yPos);
        return tilePosition;
    }

    private void OnTileClick(Tile tileData)
    {
        var matcheTiles = logic.GetMatches(tileData);

        for (int i = 0; i < matcheTiles.Count; i++)
        {
            Debug.Log($"{matcheTiles[i].row} - {matcheTiles[i].column}");
        }
    }
}



[Serializable]
public struct ArtStruct
{
    public TileType type;
    public Sprite sprite;
}
