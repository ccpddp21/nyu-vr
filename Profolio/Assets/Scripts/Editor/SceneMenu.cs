using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.SceneManagement;

public static class SceneMenu
{
    [MenuItem("Scenes/Lobby")]
    static void OpenLobby()
    {
        OpenScene(SceneUtils.Name.Lobby);
    }

    [MenuItem("Scenes/Maze")]
    static void OpenMaze()
    {
        OpenScene(SceneUtils.Name.Maze);
    }

    [MenuItem("Scenes/ComplexInteractions")]
    static void OpenComplexInteractions()
    {
        OpenScene(SceneUtils.Name.ComplexInteractions);
    }

    static void OpenScene(string name)
    {
        Scene persistentScene = EditorSceneManager.OpenScene("Assets/Scenes/" + SceneUtils.Name.XRPersistent + ".unity", OpenSceneMode.Single);
        Scene currentScene = EditorSceneManager.OpenScene("Assets/Scenes/" + name + ".unity", OpenSceneMode.Additive);
        SceneUtils.AlignXRRig(persistentScene, currentScene);
    }
}
