using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractQuizController<T> where T : AbstractQuizQuestion
{
    protected AbstractQuizView AbstractQuizView;
    protected List<T> _quizQuestions;
    protected int _currentQuestionIndex;
    protected int _currentScore = 0;
    protected ResultSavingManager _resultSavingManager;
    protected LeaderBoardController _leaderBoardController;

    #region public methods

    public AbstractQuizController(AbstractQuizView abstractQuizView)
    {
        AbstractQuizView = abstractQuizView;
        LoadQuizQuestions();
    }
    

    public void StartNewGame()
    {
        if (_quizQuestions.Count == 0)
        {
            throw new Exception("There is not quesions loaded!!!");
        }

        _currentScore = 0;
        _currentQuestionIndex = 0;
        AbstractQuizView.SetQuestion(_quizQuestions[_currentQuestionIndex]);
        AbstractQuizView.SetQuestionCounterText(_quizQuestions.Count, _currentQuestionIndex);
    }
    
    public void SetNewQuestion()
    {
        int lastQuestionIndex = _quizQuestions.Count - 1;
        
        if (_currentQuestionIndex + 1 > lastQuestionIndex)
        {
            OnQuesionsEnds();
            return;
        }
        
        if (_currentQuestionIndex + 1 <= lastQuestionIndex)
        {
            ++_currentQuestionIndex;
            AbstractQuizView.SetQuestion(_quizQuestions[_currentQuestionIndex]);
            AbstractQuizView.SetQuestionCounterText(_quizQuestions.Count, _currentQuestionIndex);
        }
    }
    
    public void IncreaseScore()
    {
        _currentScore++;
        Debug.Log($"Score increased current score {_currentScore}");
    }
    
    public void SetLeaderBoardController(LeaderBoardController lbController)
    {
        _leaderBoardController = lbController;
    }

    #endregion

    #region abstract methods

    protected abstract void LoadQuizQuestions();
    protected abstract void OnQuesionsEnds();

    #endregion
}
