using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeController : MonoBehaviour
{
    public Items cherry;
    public Items gem;
    public Inventorys myBag;



    // Start is called before the first frame update
    
    public void GameStart()
    {
        GameObject dataSave = GameObject.Find("DataSave");
        if (dataSave != null)
            Destroy(dataSave);

        cherry.sum = 0;
        cherry.isEnough = false;

        gem.sum = 0;
        gem.isEnough = false;

        for(int i = 0; i < myBag.itemList.Count; i++)
        {
            myBag.itemList[i] = null;
        }


        SceneManager.LoadScene("House1");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
