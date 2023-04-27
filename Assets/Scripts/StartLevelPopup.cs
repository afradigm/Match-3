using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartLevelPopup : MonoBehaviour
{
    [SerializeField] private Text levelText;
    [SerializeField] private GameObject panel;
    [SerializeField] private Transform popup;
    private Action OnStartLevel;


    public void Setup(int nextLevel, Action OnStartLevel)
    {
        this.OnStartLevel = OnStartLevel;
        OpenNextLevel(nextLevel);
    }

    public void Start_OnClick()
    {
        CloseLevelPopup();
        OnStartLevel?.Invoke();
    }

    public void OpenLevelPopup()
    {
        popup.localScale = Vector3.zero;
        popup.gameObject.SetActive(true);
        panel.SetActive(true);
        popup.DOScale(Vector3.one, 0.5f);
    }

    public void CloseLevelPopup()
    {
        popup.gameObject.transform.DOScale(Vector3.zero, 0.5f).OnComplete(() =>
        {
            panel.SetActive(false);
            popup.gameObject.SetActive(false);
        });
    }

    public void OpenNextLevel(int nextLevel)
    {
        levelText.text = $"Level: {nextLevel + 1}";
        OpenLevelPopup();
    }
}
