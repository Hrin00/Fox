using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Transform myCamera;
    public float moveRate;
    private float startX;

    private Transform middle3;

    // Start is called before the first frame update
    void Start()
    {
        startX = transform.position.x;


        middle3 = GameObject.Find("middle (3)").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(startX + moveRate * myCamera.position.x , transform.position.y);

        if (myCamera.position.x > -9)
            middle3.position = new Vector2(-17.4f, middle3.position.y);


    }
}
