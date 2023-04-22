using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileView : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    private Tile tileData;
    private bool isClickable = true;

    public void Setup(Tile tileData, float scale, Vector2 position, Sprite sprite, int sortingOrder, float delay)
    {
        this.tileData = tileData;
        transform.localScale = new Vector2(scale, scale);
        transform.localPosition = position;
        spriteRenderer.sprite = sprite;
        spriteRenderer.sortingOrder = sortingOrder;
        SetActive(delay);
    }

    private void SetActive(float delay)
    {
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
            transform.DOPunchScale(new Vector3(0.1f, 0.1f, 0f), 0.5f, 10, 0.5f).OnComplete(() =>
            {
                // Set the tile back to clickable after the animation is complete
                isClickable = true;
            });
        }
    }

    public Vector2 GetRealScale()
    {
        return spriteRenderer.size;
    }
}
