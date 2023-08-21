using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravedadAire : MonoBehaviour
{
    public float gravedadAire = 3.0f;
    public float gravedadAgua = 0.1f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>())
        {
            collision.GetComponent<Rigidbody2D>().gravityScale = gravedadAire;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Rigidbody2D>() != null)
            collision.GetComponent<Rigidbody2D>().gravityScale = gravedadAgua;
    }

}
