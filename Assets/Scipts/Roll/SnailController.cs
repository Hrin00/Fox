using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class SnailController : RollController
{
    public GameObject gem__Diffuse;

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


        if (GlobalDataSave.Instance.snailIndex == 1)
        {
            text_Dialog_Main.text = stringTable.GetEntry("Snail1").GetLocalizedString();
            return;
        }

        if (!cherry.isEnough)
        {
            if (cherry.sum >= 6)
            {
                text_Dialog_Main.text = stringTable.GetEntry("Snail2").GetLocalizedString();

                GameObject temp = GameObject.Instantiate(gem__Diffuse, new Vector2(transform.position.x, transform.position.y + 1.5f), Quaternion.identity);
                temp.name = "Gem";
                GlobalDataSave.Instance.itemMap.Add(temp.name, temp.transform.position);

                cherry.isEnough = true;
                cherry.sum -= 6;
                InventoryController.instance.ReflashGird();
            }
            else
            {
                text_Dialog_Main.text = stringTable.GetEntry("Snail1").GetLocalizedString();
            }

        }
        else
        {
            text_Dialog_Main.text = stringTable.GetEntry("Snail3").GetLocalizedString();
        }

    }
}
