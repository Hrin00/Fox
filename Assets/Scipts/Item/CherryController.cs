using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CherryController : ItemController
{
    public Items thisItem;
    public Inventorys myBag;




    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            gameObject.GetComponent<Collider2D>().enabled = false;


            if (!myBag.itemList.Contains(thisItem))
            {
                for(int i = 0; i < myBag.itemList.Count; i++)
                {
                    if(myBag.itemList[i] == null)
                    {
                        thisItem.sum++;
                        myBag.itemList[i] = thisItem;
                        break;
                    }
                        
                }
            }
            else
            {
                thisItem.sum++;
            }

            InventoryController.instance.ReflashGird();


            OnJump();

        }
    }
}
