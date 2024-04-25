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
                Telemetrador.Instance().addEvent(new PLayerBreath(Time.time));
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
