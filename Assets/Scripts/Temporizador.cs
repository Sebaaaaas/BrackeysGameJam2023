using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Temporizador : MonoBehaviour
{
    float timeRemaining = 10;
    bool paused = false;
    public TextMeshProUGUI timeText;
    void Update()
    {
        if (!paused && timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }

        DisplayTime(timeRemaining);

        if (timeRemaining <= 0)
            GetComponent<PlayerController>().muereJugador();

    }
    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        float milliSeconds = (timeToDisplay % 1) * 1000;

        if(timeToDisplay < 0)
            timeText.text = "00:00:000";
        else
            timeText.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliSeconds);


    }

    public void pauseTimer(bool pause)
    {
        paused = pause;
    }
    public void setTime(float newTime)
    {
        timeRemaining = newTime;
    }
    public void reduceTime(float amount)
    {
        timeRemaining -= amount;
    }
}
