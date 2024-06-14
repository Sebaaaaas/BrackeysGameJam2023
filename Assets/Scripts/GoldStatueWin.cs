using System.Collections;
using System.Collections.Generic;
using TelemetradorNamespace;
using UnityEngine;

public class GoldStatueWin : MonoBehaviour
{
    [SerializeField] GameObject jugador;
    float time = 0f;

    private void Update()
    {
        time += Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>() != null)
        {
            collision.GetComponent<PlayerController>().jugadorGana();
            float minutes = Mathf.FloorToInt(time / 60);
            float seconds = Mathf.FloorToInt(time % 60);
            float milliSeconds = (time % 1) * 1000;
            Telemetrador.Instance().endSession(Time.time, true,minutes,seconds,milliSeconds);
        }
    }
}
