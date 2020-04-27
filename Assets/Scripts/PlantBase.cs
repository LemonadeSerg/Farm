using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class PlantBase : Entity
{
    public Sprite[] plantTextures;
    public Sprite currentTex;
    public int DaysToGrow;
    
    public int currentDay;
    public bool Grown;
    /// <summary>
    /// Will handle on plant destruction
    /// such as when grown should drop fruit
    /// </summary>
    override public void DestroyTile()
    {

    }
    
    override public void Hit(Item item,Vector3Int selection)
    {
        print("Entity ID:" + ID.ToString() + "Hit with" + item.ID.ToString());
        SceneManager.sM.farming.removeEntity(selection);
    }
    /// <summary>
    /// Called when Plant is initially planted
    /// sets all information unique to plant
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="SDay"></param>
    /// <param name="tm"></param>
    override public void PlaceTile(Vector3Int pos,int SDay,Tilemap tm,int cDay)
    {
        tilePos = pos;
        startDay = SDay;
        currentDay = cDay;
        this.transform.position = tm.GetCellCenterWorld(pos);
        updateRendererState();
    }
    /// <summary>
    /// Called everymorning
    /// </summary>
    /// <param name="farming"></param>

    override public void morningUpdate(Farming farming, int cuDay)
    {
        if (!Grown)
        {
            currentDay=cuDay;
            updateRendererState();
            
            if (currentDay - startDay >= DaysToGrow)
                Grown = true;
        }
        else
        {

        }
    }
    override public void nightUpdate(Farming farming, int cuDay)
    {
    }
        /// <summary>
        /// Process changes for growing such as updating texture based on grow percentage
        /// </summary>
    public void updateRendererState() {
        float cDay = currentDay, SDay = startDay, DTG = DaysToGrow;

        print(Mathf.Lerp(1, plantTextures.Length, ((cDay - SDay) / DTG)) - 1);
        currentTex = plantTextures[(int)Mathf.Lerp(1, plantTextures.Length, ((cDay - SDay) / DTG)) - 1];
        this.GetComponent<SpriteRenderer>().sprite = currentTex;
    }

}
