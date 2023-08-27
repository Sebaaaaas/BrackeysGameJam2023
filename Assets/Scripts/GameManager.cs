using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] GameObject Tienda;
    [SerializeField] GameObject Jugador;
    [SerializeField] GameObject Canvas;

    void Awake()
    {
        //if (instance == null)
            instance = this;
        //else
        //    Destroy(gameObject);

        //DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {

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
