using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public static InventoryController instance;

    public Inventorys myBag;
    public GameObject emptySlot;

    GameObject grid;

    public List<GameObject> slotList = new List<GameObject>();


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(instance);


        grid = GameObject.Find("Canvas").transform.Find("Bag/BagGrid").gameObject;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        ReflashGird();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReflashGird()
    {
        for(int i = 0 ; i < grid.transform.childCount; i++)
        {
            Destroy(grid.transform.GetChild(i).gameObject);
            slotList.Clear();
        }

        for(int i = 0; i < myBag.itemList.Count; i++)
        {

            if (myBag.itemList[i] != null && myBag.itemList[i].sum == 0)
            {
                myBag.itemList[i] = null;
            }
         
            slotList.Add(GameObject.Instantiate(emptySlot));
            slotList[i].transform.SetParent(grid.transform);
            slotList[i].GetComponent<SlotController>().slotIndex = i;
            slotList[i].GetComponent<SlotController>().SetSlot(myBag.itemList[i]);
            

        }


    }

    

}
