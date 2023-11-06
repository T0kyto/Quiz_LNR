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
        /*_currentQuizController.StartNewGame();*/
        StartCoroutine(FadeOutCoroutine());
    }

    public void OnStartPicturesQuizClick()
    {
        _currentQuizController = new PicturesQuizController(_picturesQuizView);
        _picturesQuizView.gameObject.SetActive(true);
        _picturesQuizView.GetComponent<AlphaTransition>().StartFadeIn();
        _picturesQuizView.SetQuizController((PicturesQuizController)_currentQuizController);
        _currentQuizController.SetLeaderBoardController(_leaderBoardController);
        /*_currentQuizController.StartNewGame();*/
        StartCoroutine(FadeOutCoroutine());
    }

    public void ExitWOWQuiz()
    {
        _wowQuizView.GetComponent<AlphaTransition>().StartFadeOut();
        StartCoroutine(FadeInCoroutine(_wowQuizView));
    }

    public void ExitPicturesQuiz()
    {
        _picturesQuizView.GetComponent<AlphaTransition>().StartFadeOut();
        StartCoroutine(FadeInCoroutine(_picturesQuizView));
    }

    public void ExitLeaderboard()
    {
        GetComponent<AlphaTransition>().StartFadeIn();
        StartCoroutine(DisableLeaderboard());
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

    private IEnumerator FadeInCoroutine(AbstractQuizView view) 
    {
        gameObject.SetActive(true);
        GetComponent<AlphaTransition>().StartFadeIn();
        yield return new WaitForSeconds(1);
        view.gameObject.SetActive(false);
    }

    private IEnumerator DisableLeaderboard()
    {
        yield return new WaitForSeconds(1);
        _leaderBoardController.GetComponent<AlphaTransition>().SetTransparent();
        _leaderBoardController.gameObject.SetActive(false);
    }

    #endregion
}
