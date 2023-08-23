using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialSuelto : MonoBehaviour
{
    [SerializeField] Tienda.nomMat tipoMat;
    GameObject tienda;

    private void Start()
    {
        tienda = GameManager.instance.getTienda();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerController>() != null)
        {
            tienda.GetComponent<Tienda>().aniadeMaterialInventario(tipoMat);
            Destroy(gameObject);
        }
    }
}
