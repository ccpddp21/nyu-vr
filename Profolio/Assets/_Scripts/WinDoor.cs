using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WinDoor : MonoBehaviour
{
    public UnityEvent onWin = new UnityEvent();
    public GameObject canvas;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Win Key"))
        {
            onWin?.Invoke();
            gameObject.SetActive(false);
            canvas.SetActive(true);
        }
    }
}
