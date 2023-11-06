using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class AbstractQuizController<T> where T : AbstractQuizQuestion
{
    private int _currentQuestionIndex;
    protected AbstractQuizView AbstractQuizView;
    protected List<T> _quizQuestions;
    protected int _currentScore = 0;
    protected ResultSavingManager _resultSavingManager;
    protected LeaderBoardController _leaderBoardController;
    
    protected float _timer = 0;
    public float TotalTime = 180;
    private bool _isExited = false;
    

    #region public methods

    public void IncreaseTimer(float value)
    {
        _timer += value;
        /*Debug.Log($"Current Timer value {_timer}");*/
        if (_timer >= TotalTime && !_isExited)
        {
            _isExited = true;
            AbstractQuizView.OnTimerEnds.Invoke();
        }
    }

    public void ResetTimer()
    {
        _timer = 0;
    }

    public AbstractQuizController(AbstractQuizView abstractQuizView)
    {
        AbstractQuizView = abstractQuizView;
        LoadQuizQuestions();
    }
    
    public void StartNewGame()
    {
        ResetTimer();
        
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
        ResetTimer();
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

    public bool IsQuestionsRemains()
    {
        return (_quizQuestions.Count - _currentQuestionIndex) > 1;
    }

    #endregion

    #region abstract methods

    protected abstract void LoadQuizQuestions();
    protected abstract void OnQuesionsEnds();

    #endregion
}
