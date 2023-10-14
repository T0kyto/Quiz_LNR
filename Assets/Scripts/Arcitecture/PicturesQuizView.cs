using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PicturesQuizView : MonoBehaviour, IQuizView
{
    [SerializeField] private Image _questionImage;
    [SerializeField] private GridLayoutGroup _questionButtonsGroup;
    [SerializeField] private TMP_Text _questionText;
    [SerializeField] private string _questionImagesFolder;
    [SerializeField] private CustomButton _questionButtonPrefab;
    [SerializeField] private Button _nextQuestionButton;
    [SerializeField] private TMP_Text _questionCounter;
    private PicturesQuizController _quizController;
    private int _correctAnswer;
    private List<CustomButton> _currentButtons = new List<CustomButton>();
    private bool _isCurrentQuestionAnswered = false;


    #region public methods

    public void OnAnswerButtonClick(int clickedButtonIndex)
    {
        if (_isCurrentQuestionAnswered)
        {
            return;
        }
        
        _currentButtons[clickedButtonIndex].SetAnswerColor(clickedButtonIndex == _correctAnswer);
        _isCurrentQuestionAnswered = true;

        if (clickedButtonIndex == _correctAnswer)
        {
            _quizController.IncreaseScore();
        }
        
        _nextQuestionButton.gameObject.SetActive(true);
        _nextQuestionButton.GetComponent<AlphaTransition>().StartFadeIn(1);
    }
    public void SetQuestion(AbstractQuizQuestion question)
    {
        _isCurrentQuestionAnswered = false;
        ClearAnswerButtons();
        PicturesQuizQuestion q = (PicturesQuizQuestion)question;
        Sprite questionSprite = DataLoader.LoadImage(Path.Combine(_questionImagesFolder, q.ImagePath + ".jpeg"));
        SetQuestionText(question.Question);
        SetQuestionImage(questionSprite);
        _correctAnswer = q.AnswerIndex;
        
        for (int i = 0; i < question.Answers.Length; i++)
        {
            var i1 = i;
            CustomButton button = InstantiateAnswerButton(question.Answers[i], () =>
            {
                OnAnswerButtonClick(i1);
            });
            PushAnswerButtonToView(button);
        };
            
    }

    public void SetQuizController(PicturesQuizController controller)
    {
        _quizController = controller;
    }

    public void SetNextQuestion()
    {
        _quizController.SetNewQuestion();
    }

    public void SetQuestionCounterText(int questionAmount, int currentQuestionId)
    {
        _questionCounter.text = $"{currentQuestionId + 1}/{questionAmount}";
    }

    #endregion

    #region private methods

    private void SetQuestionImage(Sprite questionImage)
    {
        _questionImage.sprite = questionImage;
    }

    private void PushAnswerButtonToView(CustomButton newButton)
    {
        newButton.transform.parent = _questionButtonsGroup.transform;
        newButton.transform.localScale = Vector3.one;
        
        _currentButtons.Add(newButton);
    }

    private void SetQuestionText(string questionText)
    {
        _questionText.text = questionText;
    }

    private void ClearAnswerButtons()
    {
        foreach(CustomButton button in _currentButtons)
        {
            Destroy(button.gameObject);
        }

        _currentButtons.Clear();
    }
    
    private CustomButton InstantiateAnswerButton(string answerText, Action buttonAction)
    {
        CustomButton button = Instantiate(_questionButtonPrefab);
        button.InitButton(answerText, buttonAction);
        
        return button;
    }

    #endregion
}

