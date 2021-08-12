using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dial : MonoBehaviour
{
    Vector3 _startRotation;

    MeshRenderer _meshRenderer = null;

    void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    public void StartTurn()
    {
        _startRotation = transform.localEulerAngles;
        _meshRenderer.material.SetColor("_Color", Color.red);
    }

    public void StopTurn()
    {
        _meshRenderer.material.SetColor("_Color", Color.white);
    }

    public void DialUpdate(float angle)
    {
        Vector3 angles = _startRotation;
        angles.y -= angle;
        transform.localEulerAngles = angles;
    }
}
