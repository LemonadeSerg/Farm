using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManagment : MonoBehaviour
{
    public Item currentItem;
    public int[,] Inventory = new int[10,3];
    public int[,] Count = new int[10, 3];

    public bool isShowing = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    

    // Update is called once per frame
    void Update()
    {
       
    }

    public void open()
    {
        SceneManager.sM.freezePlayer = true;
        SceneManager.sM.onInv = true;
        isShowing = SceneManager.sM.onInv;
    }

    public void close()
    {
        SceneManager.sM.freezePlayer = false;
        SceneManager.sM.onInv = false;
        isShowing = SceneManager.sM.onInv;
    }


    private void OnGUI()
    {
        if(isShowing)
        for(int x = 0; x < 10; x++)
        {
            for(int y = 0; y < 3; y++)
            {
                if(Inventory[x,y] != 0)
                GUI.Label(new Rect(1000 + x * 30, 100 + y * 30, 30, 30), SceneManager.sM.collection.getItemFromID(Inventory[x, y]).Icon);
            }
        }
    }
}
