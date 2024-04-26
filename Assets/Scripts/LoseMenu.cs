using System.Collections;
using System.Collections.Generic;
using TelemetradorNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseMenu : MonoBehaviour
{
    public void ToMainMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
    private void OnApplicationQuit()
    {

        //Telemetrador.Instance().endSession(Time.time,false);
        Telemetrador.Instance().endQuit(Time.time);
    }

}
