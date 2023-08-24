using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoAlmeja : MonoBehaviour
{
    float distanciaMordisco = 2.0f;
    public bool muerde = false;

    GameObject Jugador;
    private void Start()
    {
        Jugador = GameManager.instance.getJugador();
    }

    private void Update()
    {
        muerde = false;
        if ((Jugador.transform.position - transform.position).magnitude < distanciaMordisco)
            muerde = true;
    }

}
