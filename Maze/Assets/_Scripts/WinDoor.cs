using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WinDoor : MonoBehaviour
{
    public UnityEvent playerWon = new UnityEvent();

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Win Key"))
        {
            gameObject.SetActive(false);
            playerWon.Invoke();
        }
    }
}
