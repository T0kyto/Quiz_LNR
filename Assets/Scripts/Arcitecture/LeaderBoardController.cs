using System.Collections.Generic;
using System.Linq;
using AwakeSolutions;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LeaderBoardController : MonoBehaviour
{
    /*/*[SerializeField] private GridLayoutGroup _tableGridGroup;
    [SerializeField] private GameObject _tableRowPrefab;
    [SerializeField] private GameObject _DataInputField;
    [SerializeField] private TMP_Text _currentScoreText;*/
    [SerializeField] private TMP_InputField _nameInputField;
    [SerializeField] private UnityEvent _onContinue;
    [SerializeField] private AwakeMediaPlayer _background;
    private ResultSavingManager _savingManager;
    private List<GameObject> _currentRows = new List<GameObject>();

    [SerializeField] private UIAnimationController _nameInputLayout;
    [SerializeField] private UIAnimationController _leaderboardLayout;
    [SerializeField] private GameObject _tableRowPrefab;
    [SerializeField] private GameObject _tableGrid;
    [SerializeField] private TMP_Text _scoreText;

    private int _currentScore;

    #region public methods
    public void SetLeaderBoard(List<QuizResult> results, int currentScore)
    {
        ClearCurrentLeaderboard();
        _scoreText.text = currentScore.ToString();
        List<QuizResult> cuttedResults = results.Take(10).ToList();
        _nameInputField.text = "";
        
        foreach (QuizResult result in cuttedResults)
        {
            PushRowToLeaderboard(result);
        }
    }


    public void SetSavingManager(ResultSavingManager manager)
    {
        _savingManager = manager;
    }
    

    public void ShowNameInput(int score)
    {
        _nameInputLayout.SetActiveState();
        _currentScore = score;
    }

    public void ShowLeaderBoard(int score)
    {
        _currentScore = score;
        _leaderboardLayout.SetActiveState();
    }

    public void SetLightBackground()
    {
        _background.Open("Design", "background_light", true, true);
    }
    
    public void SetDarkBackground()
    {
        _background.Open("Design", "background_wow", true, true);
    }

    public void OnNameInputContinue()
    {
        int playerScore = _currentScore;
        string playerName = _nameInputField.text == "" ? "Безымянный игрок" : _nameInputField.text;
        long timestamp = DataLoader.GetTimeStamp();
            
        _savingManager.AddResult(
            new QuizResult(
                playerScore, 
                playerName, 
                timestamp
            ));
            
        SetLeaderBoard((List<QuizResult>)_savingManager.GetResults(), _currentScore);
        
        _nameInputLayout.SetUnactiveState();
        _leaderboardLayout.SetActiveState();
    }
    #endregion

    #region private methods
    private void PushRowToLeaderboard(QuizResult result)
    {
        GameObject row = Instantiate(_tableRowPrefab, _tableGrid.transform);
        TMP_Text[] textFields = row.GetComponentsInChildren<TMP_Text>();
        textFields[0].text = result.playerName;
        textFields[1].text = result.correctAnswers.ToString();
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
