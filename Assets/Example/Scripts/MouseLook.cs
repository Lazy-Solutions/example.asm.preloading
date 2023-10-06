using System.Collections;
using System.Collections.Generic;
using AdvancedSceneManager.Models;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    /*
     * Used Brackeys controller, because we are not focusing on making these. 
     * If you want to see which one, https://www.youtube.com/watch?v=_QajrabyTJc&ab_channel=Brackeys
     */
    [SerializeField] private Scene pauseScene;

    public float mouseSens = 100f;
    public Transform playerBody;
    
    float _xRotation = 0f;

    private void Update()
    {

        UpdateCursor();

        if (!Cursor.visible)
        {

            float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;

            _xRotation -= mouseY;
            _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(_xRotation, 0, 0);
            playerBody.Rotate(Vector3.up * mouseX);

        }

    }

    #region Show cursor when pause screen is open

    //Cursor does not work in build, so let's check if pause screen is open or not and toggle cursor depending on that

    private void OnApplicationFocus(bool focus)
    {
        UpdateCursor();
    }

    void UpdateCursor()
    {

        if (pauseScene.isOpen)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

    }

    #endregion

}
