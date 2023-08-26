using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public int vida = 1;
    public float cantidadAirePerdida = 5.0f;
    
    public void RecibeDanio(int cantidad)
    {
        vida -= cantidad;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>())
            collision.GetComponent<PlayerController>().pierdeOxigeno(cantidadAirePerdida);
    }


}
