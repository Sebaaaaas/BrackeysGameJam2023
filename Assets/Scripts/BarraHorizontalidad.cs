using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarraHorizontalidad : MonoBehaviour
{
    [SerializeField] GameObject Barra;
    GameObject indicadorHorizonte;

    public float minIz, maxDer, minPosEnBarra, maxPosEnBarra;
    private void Start()
    {
        indicadorHorizonte = Barra.transform.Find("IndicadorProfundidad").gameObject;
    }
    private void Update()
    {

        float porcentajeHorizonte = (100 * (transform.position.x - minIz)) / (maxDer - minIz);
        //float prcnt = ((transform.position.y - comienzoOscuridad) * 100) / (maxOscuridad - comienzoOscuridad);


        if (porcentajeHorizonte < 0)
            indicadorHorizonte.transform.localPosition = new Vector3(minPosEnBarra, 0, Barra.transform.position.z);
        else if (porcentajeHorizonte > 100)
            indicadorHorizonte.transform.localPosition = new Vector3(maxPosEnBarra, 0, Barra.transform.position.z);
        else
        {
            float nuevaProfundidad = porcentajeHorizonte / 100 * (maxPosEnBarra - minPosEnBarra);
            indicadorHorizonte.transform.localPosition = new Vector3(minPosEnBarra + nuevaProfundidad, 0, Barra.transform.position.z);
        }

    }
}
