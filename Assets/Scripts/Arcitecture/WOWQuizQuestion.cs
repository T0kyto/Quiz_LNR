public class WOWQuizQuestion: AbstractQuizQuestion
{
    public WOWQuizQuestion(string questionImagePath, int answerIndex, string decorationImagePath, string infoImagePath) : base(questionImagePath)
    {
        _answerIndex = answerIndex;
        _decorationImagePath = decorationImagePath;
        _infoImagePath = infoImagePath;
    }
    
    
    private string _decorationImagePath;
    private int _answerIndex;
    private string _infoImagePath;
    
    public int AnswerIndex => _answerIndex;
    public string DecorationImagePath => _decorationImagePath;
    public string InfoImagePath => _infoImagePath;
}
