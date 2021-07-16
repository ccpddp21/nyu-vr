using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

[System.Serializable]
public class DragEvent : UnityEvent<float> {}

public class DragInteractable : XRBaseInteractable
{
    public Transform startDragPosition = null;
    public Transform endDragPosition = null;

    [HideInInspector]
    public float dragPercent = 0.0f;

    protected XRBaseInteractor _interactor = null;

    public UnityEvent onDragStart = new UnityEvent();
    public UnityEvent onDragEnd = new UnityEvent();
    public DragEvent onDragUpdate = new DragEvent();

    Coroutine _drag = null;

    void StartDrag()
    {
        if (_drag != null)
        {
            StopCoroutine(_drag);
        }

        _drag = StartCoroutine(CalculateDrag());
        onDragStart.Invoke();
    }

    void EndDrag()
    {
        if (_drag != null)
        {
            StopCoroutine(_drag);
            _drag = null;
        }
        onDragEnd.Invoke();
    }

    public static float InverseLerp(Vector3 a, Vector3 b, Vector3 value)
    {
        Vector3 AB = b - a;
        Vector3 AV = value - a;

        return Mathf.Clamp01(Vector3.Dot(AV, AB) / Vector3.Dot(AB, AB));
    }

    IEnumerator CalculateDrag()
    {
        while (_interactor != null)
        {
            // get a line in local space
            Vector3 line = startDragPosition.localPosition - endDragPosition.localPosition;

            // convert our interactor position to local space
            Vector3 interactorLocalPosition = startDragPosition.parent.InverseTransformPoint(_interactor.transform.position);

            // project the interactor position onto the line
            Vector3 projectedPoint = Vector3.Project(interactorLocalPosition, line.normalized);

            // reverse interpolate that position on the line to get a percentage of how far the drag has moved
            dragPercent = InverseLerp(startDragPosition.localPosition, endDragPosition.localPosition, projectedPoint);

            onDragUpdate?.Invoke(dragPercent);

            yield return null;
        }
    }

    protected override void OnSelectEntered(XRBaseInteractor interactor)
    {
        _interactor = interactor;
        StartDrag();
        base.OnSelectEntered(interactor);
    }

    protected override void OnSelectExited(XRBaseInteractor interactor)
    {
        EndDrag();
        _interactor = null;
        base.OnSelectExited(interactor);
    }
}
