using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    QuizLogic quizLogic;
    EndLogic endLogic;

    void Start()
    {
        quizLogic = FindObjectOfType<QuizLogic>();
        endLogic = FindObjectOfType<EndLogic>();

        quizLogic.gameObject.SetActive(true);
        endLogic.gameObject.SetActive(false);
        
    }

    void Update()
    {
        if (quizLogic.quizIsComplete)
        {
            quizLogic.gameObject.SetActive(false);
            endLogic.gameObject.SetActive(true);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
