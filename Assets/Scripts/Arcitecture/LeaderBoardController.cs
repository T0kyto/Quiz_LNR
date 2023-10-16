using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LeaderBoardController : MonoBehaviour
{
    [SerializeField] private GridLayoutGroup _tableGridGroup;
    [SerializeField] private GameObject _tableRowPrefab;
    [SerializeField] private GameObject _DataInputField;
    [SerializeField] private TMP_Text _currentScoreText;
    [SerializeField] private TMP_InputField _nameInputField;
    [SerializeField] private UnityEvent _onContinue;
    private ResultSavingManager _savingManager;
    private List<GameObject> _currentRows = new List<GameObject>();

    #region public methods
    public void SetLeaderBoard(List<QuizResult> results)
    {
        ClearCurrentLeaderboard();
        
        List<QuizResult> cuttedResults = results.Take(10).ToList();
        
        foreach (QuizResult result in cuttedResults)
        {
            PushRowToLeaderboard(result);
        }
    }

    public void ShowNameInputField(int score) // Показывает поле ввода имени
    {
        _DataInputField.SetActive(true);
        _nameInputField.text = "";
        _currentScoreText.text = score.ToString();
    }

    public void HideNameInputField()
    {
        _DataInputField.SetActive(false);
    }

    public void SetSavingManager(ResultSavingManager manager)
    {
        _savingManager = manager;
    }

    public void OnContinueButtonClick()
    {
        if (_DataInputField.activeSelf) // Если активно поле ввода имени, то мы сохраняем новый результат и обновляем таблицу
        {
            int playerScore = int.Parse(_currentScoreText.text);
            string playerName = _nameInputField.text == " " ? "Безымянный игрок" : _nameInputField.text;
            long timestamp = DataLoader.GetTimeStamp();
            
            _savingManager.AddResult(
                new QuizResult(
                    playerScore, 
                    playerName, 
                    timestamp
                ));
            
            SetLeaderBoard((List<QuizResult>)_savingManager.GetResults());
            _DataInputField.SetActive(false);
        }
        else
        {
            _onContinue.Invoke();
        }

    }
    #endregion

    #region private methods
    private void PushRowToLeaderboard(QuizResult result)
    {
        GameObject row = Instantiate(_tableRowPrefab, _tableGridGroup.transform);
        TMP_Text[] textFields = row.GetComponentsInChildren<TMP_Text>();

        textFields[0].text = (_currentRows.Count + 1).ToString();
        textFields[1].text = result.playerName;
        textFields[2].text = result.correctAnswers.ToString();
        
        _currentRows.Add(row);
    }

    private void ClearCurrentLeaderboard()
    {
        foreach (GameObject row in _currentRows)
        {
            Destroy(row);
        }
        _currentRows.Clear();
    }
    #endregion
}
