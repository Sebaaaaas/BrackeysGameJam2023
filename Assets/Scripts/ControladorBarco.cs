using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorBarco : MonoBehaviour
{
    bool jugadorEnBarco = false;
    bool proximoABarco = false;

    public GameObject jugador;

    Rigidbody2D rb2d;

    #region VariablesNavegar

    Vector2 direccion;
    public float velocidad;
    public float maxVel;

    #endregion VariablesNavegar

    #region Controles

    public KeyCode keyCodeLEFT;
    public KeyCode keyCodeRIGHT;
    public KeyCode keyCodeSPACE;

    #endregion Controles

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        direccion = new Vector2();

        if (Input.GetKey(keyCodeLEFT))
            direccion.x = -1;
        else if (Input.GetKey(keyCodeRIGHT))
            direccion.x = 1;

        if(Input.GetKeyDown(keyCodeSPACE))
            if(proximoABarco && !jugadorEnBarco)
                SubirJugadorABarco();
            else if (jugadorEnBarco)
                BajarJugadorDeBarco();

    } 

    private void FixedUpdate()
    {
        if(jugadorEnBarco)
            rb2d.AddForce(direccion.normalized * velocidad);

        if (rb2d.velocity.magnitude > maxVel)
            rb2d.velocity = rb2d.velocity.normalized * maxVel;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>())
        {
            proximoABarco = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>())
        {
            proximoABarco = false;
        }
    }

    private void SubirJugadorABarco()
    {
        jugador.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        jugador.GetComponent<Rigidbody2D>().isKinematic = true;
        jugador.transform.parent = transform;
        jugador.transform.position = transform.position;


        jugadorEnBarco = true;
    }

    private void BajarJugadorDeBarco()
    {
        Debug.Log("bajando");
        jugador.GetComponent<Rigidbody2D>().isKinematic = false;
        jugador.transform.position.Set(transform.position.x, transform.position.y - 5, transform.position.z);
        jugador.transform.parent = null;

        jugadorEnBarco = false;
    }
}
