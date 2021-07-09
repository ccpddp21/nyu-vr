using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeySpawner : MonoBehaviour
{
    public static KeySpawner instance;

    public GameObject[] keySpawns;
    public GameObject keyPrefab;

    private GameObject key;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        keySpawns = GameObject.FindGameObjectsWithTag("Key Spawn");

        key = Instantiate(keyPrefab);

        SetKeyPosition();
    }

    public void SetKeyPosition()
    {
        int rand = Random.Range(0, keySpawns.Length);
        key.transform.SetParent(keySpawns[rand].transform.Find("Spawn Point"));
        key.transform.localPosition = new Vector3(0, 0, 0);
        key.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
    }
}
