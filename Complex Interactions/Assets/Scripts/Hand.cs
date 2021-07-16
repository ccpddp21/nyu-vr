using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public enum HandType
{
    Left,
    Right
}

public class Hand : MonoBehaviour
{
    public HandType type = HandType.Left;
    public bool isHidden { get; private set; } = false;
    public InputAction trackedAction = null;

    private bool _isCurrentlyTracked = false;
    private List<Renderer> _currentRenderers = new List<Renderer>();

    private Collider[] _colliders = null;
    public bool isCollisionEnabled { get; private set; } = false;

    public XRBaseInteractor interactor = null;

    void Awake()
    {
        if (interactor == null)
            interactor = GetComponentInParent<XRBaseInteractor>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _colliders = GetComponentsInChildren<Collider>().Where(childCollider => !childCollider.isTrigger).ToArray();
        trackedAction.Enable();
        Hide();
    }

    void OnEnable()
    {
        interactor.onSelectEntered.AddListener(OnGrab);
        interactor.onSelectExited.AddListener(OnRelease);
    }

    void OnDisable()
    {
        interactor.onSelectEntered.RemoveListener(OnGrab);
        interactor.onSelectExited.RemoveListener(OnRelease);
    }

    // Update is called once per frame
    void Update()
    {
        float isTracked = trackedAction.ReadValue<float>();
        if (isTracked == 1.0f && !_isCurrentlyTracked)
        {
            _isCurrentlyTracked = true;
            Show();
        }
        else if (isTracked == 0 && _isCurrentlyTracked)
        {
            _isCurrentlyTracked = false;
            Hide();
        }
    }

    public void Show()
    {
        foreach (Renderer renderer in _currentRenderers)
        {
            renderer.enabled = true;
        }
        isHidden = false;
        EnableCollision(false);
    }

    public void Hide()
    {
        _currentRenderers.Clear();
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            renderer.enabled = false;
            _currentRenderers.Add(renderer);
        }
        isHidden = true;
        EnableCollision(false);
    }

    public void EnableCollision(bool enabled)
    {
        if (isCollisionEnabled == enabled)
            return;

        isCollisionEnabled = enabled;

        foreach (Collider collider in _colliders)
        {
            collider.enabled = isCollisionEnabled;
        }
    }

    void OnGrab(XRBaseInteractable grabbedObject)
    {
        HandControl ctrl = grabbedObject.GetComponent<HandControl>();
        if (ctrl != null)
        {
            if (ctrl.hideHand)
            {
                Hide();
            }
        }
    }

    void OnRelease(XRBaseInteractable grabbedObject)
    {
        HandControl ctrl = grabbedObject.GetComponent<HandControl>();
        if (ctrl != null)
        {
            if (ctrl.hideHand)
            {
                Show();
            }
        }
    }
}
