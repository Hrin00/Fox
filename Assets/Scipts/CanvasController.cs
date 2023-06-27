using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class CanvasController : MonoBehaviour
{
    public GameObject pause;
    public GameObject menu;
    public AudioMixer audioMixer;
    GameObject bag;

    // Start is called before the first frame update
    void Start()
    {
        GameObject root = GameObject.Find("Canvas");
        bag = root.transform.Find("Bag").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        BagControl();
    }

    public void Pause()
    {
        Debug.Log("Pause");
        menu.SetActive(true);
        pause.SetActive(false);

        Time.timeScale = 0f;
        GameObject.Find("Player").GetComponent<PlayerController>().enabled = false;
    }

    public void Resume()
    {
        menu.SetActive(false);
        pause.SetActive(true);

        Time.timeScale = 1f;
        GameObject.Find("Player").GetComponent<PlayerController>().enabled = true;
    }

    public void ChangeVolume(float volume)
    {
        audioMixer.SetFloat("MainVolume", volume);
    }

    public void BackToTitle()
    {
        SceneManager.LoadScene("Home");
    }

    public void BagControl()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {

            bag.SetActive(!bag.activeInHierarchy);
        }

        if (Input.GetKeyDown(KeyCode.Escape) && bag.activeInHierarchy)
        {
            bag.SetActive(false);
        }

    }
}
