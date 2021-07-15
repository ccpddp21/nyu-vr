using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

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
    private List<MeshRenderer> _currentRenderers = new List<MeshRenderer>();

    private Collider[] _colliders = null;
    public bool isCollisionEnabled { get; private set; } = false;

    // Start is called before the first frame update
    void Start()
    {
        _colliders = GetComponentsInChildren<Collider>().Where(childCollider => !childCollider.isTrigger).ToArray();
        trackedAction.Enable();
        Hide();
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
        foreach (MeshRenderer renderer in _currentRenderers)
        {
            renderer.enabled = true;
        }
        isHidden = false;
        EnableCollision(enabled);
    }

    public void Hide()
    {
        _currentRenderers.Clear();
        MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer renderer in renderers)
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
}
