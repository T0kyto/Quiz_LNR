using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

public class ResultSavingManager
{
    private List<QuizResult> _quizResults = new List<QuizResult>();
    private string _dataPath;
    private const int MaxUniqueResultsToSave = 10;

    #region public methods

    public ResultSavingManager(string filename)
    {
        _dataPath = DataLoader.BuildStreamingAssetPath(filename);
        Debug.Log($"saving data in {_dataPath}");
        LoadResults();
    }
    
    public void AddResult(QuizResult result)
    {
        _quizResults.Add(result);
        SaveResults();
    }
    
    public bool IsScoreInTop(int correctAnswers)
    {
        if (correctAnswers == 0)
        {
            return false;
        }
        
        if (_quizResults.Count < 10)
        {
            return true;
        }
        // Сортируем результаты по убыванию количества правильных ответов
        _quizResults = _quizResults.OrderByDescending(result => result.correctAnswers).ToList();
        // Группируем результаты по количеству правильных ответов
        var groupedResults = _quizResults.GroupBy(result => result.correctAnswers);
        // Выбираем топ 10 уникальных групп
        var topUniqueGroups = groupedResults.Take(MaxUniqueResultsToSave);
        // Проверяем, входит ли заданное количество правильных ответов в топ
        foreach (var group in topUniqueGroups)
        {
            if (correctAnswers >= group.Key)
            {
                return true;
            }
        }
        
        return false;
    }

    public IReadOnlyList<QuizResult> GetResults()
    {
        return _quizResults;
    }

    #endregion

    #region private methods

    private void SaveResults()
    {
        _quizResults = _quizResults.OrderByDescending(result => result.correctAnswers).ToList();

        // Группируем результаты по количеству правильных ответов и выбираем топ 10 уникальных групп
        var topUniqueResults = _quizResults.GroupBy(result => result.correctAnswers)
            .Take(MaxUniqueResultsToSave)
            .SelectMany(group => group);

        _quizResults = topUniqueResults.ToList();

        string json = JsonConvert.SerializeObject(_quizResults.ToArray());
        File.WriteAllText(_dataPath, json);
        
        LoadResults();
    }
    
    private void LoadResults()
    {
        _quizResults = DataLoader.GetListFromJSON<QuizResult>(_dataPath);
    }

    #endregion
}
