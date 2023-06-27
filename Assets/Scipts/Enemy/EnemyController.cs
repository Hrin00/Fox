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

        //����1 Resource�м���clip 
        //audioSource.clip = Resources.Load<AudioClip>("Enemy_Die");

        //����2 ֱ�Ӱ�clip�Ͻ�ȥ ֻ��һ��die clip
        audioSource.Play();


    }



    void Death()
    {
        Destroy(gameObject);
    }
}
