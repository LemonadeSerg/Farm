using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class PlantBase : MonoBehaviour
{
    public Sprite[] plantTextures;
    public Sprite currentTex;
    public int DaysToGrow;
    public Vector3Int tilePos;
    public int StartDay;
    public int currentDay;
    public bool Grown;

    /// <summary>
    /// Will handle on plant destruction
    /// such as when grown should drop fruit
    /// </summary>
    public void DestroyTile()
    {

    }
    /// <summary>
    /// Called when Plant is initially planted
    /// sets all information unique to plant
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="SDay"></param>
    /// <param name="tm"></param>
    public void PlaceTile(Vector3Int pos,int SDay,Tilemap tm)
    {
        tilePos = pos;
        StartDay = SDay;
        currentDay = SDay;
        this.transform.position = tm.GetCellCenterWorld(pos);
        currentTex = plantTextures[0];
        this.GetComponent<SpriteRenderer>().sprite = currentTex;
    }
    /// <summary>
    /// Called everymorning
    /// </summary>
    /// <param name="farming"></param>
    public void morningUpdate(Farming farming)
    {
        if (!Grown)
        {
            currentDay++;
            grow();
            this.GetComponent<SpriteRenderer>().sprite = currentTex;
            if (currentDay - StartDay >= DaysToGrow)
                Grown = true;
        }
        else
        {

        }
    }

    /// <summary>
    /// Process changes for growing such as updating texture based on grow percentage
    /// </summary>
    void grow() {
        float cDay = currentDay, SDay = StartDay, DTG = DaysToGrow;

        print(Mathf.Lerp(1, plantTextures.Length, ((cDay - SDay) / DTG)) - 1);
        currentTex = plantTextures[(int)Mathf.Lerp(1, plantTextures.Length, ((cDay - SDay) / DTG)) - 1];
    }

}
