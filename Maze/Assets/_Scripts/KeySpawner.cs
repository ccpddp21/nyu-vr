using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeySpawner : MonoBehaviour
{
    public GameObject[] keySpawns;
    public GameObject keyPrefab;

    // Start is called before the first frame update
    void Start()
    {
        keySpawns = GameObject.FindGameObjectsWithTag("Key Spawn");

        int rand = Random.Range(0, keySpawns.Length);
        GameObject key = Instantiate(keyPrefab, keySpawns[rand].transform.Find("Spawn Point"));
    }
}
