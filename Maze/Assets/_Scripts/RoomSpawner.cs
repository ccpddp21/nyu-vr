using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public static RoomSpawner instance;

    public GameObject[] roomSpawns;
    public GameObject roomPrefab;

    private GameObject room;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        roomSpawns = GameObject.FindGameObjectsWithTag("Room Spawn");
        room = Instantiate(roomPrefab);

        SetRoomPosition();
    }

    public void SetRoomPosition()
    {
        ResetSpawns();

        int rand = Random.Range(0, roomSpawns.Length);
        room.transform.SetParent(roomSpawns[rand].transform);
        room.transform.localPosition = new Vector3(0, 0, 0);
        room.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));

        roomSpawns[rand].transform.Find("Wall").gameObject.SetActive(false);
    }

    private void ResetSpawns()
    {
        for (int i = 0; i < roomSpawns.Length; i++)
        {
            roomSpawns[i].transform.Find("Wall").gameObject.SetActive(true);
        }
    }
}
