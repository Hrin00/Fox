using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class RabbitController : RoleController
{
    public Items cherry;
    public Items gem;


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public override void Speak()
    {
        base.Speak();
        var stringTable = LocalizationSettings.StringDatabase.GetTable("LocalizationStringTable");

        if (GlobalDataSave.Instance.rabbitIndex == 1)
        {

            text_Dialog_Main.text = stringTable.GetEntry("Rabbit1").GetLocalizedString();
            GlobalDataSave.dialogMainKey = "Rabbit1";
            return;
        }

        if (cherry.isEnough)
        {
            if (gem.isEnough)
            {
                text_Dialog_Main.text = stringTable.GetEntry("Rabbit5").GetLocalizedString();
                GlobalDataSave.dialogMainKey = "Rabbit5";
            }
            else
            {
                if (gem.sum >= 5)
                {
                    text_Dialog_Main.text = stringTable.GetEntry("Rabbit4").GetLocalizedString();
                    GlobalDataSave.dialogMainKey = "Rabbit4";
                    gem.sum -= 5;
                    gem.isEnough = true;
                    InventoryController.instance.ReflashGird();
                }
                else
                {
                    text_Dialog_Main.text = stringTable.GetEntry("Rabbit3").GetLocalizedString();
                    GlobalDataSave.dialogMainKey = "Rabbit3";
                }
            }
        }
        else
        {
            text_Dialog_Main.text = stringTable.GetEntry("Rabbit2").GetLocalizedString();
            GlobalDataSave.dialogMainKey = "Rabbit2";
        }








    }
}
