using AdvancedSceneManager;
using AdvancedSceneManager.Models;
using AdvancedSceneManager.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Preloader : MonoBehaviour
{

    public enum LoadState
    {
        None, Preloading, Waiting, QueueFinishLoad, FinishingLoad, Done
    }

    public static Dictionary<Scene, LoadState> currentState = new Dictionary<Scene, LoadState>();

    public static IEnumerator Preload(Scene scene)
    {

        currentState.Set(scene, LoadState.Preloading);
        var async = SceneManager.standalone.Preload(scene);
        yield return async;

        if (currentState.GetValue(scene) != LoadState.QueueFinishLoad)
        {
            currentState.Set(scene, LoadState.Waiting);
            yield return new WaitUntil(() => currentState.GetValue(scene) == LoadState.QueueFinishLoad);
        }

        currentState.Set(scene, LoadState.FinishingLoad);
        yield return async.value.FinishLoading();
        currentState.Set(scene, LoadState.Done);

    }

    public static void FinishLoad(Scene scene)
    {
        currentState.Set(scene, LoadState.QueueFinishLoad);
    }

}
