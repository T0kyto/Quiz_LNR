public class WOWQuizQuestion: AbstractQuizQuestion
{
    public WOWQuizQuestion(string question, string[] answers, string info, string infoPicturePath) : base(question, answers)
    {
        _info = info;
        _infoPicturePath = infoPicturePath;
    }

    private int _answerIndex;
    private string _info;
    private string _infoPicturePath;
    
    public int AnswerIndex => _answerIndex;
    public string Info => _info;
    public string InfoPicturePath => _infoPicturePath;
}
