using System.Collections;
using UnityEngine;

public class PicturesQuizView : AbstractQuizView
{
    [SerializeField] private AlphaTransition _questionSectionAlpha;

    [SerializeField] protected string _decorationsFolder = "WOWDesignImages";
    [SerializeField] protected string _questionsFolder = "WOWQuestionImages";

    #region public methods

    public override void SetQuestion(AbstractQuizQuestion question)
    {
        _isCurrentQuestionAnswered = false;
        var q = (PicturesQuizQuestion)question;
        _correctAnswer = q.AnswerIndex;
        SetQuestionText(q.QuestionImagePath, _questionsFolder);
        SetQuestionDecoration(q.DecorationImagePath, _decorationsFolder);
        UpdateAnswerButtons(q.AnswerIndex);
        _questionSectionAnimator.SetActiveState();
        
    }
    
    public override void SetNextSection()
    {
        if (!_isCurrentQuestionAnswered)
        {
            return;
        }
        StartCoroutine(SetNextQuestionCoroutine());
    }

    #endregion

    #region private methods

    private IEnumerator SetNextQuestionCoroutine()
    {
        _questionSectionAlpha.StartFadeOut(0.5f);
        yield return new WaitForSeconds(0.5f);
        _questionSectionAnimator.ToggleState();
        yield return new WaitForSeconds(0.4f);
        _questionSectionAlpha.SetOpaque();
        _quizController.SetNewQuestion();
    }

    #endregion
}

