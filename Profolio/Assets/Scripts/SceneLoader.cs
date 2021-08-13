using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class SceneLoader : Singleton<SceneLoader>
{
    public Material screenFade = null;
    [Min(0.001f)]
    public float speed = 1.0f;
    [Range(0.0f, 5.0f)]
    public float addedWaitTime = 2.0f;
    public UnityEvent onLoadStart = new UnityEvent();
    public UnityEvent onLoadFinished = new UnityEvent();

    private bool isLoading = false;
    private float fadeAmount = 0.0f;
    private Coroutine fadeCoroutine = null;
    static readonly int fadeAmoutPropID = Shader.PropertyToID("_FadeAmount");

    private Scene persistentScene;

    private void Awake()
    {
        SceneManager.sceneLoaded += SetActiveScene;

        persistentScene = SceneManager.GetActiveScene();

        if (!Application.isEditor)
        {
            SceneManager.LoadSceneAsync(SceneUtils.Names.Lobby, LoadSceneMode.Additive);
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= SetActiveScene;
    }

    public void LoadScene(string name)
    {
        if (!isLoading)
        {
            StartCoroutine(Load(name));
        }
    }

    private void SetActiveScene(Scene scene, LoadSceneMode mode)
    {
        SceneManager.SetActiveScene(scene);
        SceneUtils.AlignXRRig(persistentScene, scene);
    }

    IEnumerator Load(string name)
    {
        isLoading = true;
        onLoadStart?.Invoke();
        yield return FadeOut();
        yield return StartCoroutine(UnloadCurrentScene());

        yield return new WaitForSeconds(addedWaitTime);

        yield return StartCoroutine(LoadNewScene(name));
        yield return FadeIn();
        onLoadFinished?.Invoke();
        isLoading = false;
    }

    IEnumerator UnloadCurrentScene()
    {
        AsyncOperation unload = SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        while (!unload.isDone)
        {
            yield return null;
        }
    }

    IEnumerator LoadNewScene(string name)
    {
        AsyncOperation unload = SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
        while (!unload.isDone)
        {
            yield return null;
        }
    }

    IEnumerator FadeOut()
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }
        fadeCoroutine = StartCoroutine(Fade(1.0f));
        yield return fadeCoroutine;
    }

    IEnumerator FadeIn()
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }
        fadeCoroutine = StartCoroutine(Fade(0.0f));
        yield return fadeCoroutine;
    }

    IEnumerator Fade(float target)
    {
        while (!Mathf.Approximately(fadeAmount, target))
        {
            fadeAmount = Mathf.MoveTowards(fadeAmount, target, speed * Time.deltaTime);
            screenFade.SetFloat(fadeAmoutPropID, fadeAmount);
            yield return null;
        }

        screenFade.SetFloat(fadeAmoutPropID, target);
    }
}
