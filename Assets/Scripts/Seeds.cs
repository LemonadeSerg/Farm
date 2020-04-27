using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class Seeds : Item
{
    // Start is called before the first frame update
    public TileBase requiredTile;
    public PlantBase plant;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    override public void ItemRightClick(Tilemap tilemap,Vector3Int selection)
    {
        if (!SceneManager.sM.farming.isEntityAtCoord(selection,plant.CoveringTiles))
        {
            if(SceneManager.sM.farming.getTileAtCoord(selection,tilemap) == requiredTile)
            SceneManager.sM.farming.createEntity(plant, selection, SceneManager.sM.Day);
        }
    }

    override public void ItemLeftClick(Tilemap tilemap, Vector3Int selection)
    {

    }
}
