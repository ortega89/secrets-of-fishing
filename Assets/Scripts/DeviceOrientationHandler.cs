using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeviceOrientationHandler : MonoBehaviour
{
    public Camera renderingCamera;
    public float cameraSizeForPortrait;
    public float cameraSizeForLandscape;

    private DeviceOrientation previousOrientation;
    private bool noOrientation;

    // Start is called before the first frame update
    void Start()
    {
        DeviceOrientation currentOrientation = Input.deviceOrientation;
        noOrientation = currentOrientation == DeviceOrientation.Unknown;
    }

    // Update is called once per frame
    void Update()
    {
        if (noOrientation)
        {
            CheckScreenSize();
        }
        else
        {
            CheckDeviceOrientation();
        }
    }

    private void CheckScreenSize()
    {
        DeviceOrientation currentOrientation = Screen.width >= Screen.height
                ? DeviceOrientation.LandscapeLeft
                : DeviceOrientation.Portrait;

        CheckOrientation(currentOrientation);
    }

    private void CheckDeviceOrientation()
    {
        DeviceOrientation currentOrientation = Input.deviceOrientation;
        CheckOrientation(currentOrientation);
    }

    private void CheckOrientation(DeviceOrientation currentOrientation)
    {
        if (previousOrientation != currentOrientation)
        {
            if (currentOrientation == DeviceOrientation.Portrait || currentOrientation == DeviceOrientation.PortraitUpsideDown)
            {
                SetPortrait();
            }
            else
            {
                SetLandscape();
            }
            previousOrientation = currentOrientation;
        }
    }

    private void SetPortrait()
    {
        Debug.Log("Switching camera to portrait");
        SetOrientation(cameraSizeForPortrait);
    }

    private void SetLandscape()
    {
        Debug.Log("Switching camera to landscape");
        SetOrientation(cameraSizeForLandscape);
    }

    private void SetOrientation(float cameraSize)
    {
        renderingCamera.orthographicSize = cameraSize;
    }
}
