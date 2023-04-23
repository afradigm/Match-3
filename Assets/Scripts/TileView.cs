using DG.Tweening;
using System;
using UnityEngine;

public class TileView : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    private Tile tileData;
    private bool isClickable = true;
    private Action<Tile> OnClick;

    public void Setup(Tile tileData, float scale, Vector2 position, Sprite sprite, int sortingOrder, float delay, Action<Tile> OnClick)
    {
        this.tileData = tileData;
        this.OnClick = OnClick;

        transform.localScale = new Vector2(scale, scale);
        transform.localPosition = position;
        spriteRenderer.sprite = sprite;
        SetSortingLayer(sortingOrder);
        transform.name = $"{tileData.row} - {tileData.column}";
        transform.DOPunchScale(new Vector3(0.1f, 0.1f, 0f), 0.5f, 10, 0.5f).SetDelay(delay).OnStart(() =>
        {
            SetActive(true);
        });
    }

    public void SetActive(bool value)
    {
        gameObject.SetActive(value);
    }

    public void SetSortingLayer(int sortingOrder)
    {
        spriteRenderer.sortingOrder = sortingOrder;
    }

    public void TweenOnClick()
    {
        if (isClickable)
        {
            isClickable = false;
            transform.DOPunchScale(new Vector3(0.1f, 0.1f, 0f), 0.5f, 10, 0.5f).OnComplete(() =>
            {
                isClickable = true;
            });
        }
    }

    public void SetOnClickableActive(bool value)
    {
        isClickable = value;
    }

    public void OnMouseUp()
    {
        OnClick?.Invoke(tileData);
    }

    public Vector2 GetRealScale()
    {
        return spriteRenderer.size;
    }
}
