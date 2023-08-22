using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public List<string> nombresMateriales;
    public List<Texture> ImagenesMateriales;

    //public Dictionary<string, Texture> materiales;
    struct Material
    {
        public string nombre;
        public Texture tex;
    }

    List<Material> materiales;
    private void Start()
    {
        int i = 0;
        foreach(string nombre in nombresMateriales)
        {
            //Material mat = new Material(nombre, ImagenesMateriales[0]);

            //materiales.Add(new Material(nombre, ImagenesMateriales[i]));
        }
    }
}
