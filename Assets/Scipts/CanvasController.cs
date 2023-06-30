using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    public GameObject pause;
    public GameObject menu;
    public AudioMixer audioMixer;
    GameObject bag;

    //Localization
    public LocalizationSettings localSettings;
    public Locale zh;
    public Locale jp;
    public Locale en;

    //Text
    GameObject dialog_Sign;
    Text text_Dialog_Sign;

    GameObject dialog_Main;
    Text text_Dialog_Main;
    Text text_Dialog_Main_Sign;

    // Start is called before the first frame update
    void Start()
    {
        GameObject root = GameObject.Find("Canvas");
        bag = root.transform.Find("Bag").gameObject;

        dialog_Sign = root.transform.Find("Dialog_Sign").gameObject;
        text_Dialog_Sign = dialog_Sign.transform.Find("Text_Dialog_Sign").gameObject.GetComponent<Text>();

        dialog_Main = root.transform.Find("Dialog_Main").gameObject;
        text_Dialog_Main = dialog_Main.transform.Find("Text_Dialog_Main").gameObject.GetComponent<Text>();
        text_Dialog_Main_Sign = dialog_Main.transform.Find("Text_Dialog_Main_Sign").gameObject.GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        BagControl();
    }

    public void Pause()
    {
        //设定语言得方法
        //LocalizationSettings.SelectedLocale = zh;
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

    public void RefreshDialogText()
    {
        var stringTable = LocalizationSettings.StringDatabase.GetTable("LocalizationStringTable");
        if (GlobalDataSave.dialogAddKey != null)
            text_Dialog_Sign.text = stringTable.GetEntry(GlobalDataSave.dialogAddKey).GetLocalizedString();
        if (GlobalDataSave.dialogMainKey != null)
            text_Dialog_Main.text = stringTable.GetEntry(GlobalDataSave.dialogMainKey).GetLocalizedString();
        text_Dialog_Main_Sign.text = stringTable.GetEntry("textDialogMainSign").GetLocalizedString();

    }


    public void CN()
    {
        LocalizationSettings.SelectedLocale = zh;
        RefreshDialogText();
        Resume();
    }
    public void JP()
    {
        LocalizationSettings.SelectedLocale = jp;
        RefreshDialogText();
        Resume();
    }
    public void EN()
    {
        LocalizationSettings.SelectedLocale = en;
        RefreshDialogText();
        Resume();
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
