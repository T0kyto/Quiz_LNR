/*
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractQuizController<T>
{
    protected AbstractQuizView _quizView;
    private List<T> _quizQuestions;
    private int _currentQuestionIndex;
    protected int _currentScore = 0;
    protected string _resultFilePath;
    protected ResultSavingManager _resultSavingManager;
    protected LeaderBoardController _leaderBoardController;

    #region public methods

    public AbstractQuizController(PicturesQuizView quizView)
    {
        _quizView = quizView;
        _quizQuestions = LoadQuizQuestions();
    }
    

    public void StartNewGame()
    {
        if (_quizQuestions.Count == 0)
        {
            throw new Exception("There is not quesions loaded!!!");
        }

        _currentScore = 0;
        _currentQuestionIndex = 0;
        _quizView.SetQuestion(_quizQuestions[_currentQuestionIndex]);
        _quizView.SetQuestionCounterText(_quizQuestions.Count, _currentQuestionIndex);
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
            _quizView.SetQuestion(_quizQuestions[_currentQuestionIndex]);
            _quizView.SetQuestionCounterText(_quizQuestions.Count, _currentQuestionIndex);
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

    public abstract List<T> LoadQuizQuestions();
    public abstract void OnQuesionsEnds();

    #endregion
}
*/
