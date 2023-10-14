public class QuizResult
{
    public int correctAnswers;
    public string playerName;
    public long timestamp;

    public QuizResult(int correctAnswers, string playerName, long timestamp)
    {
        this.correctAnswers = correctAnswers;
        this.playerName = playerName;
        this.timestamp = timestamp;
    }
}