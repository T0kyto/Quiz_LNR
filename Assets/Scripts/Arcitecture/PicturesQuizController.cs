using System;
using System.Collections.Generic;
using UnityEngine;
public class PicturesQuizController : AbstractQuizController<PicturesQuizQuestion>, IQuizController
{
    private string _resultFilePath = "picturesResult.json";
    private int _totalQuestionsCount = 7;
    
    public PicturesQuizController(AbstractQuizView abstractQuizView) : base(abstractQuizView) {}

    #region protected methods

    protected override void LoadQuizQuestions()
    {
        string questionsJsonName = (DataLoader.BuildStreamingAssetPath("picturesQuestions.json"));
        List<PicturesQuizQuestion> quizQuestions = DataLoader.GetListFromJSON<PicturesQuizQuestion>(questionsJsonName);
        
        Debug.Log($"Loaded {quizQuestions.Count} questions from file");

        List<PicturesQuizQuestion> randomizedQuestions = (DataLoader.GetRandomElements(quizQuestions, _totalQuestionsCount));
        DataLoader.Shuffle(randomizedQuestions);
        quizQuestions = randomizedQuestions;

        _quizQuestions = quizQuestions;
    }

    protected override void OnQuesionsEnds()
    {
        _resultSavingManager = new ResultSavingManager(_resultFilePath);
        _leaderBoardController.SetLeaderBoard((List<QuizResult>)_resultSavingManager.GetResults(), _currentScore);
        _leaderBoardController.gameObject.SetActive(true);
        _leaderBoardController.SetLightBackground();
        
        _leaderBoardController.GetComponent<AlphaTransition>().StartFadeIn();
        AbstractQuizView.FadeOutDisabling();
        
        if (_resultSavingManager.IsScoreInTop(_currentScore)) // Если текущий набранный счет попадает в таблицу лидеров
        {
            Debug.Log("Saving data");
            _leaderBoardController.SetSavingManager(_resultSavingManager);
            _leaderBoardController.ShowNameInput(_currentScore);
        }
        else
        {
            _leaderBoardController.ShowLeaderBoard(_currentScore);
        }
    }

    #endregion
}

