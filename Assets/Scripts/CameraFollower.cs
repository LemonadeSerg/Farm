using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{

    public GameObject Player;
    public float LerpPercentage,zoomLevel;
    public Rect MoveZone;
    private Vector3 player, cam,target;
    public GameObject Selector;
    public float SelectorRange = 2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transistionCameraWithinBound();
        Secltor();
    }

    void transistionCameraWithinBound()
    {
        player = Player.transform.position;
        cam = this.transform.position;
        target.x = Mathf.Lerp(cam.x, player.x, LerpPercentage);
        target.y = Mathf.Lerp(cam.y, player.y, LerpPercentage);
        if (target.x < MoveZone.x)
            target.x = MoveZone.x;
        if (target.y < MoveZone.y)
            target.y = MoveZone.y;
        if (target.x > MoveZone.x + MoveZone.width)
            target.x = MoveZone.x + MoveZone.width;
        if (target.y > MoveZone.y + MoveZone.height)
            target.y = MoveZone.y + MoveZone.width;
        this.gameObject.transform.position = new Vector3(target.x, target.y, -10);
    }

    void Secltor()
    {
        Vector3 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 PlayerPos = Player.transform.position;
        Vector3 Direction = (MousePos - PlayerPos);
        Direction.z = 0;
        Vector3 normalized = PlayerPos + Vector3.ClampMagnitude(Direction, SelectorRange);
        Selector.transform.position = normalized;

    }
}
