using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GameBoardPresenter : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int boardSize = 9;
    [SerializeField] private float referenceSize = 10.0f;
    [SerializeField] private float tileSpacing = 0.22f;
    [SerializeField] private int minMatchSize = 2;
    [SerializeField] private Vector3 startPosition = Vector3.zero;
    [Header("References")]
    [Space(20)]
    [SerializeField] private TileView tileSample;
    [SerializeField] private GameBoardModel logic;
    [SerializeField] private List<ArtStruct> arts = new List<ArtStruct>();

    private TileView[,] tilesView;
    private List<TileView> tilesMatchesView;
    private bool isRefillBoard = false;


    private void Start()
    {
        Initialize(boardSize);
    }

    public void Initialize(int boardSize)
    {
        logic = new GameBoardModel(boardSize);
        tilesView = new TileView[boardSize, boardSize];
        tilesMatchesView = new List<TileView>();

        GenerateBoard();
    }

    private void GenerateBoard()
    {
        for (int row = 0; row < logic.Tiles.GetLength(0); row++)
        {
            for (int column = 0; column < logic.Tiles.GetLength(1); column++)
            {
                Tile tileData = logic.Tiles[row, column];
                TileView newTile = Instantiate(tileSample, transform);
                int tileIndex = row * logic.Tiles.GetLength(1) + column;
                float delay = tileIndex * 0.01f; // Change the delay time as you wish
                float tileScale = GetTileScale();
                Vector2 position = GetTilePosition(tileScale, row, column);
                tileData.SetPosition(position);
                newTile.Setup(tileData, tileScale, GetArt(tileData.type), GetSortingOrder(row), delay, OnTileClick);

                tilesView[row, column] = newTile;
            }
        }
    }

    private int GetSortingOrder(int row)
    {
        return (logic.Tiles.GetLength(0) - row);
    }

    private Sprite GetArt(TileType type)
    {
        return arts.Find(x => x.type == type).sprite;
    }

    private float GetTileScale()
    {
        float tileScale = Mathf.Abs(referenceSize / ((tileSample.GetRealScale().x + tileSpacing) * boardSize));
        return tileScale;
    }

    private Vector2 GetTilePosition(float tileScale, int row, int column)
    {
        float xPos = ((referenceSize / (boardSize)) * column) + (startPosition.x + tileScale);
        float yPos = (((referenceSize / (boardSize)) * row) - (startPosition.y + tileScale)) * -1;
        Vector2 tilePosition = new Vector2(xPos, yPos);
        return tilePosition;
    }

    private void OnTileClick(Tile tileData)
    {
        var matcheTiles = logic.GetMatches(tileData);
        if (matcheTiles.Count >= minMatchSize)
        {
            TweenPopEffect(matcheTiles);
            isRefillBoard = false;
        }
        else
        {
            TileView tile = tilesView[tileData.row, tileData.column];
            tile.TweenOnClick();
        }

        for (int i = 0; i < matcheTiles.Count; i++)
        {
           // Debug.Log($"{matcheTiles[i].row} - {matcheTiles[i].column}");
        }
    }

    private void TweenPopEffect(List<Tile> matcheTiles)
    {
        for (int i = 0; i < matcheTiles.Count; i++)
        {
            int row = matcheTiles[i].row;
            int column = matcheTiles[i].column;
            TileView tile = tilesView[row, column];
            tilesMatchesView.Add(tile);
            tile.SetSortingLayer((boardSize * boardSize * (i + 1)));
            tile.SetOnClickableActive(false);
            float endTweenValue = tile.transform.localScale.x;
            tile.transform.localScale = Vector3.zero;
            tile.transform.DOScale(endTweenValue * 1.2f, 0.5f).SetEase(Ease.OutQuad).SetId("pop").OnComplete(() =>
            {
                tile.transform.DOScale(endTweenValue * 0.2f, 0.1f).SetEase(Ease.InQuad).SetId("pop").OnComplete(() =>
                {
                    if (i == matcheTiles.Count && !isRefillBoard)
                    {
                        DOTween.Kill("pop");
                        logic.RefillBoard(new List<Tile>(matcheTiles), OnRefillBoard);
                        isRefillBoard = true;

                        Debug.Log("refill view");
                    }
                });
            });
        }
    }

    private void OnRefillBoard(Tile UpestTile, Tile bottomestTile)
    {
        TileView bottomestTileView = tilesView[bottomestTile.row, bottomestTile.column];

        if (UpestTile == null)
        {
            bottomestTileView.SetScale().SetSpriteRenderer(GetArt(bottomestTile.type)).TweenMove(10f, bottomestTile.position.y);
        }
        else
        {
            bottomestTileView.SetScale().SetSpriteRenderer(GetArt(UpestTile.type)).TweenMove(UpestTile.position.y, bottomestTile.position.y);
        }
    }
}



[Serializable]
public struct ArtStruct
{
    public TileType type;
    public Sprite sprite;
}
