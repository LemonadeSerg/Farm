using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public InventoryManagment Inv;
    public Collection collection;
    public Farming farming;
    public PlayerMovement player;
    public SaveManager saveManager;
    public static SceneManager sM;
    public int Day;
    public GameObject Selector;
    public Vector3Int selection;

    public bool freezePlayer = false;
    public bool onInv = false;
    public bool paused = false;
    // Start is called before the first frame update
    void Start()
    {
       
        sM = this;
    }

    // Update is called once per frame
    void Update()
    {

        selection = farming.getTileCoord(Selector.transform.position, farming.tilemap);

        if (Input.GetKeyDown(KeyCode.O) && paused)
            unpause();
        else if(Input.GetKeyDown(KeyCode.O) && !paused)
                pause();

        if (farming != null)
            FarmUpdate();
        if (player != null)
            playerUpdate();
        if (Inv != null)
            InvUpdate();

        if (Input.GetKeyDown(KeyCode.S))
            saveManager.Save();
        if (Input.GetKeyDown(KeyCode.L))
            saveManager.Load();
    }

    void pause()
    {
        freezePlayer = true;
        paused = true;
    }

    void unpause()
    {
        if(onInv)
        paused = false;
        else
        {
            paused = false;
            freezePlayer = false;
        }
    }

    void FixedUpdate()
    {
        if (player != null)
            playerFUpdate();
    }

    void InvUpdate()
    {
        if(!paused)
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!onInv)
                Inv.open();
            else
                Inv.close();
        }
    }
    void playerUpdate()
    {
        if (freezePlayer & player.moveState != PlayerMovement.State.Pause)
            player.moveState = PlayerMovement.State.Pause;
        else if (!freezePlayer & player.moveState == PlayerMovement.State.Pause)
            player.moveState = PlayerMovement.State.Normal;
        if(!freezePlayer)
        player.InputUpdate();
    }

    void playerFUpdate()
    {
        player.PhysicsUpdate();
    }


    void FarmUpdate()
    {
        if (!onInv)
        FarmInputs();
    }
    void FarmInputs()
    {   
        if(farming != null)
        {
            if(Input.GetMouseButtonDown(0))
                Inv.currentItem.ItemLeftClick(farming.tilemap, selection);
            if (Input.GetMouseButtonDown(1))
                Inv.currentItem.ItemRightClick(farming.tilemap, selection);
        }

    }

}
