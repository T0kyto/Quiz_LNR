public interface IQuizController
{
    public void StartNewGame();

    public void SetNewQuestion();

    public void SetLeaderBoardController(LeaderBoardController lbController);

    public void IncreaseScore();
    public void IncreaseTimer(float value);
    public void ResetTimer();

    public bool IsQuestionsRemains();
}
