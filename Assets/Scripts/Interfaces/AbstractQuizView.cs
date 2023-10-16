using UnityEngine;

public abstract class AbstractQuizView: MonoBehaviour
{
    public abstract void SetQuestion(AbstractQuizQuestion question);
    public abstract void SetQuestionCounterText(int questionAmount, int currentQuestionId);
    public abstract void SetQuizController(IQuizController controller);
}
