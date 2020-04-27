using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public SceneManager sceneManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Save()
    {
        ES3.Save<int>("CurrentDay:", sceneManager.Day);
        SaveFarm();
    }

    public void Load()
    {
        if (ES3.FileExists())
        {
            sceneManager.Day = ES3.Load<int>("CurrentDay:");
            Clean(sceneManager.farming.Entities);
            LoadFarm();
        }
        
    }

    void SaveFarm()
    {
        saveTileMap(sceneManager.farming.tilemap, "World");
        saveEntitys("Entity",sceneManager.farming.Entities);
    }

    void LoadFarm()
    {
        
        loadTileMap(sceneManager.farming.tilemap, "World");
        loadEntitys("Entity");
    }



    void saveEntitys(string SaveKey,List<Entity> Entities)
    {
        List<int> entityIDs = new List<int>();
        List<Vector3Int> TilePos = new List<Vector3Int>();
        List<int> EntityStart = new List<int>();
        foreach (Entity entity in Entities)
        {
            entityIDs.Add(entity.ID);
            TilePos.Add(entity.tilePos);
            EntityStart.Add(entity.startDay);
        }

        ES3.Save<List<int>>(SaveKey + ":IDs", entityIDs);
        ES3.Save<List<Vector3Int>>(SaveKey + ":Pos", TilePos);
        ES3.Save<List<int>>(SaveKey + ":StrD", EntityStart);
    }

    void loadEntitys(string SaveKey)
    {
        if (ES3.KeyExists(SaveKey + ":IDs"))
        {
            List<int> entityIDs = ES3.Load<List<int>>(SaveKey + ":IDs");
            List<Vector3Int> TilePos = ES3.Load<List<Vector3Int>>(SaveKey + ":Pos");
            List<int> EntityStart = ES3.Load<List<int>>(SaveKey + ":StrD");
            for (int i = 0; i < entityIDs.Count; i++)
            {
                sceneManager.farming.createEntity(SceneManager.sM.collection.Entitys[entityIDs[i]], TilePos[i], EntityStart[i]);
            }
        }
    }
    void saveTileMap(Tilemap tm, string SaveKey)
    {
        sceneManager.farming.TileIDs = new int[sceneManager.farming.Width, sceneManager.farming.Height];
        for (int y = 0; y < sceneManager.farming.Height; y++)
        {
            for (int x = 0; x < sceneManager.farming.Width; x++)
            {
                sceneManager.farming.TileIDs[x, y] = -10;
                for (int i = 0; i < sceneManager.farming.TilesToBeSaved.Length; i++)
                {
                    if (sceneManager.farming.TilesToBeSaved[i] == tm.GetTile(new Vector3Int(sceneManager.farming.StartX + x, sceneManager.farming.StartY + y, 0)))
                    {

                        sceneManager.farming.TileIDs[x, y] = i;
                    }

                }

            }
        }
        ES3.Save<int[,]>("Map:" + SaveKey, sceneManager.farming.TileIDs);
    }

    void loadTileMap(Tilemap tm, string SaveKey)
    {
        sceneManager.farming.TileIDs = ES3.Load<int[,]>("Map:" + SaveKey);
        for (int y = 0; y < sceneManager.farming.Height; y++)
        {
            for (int x = 0; x < sceneManager.farming.Width; x++)
            {
                for (int i = 0; i < sceneManager.farming.TilesToBeSaved.Length; i++)
                {
                    if (sceneManager.farming.TileIDs[x, y] == i)
                    {

                        tm.SetTile(new Vector3Int(sceneManager.farming.StartX + x, sceneManager.farming.StartY + y, 0), sceneManager.farming.TilesToBeSaved[i]);
                    }

                }

            }
        }
    }
    void Clean(List<Entity> Entities)
    {
        foreach (Entity entity in Entities)
        {
            Destroy(entity.gameObject);
        }
        Entities.Clear();
    }
}
