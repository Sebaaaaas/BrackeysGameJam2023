using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorBarco : MonoBehaviour
{
    bool jugadorEnBarco = false;
    bool proximoABarco = false;

    public GameObject jugador;
    public GameObject tienda;

    Rigidbody2D rb2d;

    #region VariablesNavegar

    Vector2 direccion;
    public float velocidad;
    public float maxVel;

    #endregion VariablesNavegar

    #region Controles

    public KeyCode keyCodeLEFT;
    public KeyCode keyCodeRIGHT;
    public KeyCode keyCodeTOFROMBOAT;

    #endregion Controles

    [SerializeField] GameObject camaraNadar;
    [SerializeField] GameObject camaraBarco;

    public float minIz, maxDer;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        direccion = new Vector2();

        if (jugadorEnBarco && Input.GetKey(keyCodeLEFT))
        {
            direccion.x = -1;
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (jugadorEnBarco && Input.GetKey(keyCodeRIGHT))
        {
            direccion.x = 1;
            GetComponent<SpriteRenderer>().flipX = true;
        }

        if(Input.GetKeyDown(keyCodeTOFROMBOAT))
            if(proximoABarco && !jugadorEnBarco)
                SubirJugadorABarco();
            else if (jugadorEnBarco)
                BajarJugadorDeBarco();

        if(transform.position.x < minIz)
            transform.position = new Vector2(minIz, transform.position.y);
        else if(transform.position.x > maxDer)
            transform.position = new Vector2(maxDer, transform.position.y);

    } 

    private void FixedUpdate()
    {
        if(jugadorEnBarco)
            rb2d.AddForce(direccion.normalized * velocidad * Time.fixedDeltaTime);

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

    public bool GetJugadorEnBarco()
    {
        return jugadorEnBarco;
    }

    private void SubirJugadorABarco()
    {
        //cambio camara
        camaraBarco.SetActive(true);
        camaraNadar.SetActive(false);

        jugador.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        jugador.GetComponent<Rigidbody2D>().isKinematic = true;
        jugador.transform.parent = transform;
        jugador.transform.position = transform.position;
        jugador.GetComponent<PlayerController>().cambiaEstado(PlayerController.EstadosJugador.EnBarca);

        if(tienda.GetComponent<Tienda>().proximoATienda)
            tienda.GetComponent<Tienda>().cambiaEstado(Tienda.EstadosTienda.TextoAbrir);
        

        jugadorEnBarco = true;
    }

    private void BajarJugadorDeBarco()
    {
        camaraBarco.SetActive(false);
        camaraNadar.SetActive(true);

        jugador.GetComponent<Rigidbody2D>().isKinematic = false;
        jugador.transform.position.Set(transform.position.x, transform.position.y - 5, transform.position.z);
        jugador.transform.parent = null;

        jugador.GetComponent<PlayerController>().cambiaEstado(PlayerController.EstadosJugador.Nadando);

        //si la tienda estaba abierta la cerramos
        tienda.GetComponent<Tienda>().cambiaEstado(Tienda.EstadosTienda.TodoCerrado);

        jugadorEnBarco = false;
    }
}
