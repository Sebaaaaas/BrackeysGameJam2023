using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    bool nadando = true;

    Rigidbody2D rb2d;

    #region VariablesNadar

    Vector2 direccion;
    public float velocidad = 1.0f;
    public float maxVel = 5.0f;

    #endregion VariablesNadar

    #region Controles

    public KeyCode keyCodeUP;
    public KeyCode keyCodeDOWN;
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
        //reseteamos a 0 por si no pulsa nada
        direccion = new Vector2();

        if (Input.GetKey(keyCodeUP))
            direccion.y = 1;

        if (Input.GetKey(keyCodeDOWN))
            direccion.y = -1;

        if (Input.GetKey(keyCodeLEFT))
            direccion.x = -1;

        if(Input.GetKey(keyCodeRIGHT))
            direccion.x = 1;

    }
    private void FixedUpdate()
    {
        if (nadando){
            rb2d.AddForce(direccion.normalized * velocidad);

            if(rb2d.velocity.magnitude > maxVel)
                rb2d.velocity = rb2d.velocity.normalized * maxVel;
        }
    }

    
}
