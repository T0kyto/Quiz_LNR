using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractQuizQuestion
{
    public AbstractQuizQuestion(string questionImagePath)
    {
        this._questionImagePath = questionImagePath;
    }
    
    private readonly string _questionImagePath;

    public string QuestionImagePath => _questionImagePath;
    
}
