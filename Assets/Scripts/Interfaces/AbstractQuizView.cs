using AwakeSolutions;
using TMPro;
using UnityEngine;

public abstract class AbstractQuizView: MonoBehaviour
{
    [SerializeField] protected UIAnimationController _nextQuestionButton;
    [SerializeField] protected TMP_Text _questionCounter;
    [SerializeField] protected TMP_Text _questionNumber;
    [SerializeField] protected AwakeMediaPlayer _questionDecortaion;
    [SerializeField] protected AwakeMediaPlayer _questionTextImage;
    [SerializeField] protected UIAnimationController _questionSectionAnimator;
    
    protected IQuizController _quizController;
    protected int _correctAnswer;
    protected bool _isCurrentQuestionAnswered = false;

    [SerializeField] protected CustomButton[] _answerButtons;

    #region abstract methods

    public abstract void SetQuestion(AbstractQuizQuestion question);
    public abstract void SetNextSection();

    #endregion

    #region public methods

    public void SetQuestionCounterText(int questionAmount, int currentQuestionId)
    {
        _questionCounter.text = $"{currentQuestionId + 1}/{questionAmount}";
        _questionNumber.text = $"ВОПРОС № {currentQuestionId + 1}";
    }

    public void SetQuizController(IQuizController controller)
    {
        _quizController = controller;
    }

    #endregion

    #region protected methods

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
            Debug.Log("set to default");
            button.setDefaultSprite();
        }
    }

    #endregion

    #region private methods

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
        _nextQuestionButton.ToggleState();
        
        if(clickedIndex == _correctAnswer){
            _answerButtons[clickedIndex].setCorrectSprite();
            Debug.Log("Correct clicked");            
        }else{
            _answerButtons[clickedIndex].setUncorrectSprite();
            _answerButtons[_correctAnswer].setCorrectSprite();
            Debug.Log("uncorrect clicked");
        }
        
    }

    #endregion

}
