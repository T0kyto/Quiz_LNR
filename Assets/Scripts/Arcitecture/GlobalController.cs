using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GlobalController : MonoBehaviour
{
    private IQuizController _currentQuizController;
    [SerializeField] private PicturesQuizView _picturesQuizView;
    [SerializeField] private WowQuizView _wowQuizView;
    [SerializeField] private LeaderBoardController _leaderBoardController;

    public void OnStartWOWQuizClick()
    {
        _currentQuizController = new WOWQuizController(_wowQuizView);
        _wowQuizView.gameObject.SetActive(true);
        _wowQuizView.GetComponent<AlphaTransition>().StartFadeIn();
        _currentQuizController.StartNewGame();
        _wowQuizView.SetQuizController((WOWQuizController)_currentQuizController);
        _currentQuizController.SetLeaderBoardController(_leaderBoardController);
        GetComponent<AlphaTransition>().StartFadeOut();
        gameObject.SetActive(false);
    }

    public void OnStartPicturesQuizClick()
    {
        _currentQuizController = new PicturesQuizController(_picturesQuizView);
        _picturesQuizView.gameObject.SetActive(true);
        _picturesQuizView.GetComponent<AlphaTransition>().StartFadeIn();
        _currentQuizController.StartNewGame();
        _picturesQuizView.SetQuizController((PicturesQuizController)_currentQuizController);
        _currentQuizController.SetLeaderBoardController(_leaderBoardController);
        GetComponent<AlphaTransition>().StartFadeOut();
        gameObject.SetActive(false);
    }
}
