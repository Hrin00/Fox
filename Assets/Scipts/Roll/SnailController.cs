using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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

        if (GlobalDataSave.Instance.snailIndex == 1)
        {
            text_Dialog_Main.text = "好饿！我肚子好饿！有谁能给我6颗樱桃聛E";
            return;
        }

        if (!cherry.isEnough)
        {
            if (cherry.sum >= 6)
            {
                text_Dialog_Main.text = "諄E颗樱桃是给我的吗？太谢谢你了！\n\n（...咀嚼...咀嚼...）\n\n我吃饱了，这个作为回礼，莵E障掳桑�";

                GameObject temp = GameObject.Instantiate(gem__Diffuse, new Vector2(transform.position.x, transform.position.y + 1.5f), Quaternion.identity);
                temp.name = "Gem";
                GlobalDataSave.Instance.itemMap.Add(temp.name, temp.transform.position);

                cherry.isEnough = true;
                cherry.sum -= 6;
                InventoryController.instance.ReflashGird();
            }
            else
            {
                text_Dialog_Main.text = "好饿！我肚子好饿！有谁能给我6颗樱桃聛E";
            }

        }
        else
        {
            text_Dialog_Main.text = "太谢谢你了，说起来西边的小兔子好像打算集苼E颗宝石买梯子，能莵E惆丒丒�吗？";
        }

    }
}
