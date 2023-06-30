using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Equals("Player"))
        {
            Destroy(collision.gameObject);




            GameObject root = GameObject.Find("Canvas");

            GameObject gameOver = root.transform.Find("GameOver").gameObject;
            Text text_gameOver = gameOver.transform.Find("Text_GameOver").gameObject.GetComponent<Text>();

            text_gameOver.text = "Game Clear！";

            gameOver.SetActive(true);

        }
    }
}
