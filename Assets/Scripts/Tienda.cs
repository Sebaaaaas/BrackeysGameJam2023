using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Tienda : MonoBehaviour
{
    public bool proximoATienda = false;

    [SerializeField] GameObject panelInventario;
    [SerializeField] GameObject textoAbrirTienda;
    public enum EstadosTienda { TodoCerrado, TextoAbrir, Abierto }
    EstadosTienda EstadoActual;

    public KeyCode KeyCodeABRIRTIENDA;

    private void Start()
    {
        EstadoActual = EstadosTienda.TodoCerrado;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<ControladorBarco>() &&
            collision.GetComponent<ControladorBarco>().GetJugadorEnBarco())
        {
            proximoATienda = true;
            cambiaEstado(EstadosTienda.TextoAbrir);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<ControladorBarco>())
        {
            proximoATienda = false;
            cambiaEstado(EstadosTienda.TodoCerrado);
        }

    }
    private void Update()
    {
        if (EstadoActual == EstadosTienda.TextoAbrir && Input.GetKeyDown(KeyCodeABRIRTIENDA))
            cambiaEstado(EstadosTienda.Abierto);
        else if (EstadoActual == EstadosTienda.Abierto && Input.GetKeyDown(KeyCodeABRIRTIENDA))
            cambiaEstado(EstadosTienda.TextoAbrir);
    }

    public void cambiaEstado(EstadosTienda nuevoEstado)
    {
        EstadoActual = nuevoEstado;

        switch (nuevoEstado)
        {
            case EstadosTienda.TodoCerrado:
                textoAbrirTienda.SetActive(false);
                panelInventario.SetActive(false);
                break;
            case EstadosTienda.TextoAbrir:
                textoAbrirTienda.SetActive(true);
                panelInventario.SetActive(false);
                break;
            case EstadosTienda.Abierto:
                textoAbrirTienda.SetActive(false);
                panelInventario.SetActive(true);
                break;
            default:
                break;
        }
    }

    public EstadosTienda getEstadoActual()
    {
        return EstadoActual;
    }
}
