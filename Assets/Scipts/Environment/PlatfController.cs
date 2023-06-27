using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatfController : MonoBehaviour
{
    public float climbspeed = 2f;
    public Collider2D platf;
    Animator animator;
    public bool inClimb = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GameObject.Find("Player").GetComponent<Animator>();
        platf = gameObject.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {




            
        if (collision.name.Equals("Player"))
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                inClimb = true;
                animator.SetBool("climbing",true);
                ClearAnimatorBool();


                animator.speed = 1;
                

                collision.GetComponent<Rigidbody2D>().gravityScale = 0;
                Physics2D.IgnoreCollision(collision.GetComponents<Collider2D>()[0], platf);
                Physics2D.IgnoreCollision(collision.GetComponents<Collider2D>()[1], platf);
                collision.GetComponent<Rigidbody2D>().velocity = new Vector2(0, climbspeed);
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                inClimb = true;
                animator.SetBool("climbing", true);
                ClearAnimatorBool();

                animator.speed = 1;

                collision.GetComponent<Rigidbody2D>().gravityScale = 0;
                Physics2D.IgnoreCollision(collision.GetComponents<Collider2D>()[0], platf);
                Physics2D.IgnoreCollision(collision.GetComponents<Collider2D>()[1], platf);
                collision.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -climbspeed);
            }
            else if (inClimb)
            {
                collision.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

                if(animator.GetCurrentAnimatorStateInfo(0).IsName("climb"))
                    animator.speed = 0;

            }
        }
    }




    private void OnTriggerExit2D(Collider2D collision)
    {

        animator.SetBool("climbing", false);
        collision.GetComponent<Rigidbody2D>().gravityScale = 2;
        inClimb = false;
        Physics2D.IgnoreCollision(collision.GetComponents<Collider2D>()[1], platf, false);
        Physics2D.IgnoreCollision(collision.GetComponents<Collider2D>()[0], platf, false);

        animator.speed = 1;
    }

    public void ClimbJump()
    {
        animator.SetBool("climbing", false);
        ClearAnimatorBool();
        animator.SetBool("jumping", true);
        inClimb = false;

        Physics2D.IgnoreCollision(GameObject.Find("Player").GetComponents<Collider2D>()[1], platf, false);
        Physics2D.IgnoreCollision(GameObject.Find("Player").GetComponents<Collider2D>()[0], platf, false);
        GameObject.Find("Player").GetComponent<Rigidbody2D>().gravityScale = 2;
    }

    public void ClearAnimatorBool()
    {
        animator.SetBool("jumping", false);
        animator.SetBool("falling", false);
        animator.SetBool("crouching", false);
    }
}
