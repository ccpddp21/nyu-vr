using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformPosition : MonoBehaviour
{
    public GameObject blueCube;
    public GameObject yellowCube;

    public Vector3 bluePos;
    public Vector3 yellowPos;

    public float speed = 1f;

    // Update is called once per frame
    void Update()
    {
        ComputePositions();

        transform.position = Vector3.Lerp(bluePos, yellowPos, Mathf.PingPong(Time.time * speed, 1.0f));
    }

    private void ComputePositions()
    {
        bluePos = blueCube.transform.TransformPoint(Vector3.up * 3);
        yellowPos = yellowCube.transform.TransformPoint(Vector3.down * 3);
    }
}
