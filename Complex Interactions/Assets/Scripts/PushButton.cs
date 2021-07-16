using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class PushButton : MonoBehaviour
{
    public UnityEvent onPressed = new UnityEvent();
    public UnityEvent onReset = new UnityEvent();

    public UnityEvent onInteractionStart = new UnityEvent();
    public UnityEvent onInteractionEnd = new UnityEvent();

    [Min(0.01f)]
    public float depressionDepth = 0.015f;

    [Min(0.0001f)]
    public float pressThreshold = 0.001f;
    [Min(0.0001f)]
    public float resetThreshold = 0.001f;

    [Min(0.01f)]
    public float returnSpeed = 1.0f;

    private float _currentPressDepth = 0.0f;
    private float _yMax = 0.0f;
    private float _yMin = 0.0f;
    private bool _wasPressed = false;

    private List<Collider> _currentColliders = new List<Collider>();
    private XRBaseInteractor _interactor = null;

    // Start is called before the first frame update
    void Start()
    {
        _yMax = transform.localPosition.y;
    }

    void SetMinRange()
    {
        _yMin = _yMax - depressionDepth;
    }

    void SetHeight(float newHeight)
    {
        Vector3 currentPosition = transform.localPosition;
        currentPosition.y = newHeight;
        currentPosition.y = Mathf.Clamp(currentPosition.y, _yMin, _yMax);
        transform.localPosition = currentPosition;
    }

    bool IsPressed()
    {
        return transform.localPosition.y >= _yMin && transform.localPosition.y <= _yMin + pressThreshold;
    }

    bool IsReset()
    {
        return transform.localPosition.y >= _yMax - resetThreshold && transform.localPosition.y <= _yMax;
    }

    // Update is called once per frame
    void Update()
    {
        if (_interactor != null)
        {
            float newPressHeight = GetPressDepth(_interactor.transform.position);
            float deltaHeight = _currentPressDepth - newPressHeight;
            float newPressedPosition = transform.localPosition.y - deltaHeight;

            SetHeight(newPressedPosition);

            if (!_wasPressed && IsPressed())
            {
                onPressed?.Invoke();
                _wasPressed = true;
            }

            _currentPressDepth = newPressHeight;
        }
        else
        {
            if (!Mathf.Approximately(transform.localPosition.y, _yMax))
            {
                float returnHeight = Mathf.MoveTowards(transform.localPosition.y, _yMax, Time.deltaTime * returnSpeed);
                SetHeight(returnHeight);
            }
        }

        if (_wasPressed && IsReset())
        {
            onReset?.Invoke();
            _wasPressed = false;
        }
    }

    float GetPressDepth(Vector3 interactorWorldPosition)
    {
        return transform.parent.InverseTransformPoint(interactorWorldPosition).y;
    }

    void OnTriggerEnter(Collider other)
    {
        XRBaseInteractor interactor = other.GetComponentInParent<XRBaseInteractor>();
        if (interactor != null && !other.isTrigger)
        {
            _currentColliders.Add(other);
            if (_interactor == null)
            {
                _interactor = interactor;
                SetMinRange();
                _currentPressDepth = GetPressDepth(_interactor.transform.position);
                onInteractionStart?.Invoke();
            }
        }
    }

    void EndPress()
    {
        _currentColliders.Clear();
        _currentPressDepth = 0.0f;
        _interactor = null;
    }

    private void OnTriggerExit(Collider other)
    {
        if (_currentColliders.Contains(other))
        {
            _currentColliders.Remove(other);
            if (_currentColliders.Count == 0)
            {
                onInteractionEnd?.Invoke();
                EndPress();
            }
        }
    }
}
