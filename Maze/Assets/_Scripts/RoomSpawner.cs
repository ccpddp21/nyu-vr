using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public GameObject[] roomSpawns;
    public GameObject roomPrefab;

    // Start is called before the first frame update
    void Start()
    {
        roomSpawns = GameObject.FindGameObjectsWithTag("Room Spawn");

        int rand = Random.Range(0, roomSpawns.Length);
        GameObject key = Instantiate(roomPrefab, roomSpawns[rand].transform);

        roomSpawns[rand].transform.Find("Wall").gameObject.SetActive(false);
    }
}
