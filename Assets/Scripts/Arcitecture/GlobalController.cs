using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalController : MonoBehaviour
{
    private IQuizController currentQuizController;
    [SerializeField] private PicturesQuizView _picturesQuizView;
    [SerializeField] private WOWQuizView _wowQuizView;

    public void OnStartWOWQuizClick()
    {
        currentQuizController = new WOWQuizController(_wowQuizView);
        _wowQuizView.GetComponent<AlphaTransition>().StartFadeIn(1);
        currentQuizController.StartNewGame();
        _wowQuizView.SetQuizController((WOWQuizController)currentQuizController);
        GetComponent<AlphaTransition>().StartFadeOut(1);
    }

    public void OnStartPicturesQuizClick()
    {
        currentQuizController = new PicturesQuizController(_picturesQuizView);
        _picturesQuizView.GetComponent<AlphaTransition>().StartFadeIn(1);
        currentQuizController.StartNewGame();
        _picturesQuizView.SetQuizController((PicturesQuizController)currentQuizController);
        GetComponent<AlphaTransition>().StartFadeOut(1);
    }
}
