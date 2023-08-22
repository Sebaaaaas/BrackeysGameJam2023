using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Tienda : MonoBehaviour
{
    bool mostrarTienda = false;
    [SerializeField] GameObject panelInventario;

    public KeyCode KeyCodeABRIRTIENDA;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<ControladorBarco>())
            mostrarTienda = true;
    }

    private void Update()
    {
        if (mostrarTienda && Input.GetKey(KeyCodeABRIRTIENDA))
            MuestraOpcionTienda(true);

    }

    //enseñamos panel de texto que indica como abrir la tienda
    private void MuestraOpcionTienda(bool mostrar)
    {
        panelInventario.SetActive(mostrar);
    }

    //abrimos la tienda para comprar
    private void AbrirTienda()
    {

    }
}
