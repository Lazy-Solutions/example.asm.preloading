using AdvancedSceneManager;
using AdvancedSceneManager.Models;
using AdvancedSceneManager.Utility;
using Lazy.Utility;
using System;
using System.Collections;
using UnityEngine;

public class Door_LevelLoader : MonoBehaviour
{

    Vector3 startPos;
    Vector3 endPos;
    public Vector3 offset;

    public Scene SceneToLoad;

    public float stayOpenFor;

    private void Start()
    {
        startPos = transform.localPosition;
        endPos = startPos - offset;
    }

    //Called from TriggerEnterEvent component
    public void OnPreloadTriggerEnter()
    {
        //Check if scene is already open, or is preloaded already,
        //if not, then we'll start Preload coroutine.
        var isOpen = SceneManager.utility.IsOpen(SceneToLoad);
        if (!isOpen && !isOpen.isPreloaded)
            Preloader.Preload(SceneToLoad).StartCoroutine();
    }

    bool isInTrigger;
    private void OnTriggerEnter(Collider other)
    {
        isInTrigger = true;
    }

    void OnTriggerExit(Collider collider)
    {
        isInTrigger = false;
    }

    void Update()
    {

        if (isInTrigger)
            if (Preloader.currentState.GetValue(SceneToLoad) == Preloader.LoadState.Waiting)
                Preloader.FinishLoad(SceneToLoad);
            else if (Preloader.currentState.GetValue(SceneToLoad) == Preloader.LoadState.Done)
                OpenDoor();
    }

    #region Open door

    //Animate door open and close

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
