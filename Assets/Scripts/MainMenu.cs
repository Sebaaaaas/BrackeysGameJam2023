using System.Collections;
using System.Collections.Generic;
using TelemetradorNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Awake()
    {
        if (Telemetrador.Init(Formatos.JSON, "prueba", Medio.Archivo))
        {
            Debug.Log("Iniciado");
            Telemetrador.Instance().startSession(Time.time, "myGame");
        }

    }
    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        
    }

    public void Quit()
    {
        Application.Quit();
    }
}
