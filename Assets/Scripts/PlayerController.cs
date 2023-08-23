using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static Tienda;

public class PlayerController : MonoBehaviour
{
    public enum EstadosJugador { EnBarca, Nadando }
    EstadosJugador EstadoActual;

    bool nadando = true;

    [SerializeField] GameObject burbujas;

    Rigidbody2D rb2d;

    [SerializeField] GameObject panelInventario;

    #region StatsJugador
    int nivelOxigeno = 0;
    int nivelArpon = 0;
    int nivelLinterna = 0;
    int nivelVelocidad = 0;

    float tiempoOxigenoTanque;   //0
    float danioArpon;            //1
    float potenciaLinterna;      //2
    float velocidad;             //3

    [Serializable]
    public struct statsNiveles
    {
        public string nombreStat;
        public int[] nivelesStats;
    }
    public statsNiveles[] stats;
    //public Dictionary<string, List<int>> statsNiveles = new Dictionary<string, List<int>>();
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
    public KeyCode keyCodeABRIRINVENTARIO;

    #endregion Controles


    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        EstadoActual = EstadosJugador.Nadando;

        actualizaStatsJugador(); //ponemos las stats basicas de nivel 1
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

        if (Input.GetKey(keyCodeABRIRINVENTARIO))
            panelInventario.SetActive(true);
        else if(Input.GetKeyUp(keyCodeABRIRINVENTARIO))
            panelInventario.SetActive(false);


    }
    private void FixedUpdate()
    {
        if (nadando){
            rb2d.AddForce(direccion.normalized * velocidad * Time.fixedDeltaTime);

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

    public void subeNivel(string objetoAMejorar) //se podria hacer con enum pero esto vale por ahora
    {
        switch (objetoAMejorar)
        {
            case "tiempoOxigenoTanque":
                nivelOxigeno++;
                break;
            case "danioArpon":
                nivelArpon++;
                break;
            case "potenciaLinterna":
                nivelLinterna++;
                break;
            case "velocidad":
                nivelVelocidad++;
                break;
            default:
                break;
        }
    }
    public void cambiaEstado(EstadosJugador nuevoEstado)
    {
        EstadoActual = nuevoEstado;

        switch (nuevoEstado)
        {
            case EstadosJugador.EnBarca:
                burbujas.SetActive(false);
                setNadando(false);
                //ponemos animaciones
                break;
            case EstadosJugador.Nadando:
                burbujas.SetActive(true);
                setNadando(true);
                break;
            default:
                break;
        }
    }

    private void actualizaStatsJugador()
    {
        tiempoOxigenoTanque = stats[0].nivelesStats[nivelOxigeno]; //el tiempo que dura el oxigeno es = a las stats[0]
                                                                   //(corresponde a oxigeno) . de nivel[nivel actual de oxigeno]
        danioArpon = stats[1].nivelesStats[nivelArpon];
        potenciaLinterna = stats[2].nivelesStats[nivelLinterna];
        velocidad = stats[3].nivelesStats[nivelVelocidad];
    }
}
