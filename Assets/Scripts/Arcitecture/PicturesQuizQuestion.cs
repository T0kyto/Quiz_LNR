public class PicturesQuizQuestion: AbstractQuizQuestion
{
    public PicturesQuizQuestion(string questionImagePath, int answerIndex, string decorationImagePath) : base(questionImagePath)
    {
        _answerIndex = answerIndex;
        _decorationImagePath = decorationImagePath;
    }
    
    private string _decorationImagePath;
    private int _answerIndex;
    
    public int AnswerIndex => _answerIndex;
    public string DecorationImagePath => _decorationImagePath;
}