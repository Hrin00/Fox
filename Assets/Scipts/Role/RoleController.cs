using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoleController : MonoBehaviour
{


    protected GameObject dialog_Main;
    protected Text text_Dialog_Main;


    // Start is called before the first frame update
    protected virtual void Start()
    {


        GameObject root = GameObject.Find("Canvas");
        dialog_Main = root.transform.Find("Dialog_Main").gameObject;
        text_Dialog_Main = dialog_Main.transform.Find("Text_Dialog_Main").gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void Speak()
    {

    }
}
