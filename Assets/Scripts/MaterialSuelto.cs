using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialSuelto : MonoBehaviour
{
    //[SerializeField] Tienda.nomMat tipoMat;
    GameObject tienda;
    int valorMonetario = 10;
    private void Start()
    {
        tienda = GameManager.instance.getTienda();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerController>() != null)
        {
            tienda.GetComponent<Tienda>().ganaDinero(valorMonetario);
            tienda.GetComponent<Tienda>().ActualizaTextosTiendaInventario();
            Destroy(gameObject);
        }
    }
}
