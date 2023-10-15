using System;
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
    private LeaderBoardController _leaderBoardController;

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

    public void SetLeaderBoardController(LeaderBoardController lbController)
    {
        _leaderBoardController = lbController;
    }

    #endregion

    #region private methods
    private List<WOWQuizQuestion> LoadQuizQuestions()
    {
        string questionsJsonName = (DataLoader.BuildStreamingAssetPath("wowQuestions.json"));
        List<WOWQuizQuestion> quizQuestions = DataLoader.GetListFromJSON<WOWQuizQuestion>(questionsJsonName);
        
        Debug.Log($"Loaded {quizQuestions.Count} questions from file");
        
        List<WOWQuizQuestion> randomizedQuestions = (DataLoader.GetRandomElements(quizQuestions, 10));
        DataLoader.Shuffle(randomizedQuestions);
        return randomizedQuestions;
    }
    
    private void OnQuesionsEnds()
    {
        _resultSavingManager = new ResultSavingManager(_resultFilePath);
        
        _quizView.gameObject.SetActive(false);
        _leaderBoardController.SetTable((List<QuizResult>)_resultSavingManager.GetResults());
        _leaderBoardController.gameObject.SetActive(true);
        
        if (_resultSavingManager.IsScoreInTop(_currentScore)) // Если текущий набранный счет попадает в таблицу лидеров
        {
            Debug.Log("Saving data");
            _leaderBoardController.ShowInputField(_currentScore);
            _leaderBoardController.SetSavingManager(_resultSavingManager);
        }
        else
        {
            _leaderBoardController.HideInputField();
        }
    }
    
    #endregion
}


/*public class WOWQuizController : AbstractQuizController<WOWQuizQuestion>
{
    public WOWQuizController(PicturesQuizView quizView): base(quizView)
    {
        
    }

    public override List<WOWQuizQuestion> LoadQuizQuestions()
    {
        string questionsJsonName = (DataLoader.BuildStreamingAssetPath("wowQuestions.json"));
        List<WOWQuizQuestion> quizQuestions = DataLoader.GetListFromJSON<WOWQuizQuestion>(questionsJsonName);
        
        Debug.Log($"Loaded {quizQuestions.Count} questions from file");
        
        List<WOWQuizQuestion> randomizedQuestions = (DataLoader.GetRandomElements(quizQuestions, 10));
        DataLoader.Shuffle(randomizedQuestions);
        return randomizedQuestions;
    }

    public override void OnQuesionsEnds()
    {
        _resultSavingManager = new ResultSavingManager(_resultFilePath);
        
        _quizView.gameObject.SetActive(false);
        _leaderBoardController.SetTable((List<QuizResult>)_resultSavingManager.GetResults());
        _leaderBoardController.gameObject.SetActive(true);
        
        if (_resultSavingManager.IsScoreInTop(_currentScore)) // Если текущий набранный счет попадает в таблицу лидеров
        {
            Debug.Log("Saving data");
            _leaderBoardController.ShowInputField(_currentScore);
            _leaderBoardController.SetSavingManager(_resultSavingManager);
        }
        else
        {
            _leaderBoardController.HideInputField();
        }
    }

}*/