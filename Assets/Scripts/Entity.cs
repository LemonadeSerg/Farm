using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public Vector3Int tilePos;
    public int ID;
    public TileBase mainTile;
    public int startDay;
    public List<Vector3Int> CoveringTiles;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract void Hit(Item item,Vector3Int selection);
    public abstract void morningUpdate(Farming farming, int cuDay);
    public abstract void nightUpdate(Farming farming, int cuDay);

    public abstract void DestroyTile();

    public abstract void PlaceTile(Vector3Int pos, int SDay, Tilemap tm, int cDay);
}
