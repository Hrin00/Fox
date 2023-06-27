using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpforce;
    public LayerMask ground;

    Rigidbody2D rb;
    AudioSource audioSource;
    Animator animator;
    Collider2D circleColl;
    Collider2D capColl;
    Text text_life;
    bool isHurt;
    
    //是否跳起物理材质
    [SerializeField] private PhysicsMaterial2D pm00;
    [SerializeField] private PhysicsMaterial2D pm01;


    //下蹲判断参数
    public Transform head;
    bool cantStandFlag = false;
    bool canStand;

    //Volume
    Slider volume;



    // Start is called before the first frame update
    void Start()
    {

        transform.position = GlobalDataSave.Instance.position;
        

        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        audioSource = gameObject.GetComponent<AudioSource>();


        circleColl = gameObject.GetComponent<CircleCollider2D>();
        capColl = gameObject.GetComponent<CapsuleCollider2D>();


        text_life = GameObject.Find("Text_Life").GetComponent<Text>();

        GlobalDataSave.Instance.InstantiateItem();


        text_life.text = GlobalDataSave.Instance.life.ToString();

        canStand = !Physics2D.OverlapCircle(head.position, 0.2f, ground);

        volume = GameObject.Find("Canvas").transform.Find("Menu/Slider_Volume").GetComponent<Slider>();
        volume.value = GlobalDataSave.Instance.volume;
    }
    private void FixedUpdate()
    {
        if (!isHurt)
            Movement();
        else
            HurtAnime();
    }
    // Update is called once per frame
    void Update()
    {
        if (!isHurt)
            Jump();
        else
            HurtAnime();

        JumpAnime();

        if (!animator.GetBool("climbing"))
            Crouch();


        JudgeIsSky();





    }

    

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
     
        

        if (collision.gameObject.tag.Equals("Items") )
        {
            collision.gameObject.GetComponent<Collider2D>().enabled = false;

            

            ItemController item = collision.gameObject.GetComponent<ItemController>();
            item.OnJump();

            if (collision.name.Contains("Cherry"))
            {

                GlobalDataSave.Instance.cherryScore++;
                text_cherry.text = GlobalDataSave.Instance.cherryScore.ToString();
            }
            else if (collision.name.Contains("Gem"))
            {

                GlobalDataSave.Instance.gemScore++;
                text_gem.text = GlobalDataSave.Instance.gemScore.ToString();
            }
        }
    }*/

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Enemy") && !isHurt)
        {
            
            if (animator.GetBool("falling"))
            {
                EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
                enemy.JumpOn();

                rb.velocity = new Vector2(rb.velocity.x, jumpforce);
                animator.SetBool("jumping", true);

            }
            else
            {

                if (transform.position.x < collision.gameObject.transform.position.x)//在左边碰撞
                    rb.velocity = new Vector2(-3, rb.velocity.y);
                else if (transform.position.x > collision.gameObject.transform.position.x)//在右边碰撞
                    rb.velocity = new Vector2(3, rb.velocity.y);

                isHurt = true;

                audioSource.clip = Resources.Load<AudioClip>("Hurt");
                audioSource.Play();


                GlobalDataSave.Instance.DeathJudge();




            }
   
        }
    }


    void Movement()    
    {
        float horizontalmove = Input.GetAxis("Horizontal");
        float facedirection = Input.GetAxisRaw("Horizontal");

        //不写在if内为了判断受伤后不维持碰跑状态    movement失效时不按键 则生效后不修改running字段 若前一帧速度大于0.1 则维持奔跑动画
        animator.SetFloat("running", Mathf.Abs(facedirection));


        //移动控制
        if (horizontalmove != 0)
        {
            rb.velocity = new Vector2(horizontalmove * speed * Time.fixedDeltaTime, rb.velocity.y);
            /*animator.SetFloat("running", Mathf.Abs(facedirection));*/
        }
        if (facedirection != 0)
        {
            transform.localScale = new Vector3(facedirection, 1, 1);
        }

        
    }

    void Jump()
    {
        //跳跃控制 
        //无法站立时无法跳跃
        if (Input.GetButtonDown("Jump") & ((JumpJudge() && canStand) || animator.GetBool("climbing")) )
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpforce);
            animator.SetBool("jumping", true);
            audioSource.clip = Resources.Load<AudioClip>("Jump");
            audioSource.Play();
            if (animator.GetBool("climbing"))
            {
                GameObject.Find("TilemapPlatf").GetComponent<PlatfController>().ClimbJump();
                animator.speed = 1;
            }
        }
    }


    void JumpAnime()
    {
        if (animator.GetBool("jumping"))
        {
            if(rb.velocity.y <= 0)
            {
                animator.SetBool("jumping",false);
                animator.SetBool("falling", true);
            }
        }
        else if (animator.GetBool("falling"))
        {
            if (JumpJudge())
            {
                animator.SetBool("falling", false);

            }
        }
    }

    void HurtAnime()
    {
        if (isHurt)
        {
            animator.SetBool("hurting", true);

            if (Mathf.Abs(rb.velocity.x) < 0.1f)
            {
                isHurt = false;
                animator.SetBool("hurting", false);

            }

        }
    }




    void JudgeIsSky()
    {
        //如果为true 表示接地
        if (JumpJudge())
        {
            circleColl.sharedMaterial = pm01;
            capColl.sharedMaterial = pm01;
        }
        else
        {
            circleColl.sharedMaterial = pm00;
            capColl.sharedMaterial = pm00;

        }

        //如果在往下掉 且没碰到地 就是falling

        if (!JumpJudge() && rb.velocity.y < -0.1f)
            animator.SetBool("falling", true);


    }

    //通过射线判断是否接地 暂时只用于修改是否在空中的材质
    bool JumpJudge()
    {



        //判断是否无上条但下落


        RaycastHit2D capiHitLeft;
        RaycastHit2D capiHitMiddle;
        RaycastHit2D capiHitRight;



        capiHitMiddle = Physics2D.Raycast(transform.position + new Vector3(0, -0.9f, 0), Vector2.down, 0.15f, ground);
        capiHitLeft = Physics2D.Raycast(transform.position + new Vector3(-0.3f, -0.9f, 0), Vector2.down, 0.15f, ground);
        capiHitRight = Physics2D.Raycast(transform.position + new Vector3(0.3f, -0.9f, 0), Vector2.down, 0.15f, ground);

        




        if ((capiHitLeft.collider != null && capiHitLeft.collider.name.Contains("Tilemap")) ||
            (capiHitMiddle.collider != null && capiHitMiddle.collider.name.Contains("Tilemap")) || (capiHitRight.collider != null && capiHitRight.collider.name.Contains("Tilemap")))
            return true;
        else
            return false;
    }





    //改写Crouch 通过Physics2D.OverlapCircle方法与cantStandFlag来改写Crouch
    /*void Crouch()
    {
        if (animator.GetBool("crouching"))
        {
            circleColl.enabled = true;
            capColl.enabled = false;
        }
        else
        {
            circleColl.enabled = false;
            capColl.enabled = true;
        }



        //如果当前是蹲着的并松开按键 随时判断是否能站立
        if (animator.GetBool("crouching"))
        {

            if (CheckStand())
            {
                StopCrouch();
            }
        }


        if (Input.GetKey(KeyCode.DownArrow))
        {
            animator.SetBool("crouching", true);
            speed = 5;
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            if (CheckStand() && !animator.GetBool("hurting"))
                StopCrouch();
        }


    }*/

    /* bool CheckStand()//射线检测是否可以站立
     {


         RaycastHit2D hitleft;
         RaycastHit2D hitmiddle;
         RaycastHit2D hitright;

         hitleft = Physics2D.Raycast(transform.position + new Vector3(-0.5f, -0.5f, 0), Vector2.up, 0.7f, ground);
         hitmiddle = Physics2D.Raycast(transform.position + new Vector3(0, -0.5f, 0), Vector2.up, 0.7f, ground);
         hitright = Physics2D.Raycast(transform.position + new Vector3(0.5f, -0.5f, 0), Vector2.up, 0.7f, ground);

         Debug.DrawRay(transform.position + new Vector3(-0.5f, -0.5f, 0), new Vector2(0, 0.7f), Color.black, 10f); //测试绘制ray_left
         Debug.DrawRay(transform.position + new Vector3(0, -0.5f, 0), new Vector2(0, 0.7f), Color.black, 10f); //测试绘制ray_middle
         Debug.DrawRay(transform.position + new Vector3(0.5f, -0.5f, 0), new Vector2(0, 0.7f), Color.black, 10f); //测试绘制ray_right


         if (hitleft.collider == null && hitmiddle.collider == null && hitright.collider == null)
         {
             return true;

         }
         else
         {
             return false;

         }
     }*/

    /* void StopCrouch()
     {
         animator.SetBool("crouching", false);
         speed = 10;
     }*/


    //使用Physics2D.OverlapCircle方法优化
    void Crouch()
    {
        //微调 防止空中蹲起 卡入墙中
        if (animator.GetBool("jumping") || animator.GetBool("falling"))
            canStand = !Physics2D.OverlapCircle(head.position, 0.5f, ground);
        else
            canStand = !Physics2D.OverlapCircle(head.position, 0.2f, ground);
        

        //通过按钮修改参数来实时检测当前状态 并修改人物属性

        //通过检测crouch来判断当前站立还是下蹲来改写人物属性
        if (animator.GetBool("crouching"))
        {
            circleColl.enabled = true;
            capColl.enabled = false;
            speed = 200;
        }
        else
        {
            circleColl.enabled = false;
            capColl.enabled = true;
            speed = 400;
        }


        //具体按下或放开下蹲按钮的逻辑

        //检测当前是否能站立 并过去存在站立受阻  按下或放开能正常站立都会重置flag为false 有受阻的话一定是蹲着的
        if (canStand && /*animator.GetBool("crouching") &&*/ cantStandFlag )
        {
            animator.SetBool("crouching", false);
            cantStandFlag = false;
        }


        if (Input.GetButtonDown("Crouch"))
        {
            animator.SetBool("crouching", true);  
            cantStandFlag = false;
        }
        else if (Input.GetButtonUp("Crouch") && canStand)
        {
            animator.SetBool("crouching", false);          
            cantStandFlag = false;
        } 
        else if (Input.GetButtonUp("Crouch") && !canStand)
        {
            //记录是否站立受阻
            cantStandFlag = true;
        }
        
    }

    public void Death()
    {
        Destroy(gameObject);
    }


}
