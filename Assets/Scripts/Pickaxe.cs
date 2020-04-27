using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class Pickaxe : Item
{
    public TileBase tileToBreak;
    public TileBase tileToMake;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    override public void ItemRightClick(Tilemap tilemap, Vector3Int selection)
    {
        
        if (SceneManager.sM.farming.isTileAtCoord(tileToMake, tilemap, selection))
        {
            SceneManager.sM.farming.placeTileAtCoord(tileToBreak, tilemap, selection);
        }
    }

    override public void ItemLeftClick(Tilemap tilemap, Vector3Int selection)
    {
        if (SceneManager.sM.farming.isEntityAtCoord(selection))
        {
            SceneManager.sM.farming.getEntityAtCoord(selection).Hit(this,selection);
        }
        else
        if (SceneManager.sM.farming.isTileAtCoord(tileToBreak, tilemap, selection))
        {
            SceneManager.sM.farming.placeTileAtCoord(tileToMake, tilemap, selection);
        }
    }
}
