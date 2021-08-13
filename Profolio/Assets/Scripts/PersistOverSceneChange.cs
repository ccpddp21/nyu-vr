using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistOverSceneChange : MonoBehaviour
{
    public bool applyToChildren = true;

    int persistentLayer = 0;
    int currentLayer = 0;

    void Awake()
    {
        persistentLayer = LayerMask.NameToLayer("XR Persistent");
        currentLayer = gameObject.layer;
    }

    private void OnEnable()
    {
        SceneLoader.Instance.onLoadStart.AddListener(StartPersist);
        SceneLoader.Instance.onLoadFinished.AddListener(EndPersist);
    }

    private void OnDisable()
    {
        var loader = SceneLoader.Instance;
        if (loader != null)
        {
            loader.onLoadStart.RemoveListener(StartPersist);
            loader.onLoadFinished.RemoveListener(EndPersist);
        }
    }

    void StartPersist()
    {
        currentLayer = gameObject.layer;
        SetLayer(gameObject, persistentLayer, applyToChildren);
    }

    void EndPersist()
    {
        SetLayer(gameObject, currentLayer, applyToChildren);
    }

    void SetLayer(GameObject obj, int newLayer, bool applyToChildren)
    {
        obj.layer = newLayer;

        if (applyToChildren)
        {
            foreach (Transform child in obj.transform)
            {
                SetLayer(child.gameObject, newLayer, applyToChildren);
            }
        }
    }
}
