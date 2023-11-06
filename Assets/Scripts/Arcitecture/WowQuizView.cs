using UnityEngine;

public class WowQuizView : AbstractQuizView
{
    public string infoFolder = "WOWInfoImages";
    
    [SerializeField] protected InfoLayout _infoLayoutController;
    [SerializeField] protected UIAnimationController _questionInfoAnimator;
    
    [SerializeField] private string _decorationsFolder = "WOWDesignImages";
    [SerializeField] private string _questionsFolder = "WOWQuestionImages";
    private bool _isQuestionInfoShown = false;

    #region public methods

    public override void SetQuestion(AbstractQuizQuestion question)
    {
        _isCurrentQuestionAnswered = false;
        _isQuestionInfoShown = false;
        WOWQuizQuestion q = (WOWQuizQuestion)question;
        _correctAnswer = q.AnswerIndex;
        
        SetQuestionText(q.QuestionImagePath, _questionsFolder);
        SetQuestionDecoration(q.DecorationImagePath, _decorationsFolder);
        _infoLayoutController.SetInfoLayout(infoFolder, q.InfoImagePath);
        UpdateAnswerButtons(q.AnswerIndex);
        _questionSectionAnimator.SetActiveState();
        
    }
    
    public override void SetNextSection()
    {
        if (!_isCurrentQuestionAnswered && !_isQuestionInfoShown)
        {
            return;
        }

        if (!_quizController.IsQuestionsRemains() && _isQuestionInfoShown)
        {
            _quizController.SetNewQuestion();
            Debug.Log("Last question");
            return;
        }
        
        if(_isQuestionInfoShown)
        {
            _questionInfoAnimator.SetUnactiveState();
            _quizController.SetNewQuestion();
            _nextQuestionButton.SetUnactiveState();
        }
        else
        {
            _isQuestionInfoShown = true;
            _questionSectionAnimator.SetUnactiveState();
            _questionInfoAnimator.SetActiveState();
        }
    }

    #endregion
}
