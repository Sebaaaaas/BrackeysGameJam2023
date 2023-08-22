using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Tienda : MonoBehaviour
{

    [SerializeField] GameObject panelInventario;
    [SerializeField] GameObject textoAbrirTienda;
    public enum EstadosTienda { TodoCerrado, TextoAbrir, Abierto }
    EstadosTienda EstadoActual;

    public KeyCode KeyCodeABRIRTIENDA;
    public bool proximoATienda = false;

    #region ObjetosYCostes

    public enum nomMat { algas, carne1, carne2, perlas };

    [System.Serializable]
    public struct Mterial
    {
        public nomMat nombre;
        public Sprite tex;
    }

    [System.Serializable]
    public struct Precio
    {
        public int monedas;
        public Mterial mat;
        public int cantMaterial;
    }

    [System.Serializable]
    public struct ObjetoTienda
    {
        public Precio precio;
        public Sprite objetoVendidoTextura;
    }

    #endregion ObjetosYCostes

    [SerializeField] List<ObjetoTienda> objetosTienda;

    Dictionary<string, int> inventarioJugador;

    private void Start()
    {
        EstadoActual = EstadosTienda.TodoCerrado;
        //asignamos las texturas a los objetos de la tienda
        int i = 0;
        foreach(Transform hijo in panelInventario.transform)
        {
            hijo.gameObject.GetComponent<Image>().sprite = objetosTienda[i].objetoVendidoTextura;
            hijo.Find("MaterialesRequeridos").Find("Material1").GetComponent<Image>().sprite = objetosTienda[i].precio.mat.tex;
            ++i;
        }

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
        Debug.Log("cambioEstado " + nuevoEstado);

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
