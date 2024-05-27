using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;

public class QuizLogic : MonoBehaviour
{
    [Header("Questions")]
    QuestionSO currentQuestion;
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
    [SerializeField] TextMeshProUGUI questionText;
    [Header("Answer Buttons")]
    [SerializeField] GameObject[] answerButtons;
    [SerializeField] Sprite IdleButtonSprite;
    [SerializeField] Sprite CorrectButtonSprite;

    [Header("ProgressBar")]
    [SerializeField] Slider progressBar;

    [Header("Score")]
    [SerializeField] TextMeshProUGUI scoreText;
    Timer timer;

    Score score;

    bool hasAnswered = false;

    Transform buttons;

    int correctAnswerIndex;

    public bool quizIsComplete = false;
    void Start()
    {
        progressBar.maxValue = questions.Count;
        progressBar.value = 1;
        progressBar.interactable = false;
        
        timer = FindObjectOfType<Timer>();
        score = FindObjectOfType<Score>();
        score.ResetScore();
       UpdateScore();

        GetRandomQuestion();
        DisplayQuestion();
        
    }

    void Update() //game loop
    {
        if (timer.loadNextQuestion)
        {
            hasAnswered = false;
            NextQuestion();
            timer.loadNextQuestion = false;

        }
        else if(!timer.isAnswering && !hasAnswered)
        {
            DisplayAnswer(-1); //any index that is not correct
            ToggleButtonsState(false);
        }

    }

    void DisplayAnswer(int index)
    {
        score.AddQuestionViewed();

        if (index == correctAnswerIndex)
        {
            questionText.text = "That is right!";
            answerButtons[index].GetComponent<Image>().sprite = CorrectButtonSprite;
            score.AddCorrectAnswer();
            
        }
        else
        {
            hasAnswered = true;
            questionText.text = $"Unfortunately it's wrong! the correct answer is \"{currentQuestion.GetAnswer(correctAnswerIndex)}\"!";
            answerButtons[correctAnswerIndex].GetComponent<Image>().sprite = CorrectButtonSprite;
        }
        UpdateScore();
        
    }

    public void ButtonClicked(int index){
        hasAnswered = true;
        DisplayAnswer(index);
        ToggleButtonsState(false);
        timer.ResetTimer();
    }

    public void DisplayQuestion(){
        questionText.text = currentQuestion.GetQuestion();

        for (int i = 0; i < answerButtons.Length; i++)
        {
            Button button = answerButtons[i].GetComponent<Button>();
            button.GetComponentInChildren<TextMeshProUGUI>().text = currentQuestion.GetAnswer(i);
        }
    }

    void ToggleButtonsState(bool active)
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Button button =answerButtons[i].GetComponent<Button>();
            button.interactable = active;
        }
    }

    void ResetButtonSprite()
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Image button =answerButtons[i].GetComponent<Image>();
            button.sprite = IdleButtonSprite;
        }
    }

    void GetRandomQuestion()
    {
        int randomIndex = Random.Range(0, questions.Count);
        currentQuestion = questions[randomIndex];
        correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex();

        if(questions.Contains(currentQuestion)){
            questions.Remove(currentQuestion);
        }
    }

    public void NextQuestion()
    {

        if(questions.Count != 0)
        {
            progressBar.value++;
            hasAnswered = false;
            ToggleButtonsState(true);
            GetRandomQuestion();
            ResetButtonSprite();
            DisplayQuestion();
        } else{
            quizIsComplete = true;
        }
        
    }    

    void UpdateScore(){
        scoreText.text = $"Score: {score.CalculateScore()}%";
    }
}
