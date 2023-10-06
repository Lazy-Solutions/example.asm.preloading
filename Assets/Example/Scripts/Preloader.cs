using System;
using System.Collections;
using AdvancedSceneManager.Models;
using AdvancedSceneManager.Models.Enums;
using UnityEngine;

public class Preloader : MonoBehaviour
{
    /**
     * For simplistic example this class is monobehaviour, to be easier found on component,
     * There's many ways to handle preload, and this could for example be a static aswell.
     */
    public static bool IsReady = false;
    private static bool activate = false;

    public static IEnumerator Preload(Scene scene)
    {
        Debug.Log(scene.name);
        activate = false;

        // This is the code that starts the preloading, we then wait for it to be created.
        // once it's created we continue.
        yield return scene.Preload();

        // A double check if the scene is ready and preloaded.
        yield return new WaitUntil(() =>
        {
            Debug.Log(scene.state);
            return scene.state == SceneState.Preloaded;
        });

        IsReady = true;
        // We wait for player to walk into the Activate zone.
        // In out case it's the "Door_LevelLoader" that triggers the event
        yield return new WaitUntil(() => activate == true);

        // Unity made it so we have to yield. Might be a case where it's not ready next frame, etc.
        yield return scene.FinishPreload();

        IsReady = false;

    }

    // Activate the preloaded scene,
    // If the current scene is not null set to true.
    public static void FinishLoad() => activate = true;

}
