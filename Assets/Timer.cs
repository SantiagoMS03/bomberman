using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    private float timeLeft = 200.0f; // Use a float to account for fractions of a second

    // Update is called once per frame
    void Update()
    {
        // Subtract the time passed since the last frame
        timeLeft -= Time.deltaTime;

        // Update the timer text and make sure it doesn't go below 0
        timerText.text = "TIME " + Mathf.Max(0, Mathf.FloorToInt(timeLeft)).ToString();

        // Optional: Add behavior when the timer reaches 0
        if (timeLeft <= 0)
        {
            // Stop the countdown
            timeLeft = 0;

            // Add any actions you want to happen when the timer runs out
            // For example: End the game, lose a life, etc.
        }
    }
}
