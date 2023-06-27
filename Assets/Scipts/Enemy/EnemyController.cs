using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    protected Animator animator;
    private AudioSource audioSource;
    

    protected virtual void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        audioSource = GameObject.Find("Enemy").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void JumpOn()
    {
        animator.SetTrigger("death");
        gameObject.GetComponent<Collider2D>().enabled = false;

        //方法1 Resource中加载clip 
        //audioSource.clip = Resources.Load<AudioClip>("Enemy_Die");

        //方法2 直接把clip拖进去 只有一个die clip
        audioSource.Play();


    }



    void Death()
    {
        Destroy(gameObject);
    }
}
