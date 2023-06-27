using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitController : RollController
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

        if (GlobalDataSave.Instance.rabbitIndex == 1)
        {
            
            text_Dialog_Main.text = "小狐狸你好呀！东边的蜗牛先生已经好几天没吃东西了，你能采6颗樱桃过去给他吃吗！";
            return;
        }

        if (cherry.isEnough)
        {
            if (gem.isEnough) 
            {
                text_Dialog_Main.text = "梯子就架在门口了，你快去试试吧！";
            }
            else
            {
                if(gem.sum >= 5)
                {
                    text_Dialog_Main.text = "哇！这5颗宝石是送给我的吗?! 太谢谢你了，这样我就能买梯子了。\n等我搭好梯子，你也来试试吧！";
                    gem.sum -= 5;
                    gem.isEnough = true;
                    InventoryController.instance.ReflashGird();
                }
                else
                {
                    text_Dialog_Main.text = "呀，你回来了啊！看起来你已经把樱桃给蜗牛先生了呢。那你随便逛逛吧，\n我还要凑齐5颗宝石买梯子。";
                }
            }
        }
        else
        {
            text_Dialog_Main.text = "(要是我能收集5颗宝石就能买梯子了......)\n\n哇，你怎么回来了！蜗牛先生还饿着肚子呢，快采6颗樱桃送过去吧！";
        }


        





    }
}
