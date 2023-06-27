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
    
    //�Ƿ������������
    [SerializeField] private PhysicsMaterial2D pm00;
    [SerializeField] private PhysicsMaterial2D pm01;


    //�¶��жϲ���
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

                if (transform.position.x < collision.gameObject.transform.position.x)//�������ײ
                    rb.velocity = new Vector2(-3, rb.velocity.y);
                else if (transform.position.x > collision.gameObject.transform.position.x)//���ұ���ײ
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

        //��д��if��Ϊ���ж����˺�ά������״̬    movementʧЧʱ������ ����Ч���޸�running�ֶ� ��ǰһ֡�ٶȴ���0.1 ��ά�ֱ��ܶ���
        animator.SetFloat("running", Mathf.Abs(facedirection));


        //�ƶ�����
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
        //��Ծ���� 
        //�޷�վ��ʱ�޷���Ծ
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
        //���Ϊtrue ��ʾ�ӵ�
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

        //��������µ� ��û������ ����falling

        if (!JumpJudge() && rb.velocity.y < -0.1f)
            animator.SetBool("falling", true);


    }

    //ͨ�������ж��Ƿ�ӵ� ��ʱֻ�����޸��Ƿ��ڿ��еĲ���
    bool JumpJudge()
    {



        //�ж��Ƿ�������������


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





    //��дCrouch ͨ��Physics2D.OverlapCircle������cantStandFlag����дCrouch
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



        //�����ǰ�Ƕ��ŵĲ��ɿ����� ��ʱ�ж��Ƿ���վ��
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

    /* bool CheckStand()//���߼���Ƿ����վ��
     {


         RaycastHit2D hitleft;
         RaycastHit2D hitmiddle;
         RaycastHit2D hitright;

         hitleft = Physics2D.Raycast(transform.position + new Vector3(-0.5f, -0.5f, 0), Vector2.up, 0.7f, ground);
         hitmiddle = Physics2D.Raycast(transform.position + new Vector3(0, -0.5f, 0), Vector2.up, 0.7f, ground);
         hitright = Physics2D.Raycast(transform.position + new Vector3(0.5f, -0.5f, 0), Vector2.up, 0.7f, ground);

         Debug.DrawRay(transform.position + new Vector3(-0.5f, -0.5f, 0), new Vector2(0, 0.7f), Color.black, 10f); //���Ի���ray_left
         Debug.DrawRay(transform.position + new Vector3(0, -0.5f, 0), new Vector2(0, 0.7f), Color.black, 10f); //���Ի���ray_middle
         Debug.DrawRay(transform.position + new Vector3(0.5f, -0.5f, 0), new Vector2(0, 0.7f), Color.black, 10f); //���Ի���ray_right


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


    //ʹ��Physics2D.OverlapCircle�����Ż�
    void Crouch()
    {
        //΢�� ��ֹ���ж��� ����ǽ��
        if (animator.GetBool("jumping") || animator.GetBool("falling"))
            canStand = !Physics2D.OverlapCircle(head.position, 0.5f, ground);
        else
            canStand = !Physics2D.OverlapCircle(head.position, 0.2f, ground);
        

        //ͨ����ť�޸Ĳ�����ʵʱ��⵱ǰ״̬ ���޸���������

        //ͨ�����crouch���жϵ�ǰվ�������¶�����д��������
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


        //���尴�»�ſ��¶װ�ť���߼�

        //��⵱ǰ�Ƿ���վ�� ����ȥ����վ������  ���»�ſ�������վ����������flagΪfalse ������Ļ�һ���Ƕ��ŵ�
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
            //��¼�Ƿ�վ������
            cantStandFlag = true;
        }
        
    }

    public void Death()
    {
        Destroy(gameObject);
    }


}
