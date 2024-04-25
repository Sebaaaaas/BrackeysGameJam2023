using System.Collections;
using System.Collections.Generic;
using TelemetradorNamespace;
using UnityEngine;

public class GoldStatueWin : MonoBehaviour
{
    [SerializeField] GameObject jugador;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>() != null)
        {
            collision.GetComponent<PlayerController>().jugadorGana();
           // Telemetrador.Instance().endSession(Time.time, true);
        }
    }
}
