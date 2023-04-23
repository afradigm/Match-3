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
        spriteRenderer.sortingOrder = sortingOrder;
        transform.name = $"{tileData.row} - {tileData.column}";
        transform.DOPunchScale(new Vector3(0.1f, 0.1f, 0f), 0.5f, 10, 0.5f).SetDelay(delay).OnStart(() =>
        {
            gameObject.SetActive(true);
        });
    }

    public void OnMouseUp()
    {
        if (isClickable)
        {
            isClickable = false;
            OnClick?.Invoke(tileData);

            transform.DOPunchScale(new Vector3(0.1f, 0.1f, 0f), 0.5f, 10, 0.5f).OnComplete(() =>
            {
                isClickable = true;
            });
        }
    }

    public Vector2 GetRealScale()
    {
        return spriteRenderer.size;
    }
}
