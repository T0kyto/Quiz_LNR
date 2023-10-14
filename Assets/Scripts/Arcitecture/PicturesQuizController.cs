using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class PicturesQuizController: IQuizController
{
    private PicturesQuizView _quizView;
    private List<PicturesQuizQuestion> _quizQuestions = new List<PicturesQuizQuestion>();
    private int _currentQuestionIndex;
    private int _currentScore = 0;
    private string _resultFilePath = "picturesResult.json";
    private ResultSavingManager _resultSavingManager;

    #region public methods

    public PicturesQuizController(PicturesQuizView quizView)
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

    public int GetScore()
    {
        return _currentScore;
    }

    #endregion

    #region private methods

    private List<PicturesQuizQuestion> LoadQuizQuestions()
    {
        string questionsJsonName = (DataLoader.BuildStreamingAssetPath("picturesQuestions.json"));
        List<PicturesQuizQuestion> quizQuestions = DataLoader.GetListFromJSON<PicturesQuizQuestion>(questionsJsonName);
        
        Debug.Log($"Loaded {quizQuestions.Count} questions from file");

        List<PicturesQuizQuestion> randomizedQuestions = (DataLoader.GetRandomElements(quizQuestions, 7));
        DataLoader.Shuffle(randomizedQuestions);
        return randomizedQuestions;
    }
    
    private void OnQuesionsEnds()
    {
        Debug.Log($"There is no more questions!!!");
        
        _resultSavingManager = new ResultSavingManager(_resultFilePath);
        if(_resultSavingManager.IsScoreInTop(_currentScore))
        {
            Debug.Log("Saving data");
            _resultSavingManager.AddResult(new QuizResult(_currentScore, "Unnamed player", DataLoader.GetTimeStamp()));
        }
        
    }

    #endregion

}
