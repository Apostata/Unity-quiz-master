using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndLogic : MonoBehaviour
{
    Score score;
    void Start()
    {
        TextMeshProUGUI endText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        score = FindObjectOfType<Score>();
        score.CalculateScore();
        endText.text = $"Congratulations!\nYou scored {score.CalculateScore()}%";
    }
}
