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

    [SerializeField] private Color _rightAnswerColor;
    [SerializeField] private Color _wrongAnswerColor;

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

    private void SetOnClickFunction(Action action)
    {
        _button.onClick.AddListener(() => action());
    }

    public void InitButton(string text, Action action)
    {
        SetButtonText(text);
        SetOnClickFunction(action);
    }

    public void SetAnswerColor(bool isRightAnswer)
    {
        _button.GetComponent<Image>().color =  !isRightAnswer ? _rightAnswerColor : _wrongAnswerColor;
    }
}
