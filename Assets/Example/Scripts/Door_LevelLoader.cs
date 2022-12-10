using System;
using System.Collections;
using AdvancedSceneManager.Core.Actions;
using AdvancedSceneManager.Models;
using Lazy.Utility;
using UnityEngine;

public class Door_LevelLoader : MonoBehaviour
{
    public Scene SceneToLoad;

    private bool waiting = false;

    //Called from TriggerEnterEvent component found on Doors
    public void OnPreloadTriggerEnter()
    {
        //Check if scene is already open, or is preloaded already,
        //if not, then we'll start Preload coroutine.
        if (SceneToLoad && SceneToLoad.state == SceneState.NotOpen)
        {
            Preloader.Preload(SceneToLoad).StartCoroutine();
            waiting = true;
        }
    }

    bool isInTrigger;
    private void OnTriggerEnter(Collider other) => isInTrigger = true;
    void OnTriggerExit(Collider collider) => isInTrigger = false;

    void Update()
    {
        // In this loop we make sure that the scene is preloaded and ready to be activated before opening the door.
        if (waiting && isInTrigger)
        {
            // The LoadStates are states you make yourself and is not handled by ASM
            // Here we check if its Loaded and ready to be enabled.
            // this is double checked with preloader, so we dont spam finishload.
            if (Preloader.CurrentScene.state == SceneState.Preloaded)
            {
                Preloader.FinishLoad();
            }
            else if (Preloader.CurrentScene.state == SceneState.Open)
            {
                // The scene is open, we can go ahead and open the door.
                OpenDoor();
                waiting = false;
            }
        }
    }

    #region Open door

    /* Animate door open and close
     * This is not part of the "Guide", you are free to do however you want to create the mystery of seamless loading.
     * In this example we just move(Open) a door.
     * */


    Vector3 startPos;
    Vector3 endPos;
    public Vector3 offset;

    public float stayOpenFor;

    private void Start()
    {
        startPos = transform.localPosition;
        endPos = startPos - offset;
    }

    bool isAnimating;
    void OpenDoor()
    {

        if (isAnimating)
            return;
        isAnimating = true;

        TweenOpenDoor(startPos, endPos, () =>
            countdown(stayOpenFor, () =>
                TweenOpenDoor(endPos, startPos, () => isAnimating = false).StartCoroutine()).StartCoroutine()
            ).StartCoroutine();

    }

    IEnumerator TweenOpenDoor(Vector3 startPos, Vector3 endPos, Action done = null)
    {
        var t = 0f;

        while (t < 1 / 1)
        {

            if (!this)
                yield break;

            transform.localPosition = Vector3.Lerp(startPos, endPos, t);
            t += Time.deltaTime;
            yield return null;

        }
        transform.localPosition = endPos;
        done?.Invoke();
    }

    IEnumerator countdown(float time, Action done)
    {
        yield return new WaitForSeconds(time);
        done.Invoke();
    }

    #endregion

}
