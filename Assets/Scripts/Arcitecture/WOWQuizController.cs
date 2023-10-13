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
    public WOWQuizController(WOWQuizView quizView)
    {
        _quizView = quizView;
        _quizQuestions = LoadQuizQuestions();
    }

    private List<WOWQuizQuestion> LoadQuizQuestions()
    {
        string questionsJsonName = (DataLoader.BuildStreamingAssetPath("wowQuestions.json"));
        List<WOWQuizQuestion> quizQuestions = DataLoader.GetListFromJSON<WOWQuizQuestion>(questionsJsonName);
        
        Debug.Log($"Loaded {quizQuestions.Count} questions from file");
        
        List<WOWQuizQuestion> randomizedQuestions = (DataLoader.GetRandomElements(quizQuestions, 10));
        DataLoader.Shuffle(randomizedQuestions);
        return randomizedQuestions;
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
    }
    
    private void OnQuesionsEnds()
    {
        Debug.Log($"There is no more questions!!!");
        StartNewGame();
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
        }
    }

    public void IncreaseScore()
    {
        _currentScore++;
        Debug.Log($"Score increased current score {_currentScore}");
    }
}
