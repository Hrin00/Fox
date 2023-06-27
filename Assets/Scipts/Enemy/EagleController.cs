using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleController : EnemyController
{

    // Start is called before the first frame update

    public Transform topPoint;
    public Transform bottomPoint;

    public float speed;

    float topY;
    float bottomY;
    bool faceUp = true;
    Rigidbody2D rb;


    protected override void Start()
    {
        base.Start();

        topY = topPoint.position.y;
        bottomY = bottomPoint.position.y;

        Destroy(topPoint.gameObject);
        Destroy(bottomPoint.gameObject);

        rb = gameObject.GetComponent<Rigidbody2D>();


    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if(transform.position.y >= topY)
        {
            faceUp = false;
        }
        else if(transform.position.y <= bottomY)
        {
            faceUp = true;
        }


        if (faceUp)
        {
            rb.velocity = new Vector2(0,speed);
        }
        else
        {
            rb.velocity = new Vector2(0, -speed);
        }
    }
}
