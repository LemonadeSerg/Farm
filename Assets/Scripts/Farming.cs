using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

using UnityEngine;

public class Farming : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase GrassTB, DirtTB;
    public Sprite plantTB;
    public List<PlantBase> plantList;
    public PlantCollections plantBases;
    public int Day = 0;
    public GameObject Player;
    public GameObject Selector;
    public Vector3Int selction;
    // Start is called before the first frame update
    void Start()
    {
        ///Load Map Objects
        ///Load Object Data
        ///Check EventBased Changes
    }

    // Update is called once per frame
    void Update()
    {
        Inputs();
        selction = getTileCoord(Selector.transform.position,tilemap);
    }

    /**************************************************************************/
    /******************Input And Actions**************************************/
    /************************************************************************/
    /// <summary>
    /// Links inputs to functions
    /// </summary>
    /// 
    void Inputs()
    {
        if (Input.GetMouseButtonDown(0))
            PrimaryInput();
        if (Input.GetMouseButtonDown(1))
            SecondaryInput();
        if (Input.GetKeyDown(KeyCode.Alpha1))
            DebugAction();
    }
    /// <summary>
    /// Testing Actions
    /// </summary>
    void DebugAction()
    {
        MorningUpdate();
    }
    /// <summary>
    /// Handles the action for primary actions
    /// Will handle tool based changes
    /// </summary>
    void PrimaryInput()
    {

        if (isTileAtCoord(GrassTB, tilemap, selction))
        {
            placeTileAtCoord(DirtTB, tilemap, selction);
        }
        else if (isTileAtCoord(DirtTB, tilemap, selction))
        {
            placeTileAtCoord(GrassTB, tilemap, selction);
            if (isPlantAtCoord(selction))
            {
                removePlant(selction);
            }
        }

    }
    /// <summary>
    /// Handles the action for secondary actions
    /// Will handle tool based changes
    /// </summary>
    void SecondaryInput()
    {

        if (isTileAtCoord(DirtTB, tilemap, selction))
        {
            
                plantPlant(plantBases.plantBases[0], selction);
            
        }

    }

    /**************************************************************************/
    /******************Day Events*********************************************/
    /************************************************************************/
    /// <summary>
    /// Calls to be made at the start of a new day
    /// </summary>
    void MorningUpdate()
    {
        plantGrow();
    }
    /// <summary>
    /// Calls to be made at the end of a day
    /// </summary>
    void NightUpdate()
    {

    }
    /**************************************************************************/
    /******************Morning Updates****************************************/
    /************************************************************************/

    /// <summary>
    /// Calls each plant and sends it morning update
    /// </summary>
    void plantGrow()
    {
        foreach (PlantBase plant in plantList)
        {
            plant.morningUpdate(this);
        }
    }


    /**************************************************************************/
    /******************Night Updates******************************************/
    /************************************************************************/
   
    /**************************************************************************/
    /******************Plant Interactions*************************************/
    /************************************************************************/
    /// <summary>
    /// checks if there is a plant growing the cell at Vector3Int
    /// </summary>
    /// <param name="coord"></param>
    /// <returns></returns>
    bool isPlantAtCoord(Vector3Int coord)
    {
        bool found = false;
        foreach (PlantBase plant in plantList)
        {
            if (plant.tilePos == coord)
            {
                found = true;
            }
        }
        return found;
    }
    /// <summary>
    /// returns the plant growing the cell at Vector3Int
    /// </summary>
    /// <param name="coord"></param>
    /// <returns></returns>
    PlantBase getPlantAtCoord(Vector3Int coord)
    {

        foreach (PlantBase plant in plantList)
        {
            if (plant.tilePos == coord)
            {
                return plant;
            }
        }
        return null;
    }
    /// <summary>
    /// Creates new instnace of plant. sets posistion and data releative of tile posistion
    /// </summary>
    /// <param name="plant"></param>
    /// <param name="tilePos"></param>
    void plantPlant(PlantBase plant, Vector3Int tilePos)
    {
        plantList.Add(Instantiate(plant));
        plantList[plantList.Count - 1].PlaceTile(tilePos, Day,tilemap);
    }
    /// <summary>
    /// Removes the plant data based at pos(In relation of cell posistion)
    /// </summary>
    /// <param name="tm"></param>
    void removePlant(Vector3Int pos)
    {
        foreach (PlantBase plant in plantList)
        {
            if (plant.tilePos == pos)
            {
                plant.DestroyTile();
                plantList.Remove(plant);
                Destroy(plant.gameObject);
                break;
            }
        }
    }
   

    /**************************************************************************/
    /******************Tile Map Interactions**********************************/
    /************************************************************************/
    /// <summary>
    /// Removes Tile data on TileMap:tm
    /// At Vector3Int of cell
    /// </summary>
    /// <param name="tm"></param>
    /// <param name="coord"></param>
    void destroyTileAtCoord(Tilemap tm,Vector3Int coord)
    {
        tm.SetTile(coord, null);
    }
    /// <summary>
    /// Returns True if the TileBase on the TileMap:tm at Vector3Int of cell matches tb
    /// </summary>
    /// <param name="tb"></param>
    /// <param name="tm"></param>
    /// <param name="coord"></param>
    /// <returns></returns>
    bool isTileAtCoord(TileBase tb, Tilemap tm, Vector3Int coord)
    {
        return tb == getTileAtCoord(coord, tm);
    }

    /// <summary>
    /// fills Tile data tb at Vector3Int of cell belonging to tm
    /// </summary>
    /// <param name="tb"></param>
    /// <param name="tm"></param>
    /// <param name="coord"></param>
     void placeTileAtCoord(TileBase tb, Tilemap tm,Vector3Int coord)
    {
        tm.SetTile(coord, tb);
    }
    /// <summary>
    /// Returns TileBase data for cell Vector3Int on Tilemap:tm
    /// </summary>
    /// <param name="coord"></param>
    /// <param name="tm"></param>
    /// <returns></returns>
    TileBase getTileAtCoord(Vector3Int coord, Tilemap tm)
    {
        return (tm.GetTile(coord));
    }

    /**************************************************************************/
    /******************Cell To World******************************************/
    /************************************************************************/
    /// <summary>
    /// Redurns Vector3Int of Cell from Tilemap tm at the posistion of Selection Object world posisiton
    /// </summary>
    /// <param name="tm"></param>
    /// <returns></returns>
    Vector3Int getTileCoord(Vector3 Selector,Tilemap tm)
    {
        Vector3Int coordinate = tm.WorldToCell(Selector);
        return coordinate;
    }
    /// <summary>
    /// Redurns Vector3Int of Cell from Tilemap tm at the posistion of mouse
    /// </summary>
    /// <param name="tm"></param>
    /// <returns></returns>
    Vector3Int getTileCoord(Tilemap tm)
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int coordinate = tm.WorldToCell(mouseWorldPos);
        return coordinate;
    }
}