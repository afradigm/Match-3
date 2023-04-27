using DG.Tweening;
using System;
using UnityEngine;

public class TileView : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    private Tile tileData;
    private bool isClickable = true;
    private Action<Tile> OnClick;
    private float scale;
    private int sortingOrder;

    public void Setup(Tile tileData, float scale, Sprite sprite, int sortingOrder, float delay, Action<Tile> OnClick)
    {
        this.tileData = tileData;
        this.OnClick = OnClick;
        this.scale = scale;
        this.sortingOrder = sortingOrder;

        transform.localScale = new Vector2(scale, scale);
        transform.localPosition = tileData.position;
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

    public void SetSortingLayer(int sortingOrder)
    {
        spriteRenderer.sortingOrder = sortingOrder;
    }

    public Vector2 GetRealScale()
    {
        return spriteRenderer.size;
    }

    public TileView SetScale()
    {
        transform.localScale = new Vector2(scale, scale);
        return this;
    }

    public TileView SetPosition(Vector2 position)
    {
        transform.localPosition = position;
        return this;
    }

    public TileView SetSpriteRenderer(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
        return this;
    }

    public TileView TweenMove(float originPosY, float targetPosY)
    {
        spriteRenderer.sortingOrder = sortingOrder;
        SetActive(true);
        transform.localPosition = new Vector3(transform.localPosition.x, originPosY);
        transform.DOMoveY(targetPosY, 0.3f).SetEase(Ease.InOutQuad);

        return this;
    }
}
