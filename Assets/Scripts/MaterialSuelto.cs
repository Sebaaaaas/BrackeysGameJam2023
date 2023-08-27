using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MaterialSuelto : MonoBehaviour
{
    //[SerializeField] Tienda.nomMat tipoMat;
    GameObject tienda;
    public int valorMonetario = 10;
    [SerializeField] GameObject moneyGainedObject;
    GameObject canvas;
    private void Start()
    {
        tienda = GameManager.instance.getTienda();
        canvas = GameManager.instance.getCanvas();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if(collision.gameObject.GetComponent<PlayerController>() != null)
        {
            tienda.GetComponent<Tienda>().ganaDinero(valorMonetario);
            GameObject aux = Instantiate(moneyGainedObject, canvas.transform);
            Destroy(aux, 2);
            aux.GetComponent<TextMeshProUGUI>().text = '+' + valorMonetario.ToString();
            tienda.GetComponent<Tienda>().ActualizaTextosTiendaInventario();
            Destroy(gameObject);
        }
        
    }
}
