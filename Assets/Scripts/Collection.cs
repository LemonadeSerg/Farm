using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collection : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Entity> Entitys;
    public List<Item> Items;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Item getItemFromID(int ID)
    {
        foreach(Item i in Items)
        {
            if (i.ID == ID)
                return i;
        }
        return null;
    }
}
