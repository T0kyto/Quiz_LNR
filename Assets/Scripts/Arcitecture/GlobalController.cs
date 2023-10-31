using System.Collections;
using UnityEngine;


public class GlobalController : MonoBehaviour
{
    private IQuizController _currentQuizController;
    [SerializeField] private PicturesQuizView _picturesQuizView;
    [SerializeField] private WowQuizView _wowQuizView;
    [SerializeField] private LeaderBoardController _leaderBoardController;

    #region public methods

    public void OnStartWOWQuizClick()
    {
        _currentQuizController = new WOWQuizController(_wowQuizView);
        _wowQuizView.gameObject.SetActive(true);
        _wowQuizView.GetComponent<AlphaTransition>().StartFadeIn();
        _wowQuizView.SetQuizController((WOWQuizController)_currentQuizController);
        _currentQuizController.SetLeaderBoardController(_leaderBoardController);
        StartCoroutine(FadeOutCoroutine());
    }

    public void OnStartPicturesQuizClick()
    {
        _currentQuizController = new PicturesQuizController(_picturesQuizView);
        _picturesQuizView.gameObject.SetActive(true);
        _picturesQuizView.GetComponent<AlphaTransition>().StartFadeIn();
        _picturesQuizView.SetQuizController((PicturesQuizController)_currentQuizController);
        _currentQuizController.SetLeaderBoardController(_leaderBoardController);
        StartCoroutine(FadeOutCoroutine());
    }

    #endregion

    #region private methods

    private IEnumerator FadeOutCoroutine()
    {
        GetComponent<AlphaTransition>().StartFadeOut();
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
        _currentQuizController.StartNewGame();
    }

    #endregion
}
