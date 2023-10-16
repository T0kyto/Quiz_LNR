using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WowQuizView : AbstractQuizView
{
    [SerializeField] private GridLayoutGroup _questionButtonsGroup;
    [SerializeField] private TMP_Text _questionText;
    [SerializeField] private CustomButton _questionButtonPrefab;
    [SerializeField] private Button _nextQuestionButton;
    [SerializeField] private TMP_Text _questionCounter;
    [SerializeField] private AlphaTransition _questionLayoutAlpha;
    [SerializeField] private AlphaTransition _questionInfoAlpha;
    [SerializeField] private InfoLayout _infoLayoutController;
     
    private IQuizController _quizController;
    private int _correctAnswer;
    private bool _isCurrentQuestionAnswered = false;
    private List<CustomButton> _currentButtons = new List<CustomButton>();
    private bool _isQuestionInfoShown = false;

    #region public methods

    public override void SetQuestion(AbstractQuizQuestion question)
    {
        _isCurrentQuestionAnswered = false;
        _isQuestionInfoShown = false;
        ClearAnswerButtons();
        WOWQuizQuestion q = (WOWQuizQuestion)question;
        SetQuestionText(question.Question);
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
        
        _infoLayoutController.SetInfoLayout(q.Info, q.InfoPicturePath);
    }
    
    public override void SetQuizController(IQuizController controller)
    {
        _quizController = controller;
    }
    
    public override void SetQuestionCounterText(int questionAmount, int currentQuestionId)
    {
        _questionCounter.text = $"{currentQuestionId + 1}/{questionAmount}";
    }
    
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
        _nextQuestionButton.GetComponent<AlphaTransition>().StartFadeIn(0.5f);
    }

    public void SetNextSection()
    {
        if (!_isCurrentQuestionAnswered && !_isQuestionInfoShown)
        {
            return;
        }
        
        if(_isQuestionInfoShown)
        {
            _questionInfoAlpha.SetTransparent();
            _questionLayoutAlpha.SetOpaque();
            _quizController.SetNewQuestion();
        }
        else
        {
            _questionInfoAlpha.SetOpaque();
            _questionLayoutAlpha.SetTransparent();
            _isQuestionInfoShown = true;
        }
        
    }
    #endregion

    #region private methods

    private CustomButton InstantiateAnswerButton(string answerText, Action buttonAction)
    {
        CustomButton button = Instantiate(_questionButtonPrefab);
        button.InitButton(answerText, buttonAction);
        
        return button;
    }
    
    private void PushAnswerButtonToView(CustomButton newButton)
    {
        var buttonTransform = newButton.transform;
        buttonTransform.SetParent(_questionButtonsGroup.transform);
        buttonTransform.localScale = Vector3.one;
        
        _currentButtons.Add(newButton);
    }
    
    private void ClearAnswerButtons()
    {
        foreach(CustomButton button in _currentButtons)
        {
            Destroy(button.gameObject);
        }
        _currentButtons.Clear();
    }
    
    private void SetQuestionText(string questionText)
    {
        _questionText.text = questionText;
    }

    #endregion
}