using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    // Start is called before the first frame update

    protected Animator animator;
    private AudioSource audioSource;


    protected virtual void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        audioSource = GameObject.Find("Item").GetComponent<AudioSource>();


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnJump()
    {
        animator.SetTrigger("eat");
        audioSource.Play();
    }
    public void Eat()
    {
        GlobalDataSave.Instance.itemMap.Remove(gameObject.name);
        Destroy(gameObject);
    }

}
