public class PicturesQuizQuestion: AbstractQuizQuestion
{
    public PicturesQuizQuestion(string question, string[] answers, int answerIndex, string imagePath) : base(question, answers)
    {
        this._answerIndex = answerIndex;
        this._imagePath = imagePath;
    }

    private readonly int _answerIndex;
    private readonly string _imagePath;
    
    public int AnswerIndex => _answerIndex;
    public string ImagePath => _imagePath;
}