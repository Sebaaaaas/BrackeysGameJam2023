using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiburon : MonoBehaviour
{
    public float detectionRange;
    bool readyingCharge = false;
    Rigidbody2D rb2d;

    public float chargeRestTime;
    float timeToCharge;
    public float chargeSpeed;

    GameObject Jugador;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        Jugador = GameManager.instance.getJugador();
        timeToCharge = chargeRestTime;
    }

    private void Update()
    {
        if ((Jugador.transform.position - transform.position).magnitude < detectionRange)
        {
            readyingCharge = true;
        }
        else
            rb2d.velocity = Vector3.zero;

        rotateTowards(Jugador.transform.position);

        if (readyingCharge)
        {
            timeToCharge -= Time.deltaTime;

            if(timeToCharge <= 0)
            {
                rb2d.velocity = Vector3.zero;
                timeToCharge = chargeRestTime;
                readyingCharge = false;
                rb2d.AddForce(chargeSpeed * -transform.right);
            }
        }
    }

    protected void rotateTowards(Vector3 to)
    {
        Vector3 target = to;
        Vector3 direction = new Vector3(transform.position.x - target.x, transform.position.y - target.y);
        transform.right = direction;

        if (transform.rotation.eulerAngles.z < 90 || transform.rotation.eulerAngles.z > 240)
            GetComponent<SpriteRenderer>().flipY = false;
        else
            GetComponent<SpriteRenderer>().flipY = true;

    }

}
