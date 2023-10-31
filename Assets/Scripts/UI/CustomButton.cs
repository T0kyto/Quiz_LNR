using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CustomButton : MonoBehaviour
{
    private Button _button;
    private TMP_Text _buttonText;

    [SerializeField] private Sprite _defaultImage;
    [SerializeField] private Sprite _correctImage;
    [SerializeField] private Sprite _uncorrectSprite;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _buttonText = _button?.GetComponentInChildren<TMP_Text>();
    }

    private void SetButtonText(string text)
    {
        _buttonText.text = text;
    }

    private void SetButtonImage(Image image)
    {
        _button.image = image;
    }

    public void SetButtonSize(Vector2 size)
    {
        RectTransform rectTransform = _button.GetComponent<RectTransform>();
        rectTransform.sizeDelta = size;
    }

    public void SetOnClickFunction(Action action)
    {
        _button.onClick.AddListener(() => action());
    }

    public void setUncorrectSprite()
    {
        _button.image.sprite = _uncorrectSprite;
    }

    public void setCorrectSprite()
    {
        _button.image.sprite = _correctImage;
    }

    public void setDefaultSprite()
    {
        _button.image.sprite = _defaultImage;
    }
}
