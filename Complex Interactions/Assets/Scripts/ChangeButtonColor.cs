using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeButtonColor : MonoBehaviour
{
    MeshRenderer _meshRenderer = null;

    void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    public void ChangeColor()
    {
        _meshRenderer.material.SetColor("_Color", Random.ColorHSV(0, 1, 0.9f, 1, 0.9f, 1.0f));
    }

    public void LogInteractionStarted()
    {
        Debug.Log("Interaction Started");
    }

    public void LogInteractionEnded()
    {
        Debug.Log("Interaction Ended");
    }
}
