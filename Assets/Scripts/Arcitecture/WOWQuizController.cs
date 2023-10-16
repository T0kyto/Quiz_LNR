using System;
using System.Collections.Generic;
using UnityEngine;

public class WOWQuizController : AbstractQuizController<WOWQuizQuestion>, IQuizController
{
    private string _resultFilePath = "WOWResults.json";
    private int _totalQuestionsCount = 10;
    public WOWQuizController(AbstractQuizView abstractQuizView): base(abstractQuizView)
    {
        
    }

    protected override void LoadQuizQuestions()
    {
        string questionsJsonName = (DataLoader.BuildStreamingAssetPath("wowQuestions.json"));
        List<WOWQuizQuestion> quizQuestions = DataLoader.GetListFromJSON<WOWQuizQuestion>(questionsJsonName);
        
        Debug.Log($"Loaded {quizQuestions.Count} questions from file");
        
        List<WOWQuizQuestion> randomizedQuestions = (DataLoader.GetRandomElements(quizQuestions, _totalQuestionsCount));
        DataLoader.Shuffle(randomizedQuestions);
        quizQuestions = randomizedQuestions;

        _quizQuestions = quizQuestions;
    }

    protected override void OnQuesionsEnds()
    {
        _resultSavingManager = new ResultSavingManager(_resultFilePath);
        
        AbstractQuizView.gameObject.SetActive(false);
        _leaderBoardController.SetTable((List<QuizResult>)_resultSavingManager.GetResults());
        _leaderBoardController.gameObject.SetActive(true);
        
        if (_resultSavingManager.IsScoreInTop(_currentScore)) // Если текущий набранный счет попадает в таблицу лидеров
        {
            Debug.Log("Saving data");
            _leaderBoardController.ShowNameInputField(_currentScore);
            _leaderBoardController.SetSavingManager(_resultSavingManager);
        }
        else
        {
            _leaderBoardController.HideNameInputField();
        }
    }

}