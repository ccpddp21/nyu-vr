using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGame : MonoBehaviour
{
    public GameObject xrRig;
    public Transform playerSpawn;

    public void StartNewGame()
    {
        RoomSpawner.instance.SetRoomPosition();
        KeySpawner.instance.SetKeyPosition();
        xrRig.transform.position = playerSpawn.position;
    }
}
