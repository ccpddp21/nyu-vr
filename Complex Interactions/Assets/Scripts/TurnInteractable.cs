using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

[System.Serializable]
public class TurnEvent : UnityEvent<float> { };

public class TurnInteractable : XRBaseInteractable
{
    XRBaseInteractor _interactor = null;

    Coroutine _turn = null;

    [HideInInspector]
    public float turnAngle = 0.0f;

    Vector3 _startingRotation = Vector3.zero;

    public UnityEvent onTurnStart = new UnityEvent();
    public UnityEvent onTurnStop = new UnityEvent();
    public TurnEvent onTurnUpdate = new TurnEvent();

    Quaternion GetLocalRotation(Quaternion targetWorld)
    {
        return Quaternion.Inverse(targetWorld) * transform.rotation;
    }

    void StartTurn()
    {
        if (_turn != null)
        {
            StopCoroutine(_turn);
        }

        Quaternion localRotation = GetLocalRotation(_interactor.transform.rotation);
        _startingRotation = localRotation.eulerAngles;
        onTurnStart.Invoke();
        _turn = StartCoroutine(UpdateTurn());
    }

    void StopTurn()
    {
        if (_turn != null)
        {
            StopCoroutine(_turn);
            onTurnStop.Invoke();
            _turn = null;
        }
    }

    IEnumerator UpdateTurn()
    {
        while (_interactor != null)
        {
            Quaternion localRotation = GetLocalRotation(_interactor.transform.rotation);
            turnAngle = _startingRotation.z - localRotation.eulerAngles.z;
            onTurnUpdate.Invoke(turnAngle);
            yield return null;
        }

        
    }

    protected override void OnSelectEntered(XRBaseInteractor interactor)
    {
        _interactor = interactor;
        StartTurn();
        base.OnSelectEntered(interactor);
    }

    protected override void OnSelectExited(XRBaseInteractor interactor)
    {
        StopTurn();
        _interactor = null;
        base.OnSelectExited(interactor);
    }
}
