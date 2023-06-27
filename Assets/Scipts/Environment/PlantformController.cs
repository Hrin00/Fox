using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantformController : MonoBehaviour
{

    public Transform upPoint;
    public Transform bottomPoint;
    public float speed;
    Rigidbody2D rb;

    
    float upY;
    float bottomY;

    bool faceUp = true;
    bool moveFlag = true;
    float curTime;
    float lastTime;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();

        upY = upPoint.position.y + 3;
        bottomY = bottomPoint.position.y + 3;
        /*Destroy(upPoint.gameObject);
        Destroy(bottomPoint.gameObject);*/

    }

    // Update is called once per frame
    void Update()
    {
        Movemrnt();
    }

    void Movemrnt()
    {


        if(transform.position.y <= bottomY)
        {
            faceUp = true;


        }else if(transform.position.y >= upY)
        {
            faceUp = false;
        }

        if (moveFlag)
        {
            if (faceUp)
            {
                rb.velocity = new Vector2(0, speed);
            }
            else
            {
                rb.velocity = new Vector2(0, -speed);
            }
        }
    }
}
