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

    [SerializeField] private Image _checkBoxImage;

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

    public void SetUncorrectSprite()
    {
        _checkBoxImage.sprite = _uncorrectSprite;
        _checkBoxImage.GetComponent<CanvasGroup>().alpha = 1;
    }

    public void SetCorrectSprite()
    {
        _checkBoxImage.sprite = _correctImage;
        _checkBoxImage.GetComponent<CanvasGroup>().alpha = 1;
    }

    public void SetTransparent()
    {
        _checkBoxImage.GetComponent<CanvasGroup>().alpha = 0;
    }

    public void setDefaultSprite()
    {
        _checkBoxImage.sprite = _defaultImage;
    }
}
