using System.Collections;
using System.Collections.Generic;
using TelemetradorNamespace;
using UnityEngine;

public class GravedadAire : MonoBehaviour
{
    public float gravedadAire = 3.0f;
    public float gravedadAgua = 0.1f;
    [SerializeField] GameObject jugador;
   // bool inicio = true;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.GetComponent<ControladorBarco>()) //esta feo que se controle asi pero por ahora funciona
        {
            collision.GetComponent<Rigidbody2D>().gravityScale = gravedadAire;
            //if (!inicio)
            //{
            float time = collision.gameObject.GetComponent<Temporizador>().getTime();
            Debug.Log(time);
            float minutes = Mathf.FloorToInt(time / 60);
            float seconds = Mathf.FloorToInt(time % 60);
            float milliSeconds = (time % 1) * 1000;
            Debug.Log(minutes + " " + seconds + " " + milliSeconds);
            Telemetrador.Instance().addEvent(new PLayerBreath(Time.time, minutes, seconds, milliSeconds));
            //}
            //inicio = false;
        }

        jugador.GetComponent<PlayerController>().reseteaTemporizador();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.GetComponent<ControladorBarco>())
            collision.GetComponent<Rigidbody2D>().gravityScale = gravedadAgua;
    }

}
