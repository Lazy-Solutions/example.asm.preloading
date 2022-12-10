using System.Collections;
using AdvancedSceneManager;
using AdvancedSceneManager.Models;
using UnityEngine;

public class Preloader : MonoBehaviour
{
    /**
     * For simplistic example this class is monobehaviour, to be easier found on component,
     * There's many ways to handle preload, and this could for example be a static aswell.
     */

    public static Scene CurrentScene { get; private set; }
    private static bool activated = false;

    public static IEnumerator Preload(Scene scene)
    {
        CurrentScene = scene;

        // This is the code that starts the preloading, we then wait for it to be created.
        // once it's created we continue.
        var preload = SceneManager.standalone.Preload(scene);
        yield return preload;

        // A double check if the scene is ready and preloaded.
        yield return new WaitUntil(() => scene.state == SceneState.Preloaded);
        // We wait for player to walk into the Activate zone.
        // In out case it's the "Door_LevelLoader" that triggers the event
        yield return new WaitUntil(() => activated == true);

        // Unity made it so we have to yield. Might be a case where it's not ready next frame, etc.
        yield return preload.value.FinishLoading();

        // Resets the values for next preload
        activated = false;
        CurrentScene = null;
    }

    // Activate the preloaded scene,
    // If the current scene is not null set to true.
    public static void FinishLoad() => 
        activated = CurrentScene != null;

}
