using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogController : MonoBehaviour
{
    // Start is called before the first frame update

    GameObject dialog_Sign;
    Text text_Dialog_Sign;

    GameObject dialog_Main;
    Text text_Dialog_Main;


    private bool intoMainDialog = false;

    //TriggerJudge
    private bool isTriggerStay = false;
    private Collider2D triggerStayCollider = null;

    RollController roll = null;

    private PlayerController playerController;

    Slider volume;
    
    



    void Start()
    {
        GameObject root = GameObject.Find("Canvas");

        dialog_Sign = root.transform.Find("Dialog_Sign").gameObject;
        text_Dialog_Sign = dialog_Sign.transform.Find("Text_Dialog_Sign").gameObject.GetComponent<Text>();

        dialog_Main = root.transform.Find("Dialog_Main").gameObject;
        text_Dialog_Main = dialog_Main.transform.Find("Text_Dialog_Main").gameObject.GetComponent<Text>();

        playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        volume = GameObject.Find("Canvas").transform.Find("Menu/Slider_Volume").GetComponent<Slider>();

    }

    // Update is called once per frame
    void Update()
    {
        JudgeExitMainDialog();
        JudgeTriggerStay2D();



    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        text_Dialog_Sign.text = "按下E";

        if (gameObject.name.Contains("house"))
            text_Dialog_Sign.text += "进入";
        else if(gameObject.name.Contains("sign"))
            text_Dialog_Sign.text += "查看";
        else if (gameObject.name.Contains("Out"))
            text_Dialog_Sign.text += "离开";
        else if (gameObject.tag.Equals("Roll"))
            text_Dialog_Sign.text += "对话";






        if (collision.name.Equals("Player"))
        {
            
            dialog_Sign.SetActive(true);
        }

        isTriggerStay = true;
        triggerStayCollider = collision;
    }

    private void JudgeTriggerStay2D()
    {

        if (Input.GetKeyDown(KeyCode.E) && isTriggerStay && triggerStayCollider.name.Equals("Player"))
        {



            //人物点开对话框 变为静止站立 （未测试各种情况 例如受伤中对话）
            triggerStayCollider.GetComponent<Rigidbody2D>().velocity = new Vector2(0f,0f);
            Animator animator = triggerStayCollider.GetComponent<Animator>();
            animator.Play("idle", 0, 0f);
            animator.SetBool("jumping",false);
            animator.SetBool("falling",false);
            animator.SetFloat("running", 0f);
            animator.SetBool("crouching",false);








            if (gameObject.name.Equals("house1"))
            {
                GlobalDataSave.Instance.position = new Vector2(-2.5f, -1f);
                GlobalDataSave.Instance.volume = volume.value;
                SceneManager.LoadScene("House1");
            }
            else if (gameObject.name.Equals("house2"))
            {
                GlobalDataSave.Instance.position = new Vector2(-12.4f, 4);
                GlobalDataSave.Instance.volume = volume.value;
                SceneManager.LoadScene("House2");

            }
            else if (gameObject.name.Equals("Out"))
            {
                if (SceneManager.GetActiveScene().name.Equals("House1"))
                    GlobalDataSave.Instance.position = new Vector2(-54f, -2f);
                else if(SceneManager.GetActiveScene().name.Equals("House2"))
                    GlobalDataSave.Instance.position = new Vector2(15f, 1f);

                GlobalDataSave.Instance.volume = volume.value;
                SceneManager.LoadScene("Out");
            }
            else if (gameObject.name.Equals("sign"))
            {
                Debug.Log(intoMainDialog);
                if (!intoMainDialog)
                {
                    text_Dialog_Main.text = "小心!前方有怪物出没!\n\n 左右键控制移动,下键蹲下,空格键跳跃。";
                    dialog_Main.SetActive(true);
                    dialog_Sign.SetActive(false);
                    playerController.enabled = false;
                    intoMainDialog = true;

                }

            }
            else if (gameObject.tag.Equals("Roll"))
            {
                

                Debug.Log(gameObject.name);

                if (gameObject.name.Equals("Rabbit"))
                    roll = gameObject.GetComponent<RabbitController>();
                else if (gameObject.name.Equals("Snail"))
                    roll = gameObject.GetComponent<SnailController>();

                roll.Speak();

                dialog_Main.SetActive(true);
                dialog_Sign.SetActive(false);
                playerController.enabled = false;
                intoMainDialog = true;
            }
        }
            
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name.Equals("Player"))
        {
            dialog_Sign.SetActive(false);
        }

        isTriggerStay = false;
        triggerStayCollider = null;
    }


    void JudgeExitMainDialog()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && intoMainDialog)
        {
            if(roll != null)
            {
                if (roll.gameObject.name.Equals("Rabbit"))
                    GlobalDataSave.Instance.rabbitIndex++;
                else if(roll.gameObject.name.Equals("Snail"))
                    GlobalDataSave.Instance.snailIndex++;
            }

            dialog_Main.SetActive(false);
            dialog_Sign.SetActive(true);
            playerController.enabled = true;
            intoMainDialog = false;


        }
    }


}
