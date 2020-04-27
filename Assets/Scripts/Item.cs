using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    // Start is called before the first frame update
    public int ID;
    public Sprite Texture;
    bool stackable;
    public int MaxStack;
    public Texture Icon;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract void ItemRightClick(Tilemap tilemap, Vector3Int selection);
    public abstract void ItemLeftClick(Tilemap tilemap, Vector3Int selection);
}
