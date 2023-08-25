using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Tienda : MonoBehaviour
{
    [SerializeField] GameObject Jugador;
    [SerializeField] GameObject panelInventarioTienda;
    [SerializeField] GameObject panelInventarioJugador;
    [SerializeField] GameObject textoAbrirTienda;
    public enum EstadosTienda { TodoCerrado, TextoAbrir, Abierto }
    EstadosTienda EstadoActual;

    public KeyCode KeyCodeABRIRTIENDA;
    public bool proximoATienda = false;

    #region ObjetosYCostes

    [System.Serializable]
    public struct ObjetoTienda
    {
        public int[] monedasPorNivel;

        public Sprite objetoVendidoTextura;
    }

    #endregion ObjetosYCostes

    [SerializeField] List<ObjetoTienda> objetosTienda;

    int dineroJugador = 100;

    private void Start()
    {
        EstadoActual = EstadosTienda.TodoCerrado;

        //asignamos las texturas y costes a los objetos de la tienda
        int i = 0;
        foreach(Transform hijo in panelInventarioTienda.transform)
        {
            //texturas objetos vendidos
            hijo.gameObject.GetComponent<Image>().sprite = objetosTienda[i].objetoVendidoTextura;

            //texto cantidad de monedas requeridas
            int nivelObjetoAMejorar = 1;
            string costeAux = "0/" + objetosTienda[i].monedasPorNivel[nivelObjetoAMejorar].ToString();
            hijo.Find("Dinero").Find("TextoDinero").GetComponent<Text>().text = costeAux;


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

    public void ganaDinero(int cant)
    {
        dineroJugador += cant;
    }
    public void gastaDinero(int cant)
    {
        dineroJugador -= cant;

    }
    public void compraObjetoTienda(int indexTienda)
    {
        int costeMonedasObjeto = objetosTienda[indexTienda].monedasPorNivel[Jugador.GetComponent<PlayerController>().nivelesStats_[indexTienda]];

        //si se tiene dinero suficiente
        if (dineroJugador >= costeMonedasObjeto)
        {
            //realiza compra
            dineroJugador -= costeMonedasObjeto;

            //subimos el nivel
            var jugador = Jugador.GetComponent<PlayerController>();
            jugador.subeNivel(indexTienda);

            if (jugador.nivelesStats_[indexTienda] == jugador.maxLvl) //nivel max, desactivamos el boton
            {
                panelInventarioTienda.transform.GetChild(indexTienda).Find("Image").gameObject.SetActive(false);
            }

            ActualizaTextosTiendaInventario();
        }

    }

    public void ActualizaTextosTiendaInventario() //actualizamos cuando abrimos la tienda o el inventario
    {
        //TIENDA
        int i = 0;
        foreach (Transform hijo in panelInventarioTienda.transform)
        {
            //texto cantidad de monedas requeridas tienda
            int nivelActualObjeto = Jugador.GetComponent<PlayerController>().nivelesStats_[i];
            
            string costeAux = dineroJugador + "/" + objetosTienda[i].monedasPorNivel[nivelActualObjeto - 1].ToString();
            hijo.Find("Dinero").Find("TextoDinero").GetComponent<Text>().text = costeAux;
            ++i;
        }

        //INVENTARIO JUGADOR

        //monedas
        panelInventarioJugador.transform.Find("ImagenDinero").Find("CantidadDinero").gameObject.GetComponent<Text>().text = dineroJugador.ToString();

        Transform padreObjetosMejorablesJugador = panelInventarioJugador.transform.Find("ObjetosMejorables");
        i = 0;
        foreach (Transform hijo in padreObjetosMejorablesJugador)
        {
            hijo.Find("NivelObjeto").Find("TextoNivelObjeto").GetComponent<Text>().text = "Lvl " + Jugador.GetComponent<PlayerController>().nivelesStats_[i];
            ++i;
        }
    }
}
