using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public int vida = 2;
    public float cantidadAirePerdida = 5.0f;
    public GameObject materialDrop;    
    
    public void RecibeDanio(int cantidad)
    {
        vida -= cantidad;
        if(vida <= 0)
            muere();
    }

    private void muere()
    {
        Instantiate(materialDrop, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>())
            collision.GetComponent<PlayerController>().pierdeOxigeno(cantidadAirePerdida);
    }


}
