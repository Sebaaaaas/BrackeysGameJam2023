using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventarioJugador : MonoBehaviour
{
    public List<string> nombresMateriales;

    public List<Texture> ImagenesMateriales;

    public int maxMateriales = 5;

    Dictionary<string, int> materialesJugador; //nombre - cantActual - maxCantidad

    private void Start()
    {
        materialesJugador = new Dictionary<string, int>();

        foreach (string s in nombresMateriales)
        {
            materialesJugador.Add(s, 0);

        }
    }


}
