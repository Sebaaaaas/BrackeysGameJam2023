using System.Collections;
using System.Collections.Generic;
using TelemetradorNamespace;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] GameObject Tienda;
    [SerializeField] GameObject Jugador;
    [SerializeField] GameObject Canvas;

    float lastTime=0.0f;

    void Awake()
    {
        //if (instance == null)
            instance = this;
        //else
        //    Destroy(gameObject);

        //DontDestroyOnLoad(gameObject);

        //if (Telemetrador.Init(Formatos.JSON, "prueba", Medio.Archivo))
        //{
        //    Debug.Log("Iniciado");
        //   Telemetrador.Instance().
        //}
        Telemetrador.Instance().addEvent(new LevelStart(Time.time));
        Debug.Log(Telemetrador.Instance().idSesion);
    }
    private void Start()
    {

    }
    private void Update()
    {
       
        Telemetrador.Instance().Update(Time.deltaTime);
    }

    private void OnApplicationQuit() {
        
        //Telemetrador.Instance().endSession(Time.time,false);
        Telemetrador.Instance().endQuit(Time.time);
    }

    public GameObject getTienda()
    {
        return Tienda;
    }

    public GameObject getJugador()
    {
        return Jugador;
    }

    public GameObject getCanvas()
    {
        return Canvas;
    }

}
