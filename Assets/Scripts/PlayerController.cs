using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    bool nadando = true;

    Rigidbody2D rb2d;

    #region StatsJugador
    int nivelOxigeno = 0;
    int nivelArpon = 0;
    int nivelVelocidad = 0;

    public float tiempoOxigenoTanque;
    public float danioArpon;
    public float potenciaLinterna;
    public float velocidad = 1.0f;
    #endregion StatsJugador

    #region VariablesNadar

    Vector2 direccion;
    public float maxVel = 5.0f;
    #endregion VariablesNadar

    #region Arpon
    [SerializeField] float tiempoEntreDisparos = 1.0f;
    float tiempoUltimoDisparo = 0.0f;
    public GameObject arpon;
    #endregion Arpon

    #region Controles

    public KeyCode keyCodeUP;
    public KeyCode keyCodeDOWN;
    public KeyCode keyCodeLEFT;
    public KeyCode keyCodeRIGHT;
    public KeyCode keyCodeSHOOT;

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

        if (nadando && Input.GetKeyDown(keyCodeSHOOT) && Time.time - tiempoEntreDisparos > tiempoUltimoDisparo)
            Shoot();

    }
    private void FixedUpdate()
    {
        if (nadando){
            rb2d.AddForce(direccion.normalized * velocidad);

            if(rb2d.velocity.magnitude > maxVel)
                rb2d.velocity = rb2d.velocity.normalized * maxVel;
        }
    }

    public void setNadando(bool nadando_)
    {
        nadando = nadando_;
    }
    private void Shoot()
    {
        tiempoUltimoDisparo = Time.time;

        Vector3 posRaton = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = posRaton - transform.position;

        float rotZ = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        GameObject arponAux = Instantiate(arpon, transform);
        arponAux.transform.rotation = Quaternion.Euler(0,0,rotZ);
    }
    
}
