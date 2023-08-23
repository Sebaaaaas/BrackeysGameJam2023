using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Tienda : MonoBehaviour
{

    [SerializeField] GameObject panelInventarioTienda;
    [SerializeField] GameObject panelInventarioJugador;
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

    string[] nombresDeMateriales = System.Enum.GetNames(typeof(nomMat)); //convertimos el enum en string para poder usarlo(dentro de region ObjetosYCostes)
    Dictionary<string, int> inventarioJugador;
    int dineroJugador = 3;
    [SerializeField] int maxMaterialInventario = 99; //por ahora fijo para todos

    private void Start()
    {
        EstadoActual = EstadosTienda.TodoCerrado;

        //asignamos las texturas y costes a los objetos de la tienda
        int i = 0;
        foreach(Transform hijo in panelInventarioTienda.transform)
        {
            //texturas objetos vendidos
            hijo.gameObject.GetComponent<Image>().sprite = objetosTienda[i].objetoVendidoTextura;

            //texturas materiales requeridos para comprar un objeto
            Transform costeMaterial = hijo.Find("MaterialesRequeridos").Find("Material1"); //el transform que contiene todos los valores de materiales y monedas
            costeMaterial.GetComponent<Image>().sprite = objetosTienda[i].precio.mat.tex;

            //texto cantidad de materiales requeridos
            string costeAux = "0/" + objetosTienda[i].precio.cantMaterial.ToString();
            costeMaterial.Find("TextoMaterial").GetComponent<Text>().text = costeAux;

            //texto cantidad de monedas requeridas
            costeAux = "0/" + objetosTienda[i].precio.monedas.ToString();
            hijo.Find("MaterialesRequeridos").Find("Dinero").Find("TextoDinero").GetComponent<Text>().text = costeAux;


            ++i;
        }


        inventarioJugador = new Dictionary<string, int>();
        //ponemos los materiales del inventario del jugador a 0 despues de añadirlos a su diccionario de materiales
        foreach(string s in nombresDeMateriales)
        {
            inventarioJugador.Add(s, 0);
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

        switch (nuevoEstado)
        {
            case EstadosTienda.TodoCerrado:
                textoAbrirTienda.SetActive(false);
                panelInventarioTienda.SetActive(false);
                break;
            case EstadosTienda.TextoAbrir:
                textoAbrirTienda.SetActive(true);
                panelInventarioTienda.SetActive(false);
                break;
            case EstadosTienda.Abierto:
                textoAbrirTienda.SetActive(false);
                panelInventarioTienda.SetActive(true);
                break;
            default:
                break;
        }
    }

    public EstadosTienda getEstadoActual()
    {
        return EstadoActual;
    }

    public void aniadeMaterialInventario(nomMat tipoMaterial) //solo de 1 en 1 por ahora
    {
        inventarioJugador[tipoMaterial.ToString()]++;
    }

    private void quitaMaterialInventario(nomMat tipoMaterial, int cant)
    {
        inventarioJugador[tipoMaterial.ToString()] -= cant;
    }

    private void compraObjetoTienda(int indexTienda)
    {
        int costeMonedasObjeto = objetosTienda[indexTienda].precio.monedas;
        int costeMaterialObjeto = objetosTienda[indexTienda].precio.cantMaterial;
        string nombreMaterialRequerido = objetosTienda[indexTienda].precio.mat.nombre.ToString();
        //si se tienen los materiales
        if (dineroJugador > costeMonedasObjeto && inventarioJugador[nombreMaterialRequerido] > costeMaterialObjeto)
        {
            //realiza compra
            dineroJugador -= costeMonedasObjeto;
            inventarioJugador[nombreMaterialRequerido] -= costeMaterialObjeto;

            //subimos el nivel
        }
    }

    public void ActualizaTextosTiendaInventario() //actualizamos cuando abrimos la tienda o el inventario
    {
        int i = 0;
        foreach (Transform hijo in panelInventarioTienda.transform)
        {
            Transform costeMaterial = hijo.Find("MaterialesRequeridos").Find("Material1"); //el transform que contiene todos los valores de materiales y monedas

            //texto cantidad de materiales requeridos tienda
            string costeAux = inventarioJugador[objetosTienda[i].precio.mat.nombre.ToString()] + "/" + objetosTienda[i].precio.cantMaterial.ToString();
            costeMaterial.Find("TextoMaterial").GetComponent<Text>().text = costeAux;

            //texto cantidad de monedas requeridas tienda
            costeAux = dineroJugador + "/" + objetosTienda[i].precio.monedas.ToString();
            hijo.Find("MaterialesRequeridos").Find("Dinero").Find("TextoDinero").GetComponent<Text>().text = costeAux;
            ++i;
        }

        Transform padreMaterialesInventarioJugador = panelInventarioJugador.transform.Find("MaterialesInventario");
        i = 0;
        foreach(Transform hijo in padreMaterialesInventarioJugador)
        {
            hijo.Find("CantidadMaterial").GetComponent<Text>().text = "x" + inventarioJugador[nombresDeMateriales[i]];
            ++i;
        }
    }
}
