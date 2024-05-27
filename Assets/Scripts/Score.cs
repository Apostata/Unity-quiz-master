using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    // Start is called before the first frame update
    int correctAnswers = 0;
    int questionsViwed = 0;
    
    

   public int GetCorrectAnswers()
    {
        return  correctAnswers;
    }

    public void AddCorrectAnswer()
    {
        correctAnswers++;
    }

    public void AddQuestionViewed()
    {
        questionsViwed++;
    }

    public int GetQuestionsViewed()
    {
        return questionsViwed;
    }

    public int CalculateScore()
    {
        int roundedScore = correctAnswers == 0? 0 : Mathf.RoundToInt(correctAnswers * 100 / questionsViwed);
        return roundedScore;
    }

    public void ResetScore()
    {
        correctAnswers = 0;
        questionsViwed = 0;
    }
}
