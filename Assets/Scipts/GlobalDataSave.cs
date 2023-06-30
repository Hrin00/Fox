using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GlobalDataSave : MonoBehaviour
{
    public static GlobalDataSave Instance;
    //ItemScore
    public int cherryScore = 0;
    public int gemScore = 0;
    public int life = 3;


    //Roll
    public bool cherryEnough = false;
    public bool gemEnough = false;
    public int rabbitIndex = 1;
    public int snailIndex = 1;



    //InstanceItem
    public Vector2 position;
    public Dictionary<string, Vector2> itemMap;
    public GameObject Cherry;
    public GameObject Gem;
    public GameObject Gem_Diffuse;
    GameObject items;


    //Volume
    public float volume = 0;


    //Localization
    public static string dialogMainKey = null;
    public static string dialogAddKey = null;



    private void Awake()
    {
        //������ǵ�һ�� ��Instance��û����ֵ ��Ϊ�Լ� ����Ǹ��Ƴ�������Ϊ�� ɾ������Ʒ
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            itemMap = new Dictionary<string, Vector2>();

            //�������е�item
            itemMap.Add("Out_Cherry1", new Vector2(-2.5f, 0.4f));
            itemMap.Add("Out_Cherry2", new Vector2(-5.1f, 0.3f));
            itemMap.Add("Out_Cherry3", new Vector2(-3.6f, 1.3f));
            itemMap.Add("Out_Cherry4", new Vector2(-48.3f, 0.3f));
            itemMap.Add("Out_Cherry5", new Vector2(-45.9f, 0.3f));
            itemMap.Add("Out_Cherry6", new Vector2(-41.0f, -0.5f));
            itemMap.Add("Out_Gem1", new Vector2(-17.9f, 1.4f));
            itemMap.Add("Out_Gem2", new Vector2(-14.5f, -1.3f));
            itemMap.Add("House2_Gem1", new Vector2(-11.7f, -6.5f));
            itemMap.Add("House2_Gem2", new Vector2(11f, -3.3f));

            position = new Vector2(-2.5f, -1f);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InstantiateItem()
    {

        items = GameObject.Find("Item");

        foreach (KeyValuePair<string, Vector2> itemPair in itemMap)
        {

            if (itemPair.Key.Contains(SceneManager.GetActiveScene().name))
            {
                GameObject item = null;

                //Out_Cherry6特殊处理 防止和背景一起移动
                if (itemPair.Key.Contains("Cherry") && !itemPair.Key.Equals("Out_Cherry6"))
                    item = Instantiate(Cherry, itemPair.Value, Quaternion.identity, items.transform);
                else if (itemPair.Key.Contains("Cherry") && itemPair.Key.Equals("Out_Cherry6"))
                {
                    item = Instantiate(Cherry, itemPair.Value, Quaternion.identity);
                }
                else if (itemPair.Key.Contains("Gem"))
                {
                    if (SceneManager.GetActiveScene().name.Equals("Out"))
                        item = Instantiate(Gem, itemPair.Value, Quaternion.identity, items.transform);
                    else
                        item = Instantiate(Gem_Diffuse, itemPair.Value, Quaternion.identity, items.transform);
                }

                if (item != null)
                    item.name = itemPair.Key;
            }
        }
    }


    public bool DeathJudge()
    {
        GlobalDataSave.Instance.life--;
        Text text_life = GameObject.Find("Text_Life").GetComponent<Text>();
        text_life.text = GlobalDataSave.Instance.life.ToString();

        if (GlobalDataSave.Instance.life <= 0)
        {
            Animator animator = GameObject.Find("Player").GetComponent<Animator>();
            animator.SetTrigger("death");



            GameObject root = GameObject.Find("Canvas");

            GameObject gameOver = root.transform.Find("GameOver").gameObject;
            gameOver.SetActive(true);

            return true;
        }

        return false;
    }
}
