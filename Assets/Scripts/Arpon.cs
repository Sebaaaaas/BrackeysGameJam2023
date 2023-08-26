using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arpon : MonoBehaviour
{
    [SerializeField] float tiempoDeVida = 1.0f;

    public float velocidad;
    float tiempoCreacion;

    private void Start()
    {
        tiempoCreacion = Time.time;
        GetComponent<Rigidbody2D>().AddForce(transform.right * velocidad);
    }
    private void Update()
    {
        if(Time.time > tiempoCreacion + tiempoDeVida)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemigo>()){
            collision.GetComponent<Enemigo>().RecibeDanio(1);
        }
    }
}
