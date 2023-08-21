using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorEnemigo : MonoBehaviour
{
    [SerializeField] float timeBetweenDashes = 1.0f;
    float lastDashTime = 0.0f;
    [SerializeField] GameObject jugador;

    public float velocidad;
    Rigidbody2D rbd2;

    private void Start()
    {
        rbd2 = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(Time.time - timeBetweenDashes > lastDashTime)
        {
            lastDashTime = Time.time;

            rbd2.AddForce((jugador.transform.position - transform.position).normalized * velocidad);
        }
    }
}
