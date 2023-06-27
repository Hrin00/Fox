using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogController : EnemyController
{

    public Transform leftPoint;
    public Transform rightPoint;
    public LayerMask ground;
    public float speed;
    public float jumpForce;
    public Transform foot;


    
    bool faceLeft = true;
    float leftX;
    float rightX;
    float speedTemp;
    Rigidbody2D rb;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        leftX = leftPoint.position.x;
        rightX = rightPoint.position.x;

/*        GameObject lefttmp =  new GameObject("lefttemp");
        lefttmp.transform.position = leftPoint.transform.position;
        GameObject righttmp = new GameObject("righttmp");
        righttmp.transform.position = rightPoint.transform.position;*/

        Destroy(leftPoint.gameObject);
        Destroy(rightPoint.gameObject);

        rb = gameObject.GetComponent<Rigidbody2D>();
        

        speedTemp = speed;
    }

    // Update is called once per frame
    void Update()
    {
        SwitchAnime();
    }

    void Movement()
    {

        if (Physics2D.OverlapCircle(foot.position,0.2f,ground))
        {

            if(transform.position.x <= leftX)
            {
                faceLeft = false;
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (transform.position.x >= rightX)
            {
                faceLeft = true;
                transform.localScale = new Vector3(1, 1, 1);
            }

            float temp;
            if (speed != speedTemp)
            {
                temp = speed;
                speed = speedTemp;
                speedTemp = temp;
            }
                

            if (faceLeft)
            {
                rb.velocity = new Vector2(-speed, jumpForce);
            }
            else
            {
                rb.velocity = new Vector2(speed, jumpForce);
            }

            speed = speedTemp;

            animator.SetBool("jumping", true);

        }
    }

    void SwitchAnime()
    {
        if (animator.GetBool("jumping"))
        {
            if(rb.velocity.y < -0.1f)
            {
                animator.SetBool("jumping", false);
                animator.SetBool("falling", true);
            }
        }
        else if (animator.GetBool("falling"))
        {
            if(Physics2D.OverlapCircle(foot.position, 0.2f, ground))
            {
                animator.SetBool("falling", false);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
    


            animator.Play("idle",0,0f);
           

            speedTemp = speed - Mathf.Abs(rb.velocity.x) +0.5f;

            rb.velocity = new Vector2(0,0);
            animator.SetBool("jumping", false);
            animator.SetBool("falling", false);



            
        }
    }
}
