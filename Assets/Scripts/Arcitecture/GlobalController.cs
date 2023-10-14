using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalController : MonoBehaviour
{
    private IQuizController _currentQuizController;
    [SerializeField] private PicturesQuizView _picturesQuizView;
    [SerializeField] private WOWQuizView _wowQuizView;

    public void OnStartWOWQuizClick()
    {
        _currentQuizController = new WOWQuizController(_wowQuizView);
        _wowQuizView.GetComponent<AlphaTransition>().StartFadeIn();
        _currentQuizController.StartNewGame();
        _wowQuizView.SetQuizController((WOWQuizController)_currentQuizController);
        GetComponent<AlphaTransition>().StartFadeOut();
        
        Debug.Log(_currentQuizController);
    }

    public void OnStartPicturesQuizClick()
    {
        _currentQuizController = new PicturesQuizController(_picturesQuizView);
        _picturesQuizView.GetComponent<AlphaTransition>().StartFadeIn();
        _currentQuizController.StartNewGame();
        _picturesQuizView.SetQuizController((PicturesQuizController)_currentQuizController);
        GetComponent<AlphaTransition>().StartFadeOut();
        
        Debug.Log(_currentQuizController);
    }
}
