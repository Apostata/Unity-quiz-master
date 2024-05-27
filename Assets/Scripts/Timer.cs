using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeToAnswer = 30f;
    [SerializeField] float timeToDisplayAnswer = 10f;
    

    public bool isAnswering =  true;
    float timerValue;
    float fillFraction;
    public bool loadNextQuestion;
    [SerializeField] Image timerImage;
    
    void Start()
    {
        timerValue = timeToAnswer;
    }

    void Update()
    {
        UpdateTimer();
    }

    void UpdateTimer()
    {
        timerValue -= Time.deltaTime;
       
            if (isAnswering){
                if (timerValue > 0)
                {
                  fillFraction = timerValue / timeToAnswer;
                } else {
                    timerValue = timeToDisplayAnswer;
                    isAnswering = false;
                }
            }
            else{
                if (timerValue > 0)
                {
                    fillFraction = timerValue / timeToDisplayAnswer;
                } else {
                    timerValue = timeToAnswer;
                    isAnswering = true;
                    loadNextQuestion = true;
                }
            }

        timerImage.fillAmount = fillFraction;
        
    }

    public void ResetTimer()
    {
        timerValue = 0;
    }
}
