using System;
using System.Collections;
using AwakeSolutions;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public abstract class AbstractQuizView: MonoBehaviour
{
    [SerializeField] protected UIAnimationController _nextQuestionButton;
    /*[SerializeField] protected TMP_Text _questionCounter;*/
    [SerializeField] protected TMP_Text _questionNumber;
    [SerializeField] protected AwakeMediaPlayer _questionDecortaion;
    [SerializeField] protected AwakeMediaPlayer _questionTextImage;
    [SerializeField] protected UIAnimationController _questionSectionAnimator;
    [SerializeField] protected AlphaTransition _alphaTransition;
    
    protected IQuizController _quizController;
    protected int _correctAnswer;
    protected bool _isCurrentQuestionAnswered = false;

    [SerializeField] protected CustomButton[] _answerButtons;
    public UnityEvent OnTimerEnds;
    /*private float _timer;
    private bool _isExited = false;
    public float TotalTime;*/

    #region abstract methods

    public abstract void SetQuestion(AbstractQuizQuestion question);
    public abstract void SetNextSection();

    #endregion

    #region public methods
    

    public void SetQuestionCounterText(int questionAmount, int currentQuestionId)
    {
        _questionNumber.text = $"ВОПРОС № {currentQuestionId + 1}";
    }

    public void SetQuizController(IQuizController controller)
    {
        _quizController = controller;
        /*_isExited = false;*/
    }

    public void FadeOutDisabling()
    {
        StartCoroutine(FadeOutDisablingCoroutine());
    }
    
    

    #endregion

    #region protected methods

    protected IEnumerator FadeOutDisablingCoroutine()
    {
        /*_alphaTransition.StartFadeOut(2);*/
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
    }

    protected void SetQuestionText(string filename, string folderName)
    {
        _questionTextImage.Open(folderName, filename);
    }

    protected void SetQuestionDecoration(string filename, string folderName)
    {
        _questionDecortaion.Open(folderName, filename);
    }
    
    protected void UpdateAnswerButtons(int correctAnswerIndex)
    {
        for (int i = 0; i < _answerButtons.Length; i++)
        {
            var i1 = i;
            _answerButtons[i].SetOnClickFunction(() => OnAnswerButtonClick(i1));
        }
        
        foreach (CustomButton button in _answerButtons)
        {
            button.setDefaultSprite();
            button.SetTransparent();
        }
    }

    #endregion

    #region private methods

    private void Update()
    {
        _quizController.IncreaseTimer(Time.deltaTime);
    }


    private void OnAnswerButtonClick(int clickedIndex)
    {
        if (_isCurrentQuestionAnswered)
        {
            return;
        }
        
        if (clickedIndex == _correctAnswer)
        {
            _quizController.IncreaseScore();
        }
        
        _isCurrentQuestionAnswered = true;
        _nextQuestionButton.SetActiveState();
        
        if(clickedIndex == _correctAnswer){
            _answerButtons[clickedIndex].SetCorrectSprite();
            Debug.Log("Correct clicked");            
        }else{
            _answerButtons[clickedIndex].SetUncorrectSprite();
            _answerButtons[_correctAnswer].SetCorrectSprite();
            Debug.Log("uncorrect clicked");
        }
        
    }

    #endregion

}
