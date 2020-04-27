using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

using UnityEngine;

public class Farming : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase[] TilesToBeSaved;
    public List<Entity> Entities;

    public int StartX, StartY, Height, Width;

    public int[,] TileIDs;


    
    /**************************************************************************/
    /******************Saving and Loading*************************************/
    /************************************************************************/

  
    



    void MorningUpdate()
    {
        foreach (Entity entity in Entities)
        {
            entity.morningUpdate(this, SceneManager.sM.Day);
        }
    }

    void NightUpdate()
    {
        foreach (Entity entity in Entities)
        {
            entity.nightUpdate(this, SceneManager.sM.Day);
        }
    }

    public void createEntity(Entity entity, Vector3Int tilePos,int day)
    {
        Entities.Add(Instantiate(entity));
        Entities[Entities.Count - 1].PlaceTile(tilePos, day, tilemap, SceneManager.sM.Day);

    }

    public void placeTileAtCoord(TileBase tb, Tilemap tm, Vector3Int coord)
    {
        tm.SetTile(coord, tb);
        print(coord.ToString());
    }

    public void removeEntity(Vector3Int pos)
    {
        foreach (Entity entity in Entities)
        {
            if (entity.tilePos == pos)
            {
                entity.DestroyTile();
                Entities.Remove(entity);
                Destroy(entity.gameObject);
                break;
            }
        }
    }

    public void destroyTileAtCoord(Tilemap tm,Vector3Int coord)
    {
        tm.SetTile(coord, null);
    }


    public bool isEntityAtCoord(Vector3Int coord)
    {
        foreach (Entity entity in Entities)
        {
            if (entity.tilePos == coord)
            {
                return true;
            }
            foreach(Vector3Int tile in entity.CoveringTiles)
            {
                if (coord == entity.tilePos + tile)
                    return true;
            }
        }
        return false;
    }
    public bool isEntityAtCoord(Vector3Int coord,List<Vector3Int> entityCovers)
    {
        foreach (Entity entity in Entities)
        {
             if (entity.tilePos == coord)
                {
                    return true;
                }
                foreach (Vector3Int tile in entity.CoveringTiles)
                {
                    if (coord == entity.tilePos + tile)
                        return true;
                }
            foreach (Vector3Int coverSpot in entityCovers)
            {
                if (entity.tilePos == coord + coverSpot)
                {
                    return true;
                }
                foreach (Vector3Int tile in entity.CoveringTiles)
                {
                    if (coord + coverSpot == entity.tilePos + tile)
                        return true;
                }
            }
        }
        return false;
    }

    public bool isTileAtCoord(TileBase tb, Tilemap tm, Vector3Int coord)
    {
        return tb == getTileAtCoord(coord, tm);
    }

    public TileBase getTileAtCoord(Vector3Int coord, Tilemap tm)
    {
        return (tm.GetTile(coord));
    }
    public Entity getEntityAtCoord(Vector3Int coord)
    {

        foreach (Entity entity in Entities)
        {
            if (entity.tilePos == coord)
            {
                return entity;
            }
            foreach (Vector3Int tile in entity.CoveringTiles)
            {
                if (coord == entity.tilePos + tile)
                    return entity;
            }
        }
        return null;
    }
    public Vector3Int getTileCoord(Vector3 Selector,Tilemap tm)
    {
        Vector3Int coordinate = tm.WorldToCell(Selector);
        return coordinate;
    }

    public Vector3Int getTileCoord(Tilemap tm)
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int coordinate = tm.WorldToCell(mouseWorldPos);
        return coordinate;
    }
}