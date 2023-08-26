using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Tienda;

public class PlayerController : MonoBehaviour
{
    public enum EstadosJugador { EnBarca, Nadando }
    EstadosJugador EstadoActual;

    bool nadando = true;

    [SerializeField] GameObject burbujas;

    Rigidbody2D rb2d;

    [SerializeField] GameObject panelInventario;

    [Header("Animaciones")]
    private Animator animator;


    #region StatsJugador

    public int[] nivelesStats_;
    public int maxLvl = 3;

    float tiempoOxigenoTanque;   //0
    float danioArpon;            //1
    float potenciaLinterna;      //2
    float velocidad;             //3
    
    [Serializable]
    public struct statsNiveles //el oxigeno tiene X segundos al nivel 0, Y al 1, Z al 2...
    {
        public string nombreStat;
        public int[] nivelesStats;
    }
    public statsNiveles[] stats; //0 ox, 1 arp, 2 lint, 3 vel

    #endregion StatsJugador

    #region VariablesNadar

    Vector2 direccion;
    public float maxVel = 5.0f;
   
    Temporizador temporizador;
    [SerializeField] GameObject zonaOscura;
    SpriteRenderer zonaOscuraRenderer;
    public float comienzoOscuridad, maxOscuridad;
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

    [SerializeField] GameObject spawnpointJugador;
    [SerializeField] GameObject panelDerrota;

    private void Awake()
    {
        nivelesStats_ = new int[4] { 1, 1, 1, 1 };
    }
    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        EstadoActual = EstadosJugador.Nadando;

        temporizador = GetComponent<Temporizador>();
        //temporizador.setTime(tiempoOxigenoTanque);

        actualizaStatsJugador(); //ponemos las stats basicas de nivel 1

        zonaOscuraRenderer = zonaOscura.GetComponent<SpriteRenderer>();
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

        animator.SetFloat("Direccion", direccion.x);

        if (Input.GetKey(keyCodeABRIRINVENTARIO))
            panelInventario.SetActive(true);
        else if(Input.GetKeyUp(keyCodeABRIRINVENTARIO))
            panelInventario.SetActive(false);

        //manejamos la oscuridad alrededor del jugador
        Color tmp = zonaOscuraRenderer.color;

        if (transform.position.y > comienzoOscuridad)
            tmp.a = 0f;
        else if (transform.position.y < maxOscuridad)
            tmp.a = 255f;
        else
        {
            //calculamos porcentaje de la zona que llevamos
            float prcnt = ((transform.position.y - comienzoOscuridad) * 100) / (maxOscuridad - comienzoOscuridad);
            
            tmp.a = (prcnt / 100);
        }

        zonaOscuraRenderer.color = tmp;
    }
    private void FixedUpdate()
    {
        if (nadando){
            rb2d.AddForce(direccion.normalized * velocidad * Time.fixedDeltaTime);

            if(rb2d.velocity.magnitude > maxVel)
                rb2d.velocity = rb2d.velocity.normalized * maxVel;
        }
        animator.SetFloat("Horizontal", Math.Abs(rb2d.velocity.x));
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
    public void cambiaEstado(EstadosJugador nuevoEstado)
    {
        EstadoActual = nuevoEstado;

        switch (nuevoEstado)
        {
            case EstadosJugador.EnBarca:
                burbujas.SetActive(false);
                setNadando(false);
                desactivaTemporizador();
                //ponemos animaciones
                break;
            case EstadosJugador.Nadando:
                burbujas.SetActive(true);
                setNadando(true);
                activaTemporizador();
                break;
            default:
                break;
        }
    }
    public void subeNivel(int objetoAMejorar) //se podria hacer con enum pero esto vale por ahora
    {
        nivelesStats_[objetoAMejorar]++;
        actualizaStatsJugador();
    }
    private void actualizaStatsJugador()
    {
        tiempoOxigenoTanque = stats[0].nivelesStats[nivelesStats_[0]-1]; //el tiempo que dura el oxigeno es = a las stats[0]
                                                                   //(corresponde a oxigeno) . de nivel[nivel actual de oxigeno]
        danioArpon = stats[1].nivelesStats[nivelesStats_[1]-1];
        potenciaLinterna = stats[2].nivelesStats[nivelesStats_[2] - 1];
        velocidad = stats[3].nivelesStats[nivelesStats_[3] - 1];
    }

    #region Temporizador
    private void activaTemporizador()
    {
        temporizador.timeText.gameObject.SetActive(true);
        reseteaTemporizador();
        temporizador.pauseTimer(false);
    }
    public void reseteaTemporizador()
    {
        temporizador.setTime(tiempoOxigenoTanque);
    }
    private void desactivaTemporizador()
    {
        temporizador.timeText.gameObject.SetActive(false);
        temporizador.pauseTimer(true);

    }

    #endregion Temporizador
    public void pierdeOxigeno(float cantidad)
    {
        temporizador.reduceTime(cantidad);
    }
    public void muereJugador()
    {
        panelDerrota.SetActive(true);
        transform.position = spawnpointJugador.transform.position;

    }
    public void reseteaJugador()
    {
        reseteaTemporizador();
        //coloca al jugador donde corresponde
        panelDerrota.SetActive(false);
    }
    public void jugadorGana()
    {
        SceneManager.LoadScene("WinScreen");
    }
}
