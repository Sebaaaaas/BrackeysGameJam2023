using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarraProfundidad : MonoBehaviour
{
    [SerializeField] GameObject Barra;
    GameObject indicadorProfundidad;

    public float minProf, maxProf, minPosEnBarra, maxPosEnBarra;
    private void Start()
    {
        indicadorProfundidad = Barra.transform.Find("IndicadorProfundidad").gameObject;
    }
    private void Update()
    {

        float porcentajeProfundidadReal = (100 * transform.position.y) / (maxProf - minProf);
        Debug.Log(porcentajeProfundidadReal + "%");
        if(porcentajeProfundidadReal < 0)
            indicadorProfundidad.transform.localPosition = new Vector3(0, 0, Barra.transform.position.z);
        else if(porcentajeProfundidadReal > 100)
            indicadorProfundidad.transform.localPosition = new Vector3(0, 100, Barra.transform.position.z);
        else
        {
            float nuevaProfundidad = porcentajeProfundidadReal/100 * (maxPosEnBarra-minPosEnBarra);
            indicadorProfundidad.transform.localPosition = new Vector3(0, nuevaProfundidad, Barra.transform.position.z);
        }

    }
}
