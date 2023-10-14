using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WOWQuizController : IQuizController
{
    private WOWQuizView _quizView;
    private List<WOWQuizQuestion> _quizQuestions = new List<WOWQuizQuestion>();
    private int _currentQuestionIndex;
    private int _currentScore = 0;
    private string _resultFilePath = "WOWResults.json";
    private ResultSavingManager _resultSavingManager;

    #region public methods

    public WOWQuizController(WOWQuizView quizView)
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

        _currentQuestionIndex = 0;
        _currentScore = 0;
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

    #endregion

    #region private methods
    private List<WOWQuizQuestion> LoadQuizQuestions()
    {
        var questionsJsonName = (DataLoader.BuildStreamingAssetPath("wowQuestions.json"));
        var quizQuestions = DataLoader.GetListFromJSON<WOWQuizQuestion>(questionsJsonName);
        
        Debug.Log($"Loaded {quizQuestions.Count} questions from file");
        
        var randomizedQuestions = (DataLoader.GetRandomElements(quizQuestions, 10));
        DataLoader.Shuffle(randomizedQuestions);
        return randomizedQuestions;
    }
    
    private void OnQuesionsEnds()
    {
        _resultSavingManager = new ResultSavingManager(_resultFilePath);
        if (!_resultSavingManager.IsScoreInTop(_currentScore)) return;
        
        Debug.Log("Saving data");
        _resultSavingManager.AddResult(new QuizResult(_currentScore, "Unnamed player", DataLoader.GetTimeStamp()));
    }
    
    #endregion
}
