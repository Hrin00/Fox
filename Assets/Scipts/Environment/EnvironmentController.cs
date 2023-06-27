using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentController : MonoBehaviour
{
    GameObject tilemapPlatf;
    public Items gem;

    // Start is called before the first frame update
    void Start()
    {
        GameObject root = GameObject.Find("Grid");

        tilemapPlatf = root.transform.Find("TilemapPlatf").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (gem.isEnough)
        {
            tilemapPlatf.SetActive(true);
        }
    }
}
