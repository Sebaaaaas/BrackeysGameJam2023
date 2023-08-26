using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medusa : MonoBehaviour
{

    public float velocity = 2.0f;

    [SerializeField] float height = 5.0f;

    [SerializeField] float broad = 1.0f;

    [SerializeField] float maxVel = 5.0f;

    Transform myTranform;

    Rigidbody2D rb2d;

    Vector2 initPosition;
    Vector2 direction;


    bool up;
    // Start is called before the first frame update
    void Start()
    {
        myTranform = GetComponent<Transform>();
        rb2d = GetComponent<Rigidbody2D>();
        direction = new Vector2(0, 1);
        initPosition = myTranform.position;
        up = true;

        maxVel = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        float h = (initPosition.y + height);
        if (up && (myTranform.position.y >= h))
        {
            direction.y = -1;
            rb2d.velocity = Vector2.zero;
            up = false;
        }
        else if (!up && (myTranform.position.y < initPosition.y))
        {
            direction.y = 1;
            rb2d.velocity = Vector2.zero;
            up = true;
        }
    }

    private void FixedUpdate()
    {
        rb2d.AddForce(direction.normalized * velocity * Time.fixedDeltaTime);

        //if (rb2d.velocity.magnitude > maxVel)
        //    rb2d.velocity = rb2d.velocity.normalized * maxVel;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }
}
