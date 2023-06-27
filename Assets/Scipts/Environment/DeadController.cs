using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadController : MonoBehaviour
{
    // Start is called before the first frame update

    AudioSource audioSource;

    void Start()
    {
        audioSource = GameObject.Find("Player").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            Debug.Log("player");

            AudioSource[] audioSources = GameObject.FindObjectsOfType<AudioSource>();

            foreach (AudioSource temp in audioSources)
            {
                Debug.Log(temp.gameObject.name);
                temp.enabled = false;
            }
            

            audioSource.enabled = true;
            audioSource.clip = Resources.Load<AudioClip>("Player_Die");
            audioSource.Play();


            collision.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
            collision.GetComponent<PlayerController>().enabled = false;


            Invoke("Restart",1.5f);


        }
        else if (collision.tag.Equals("Enemy"))
        {
            Destroy(collision);
        }
    }

    void Restart()
    {
        if(!GlobalDataSave.Instance.DeathJudge())
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
