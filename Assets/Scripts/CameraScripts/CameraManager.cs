using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class CameraManager : MonoBehaviour
{
    public List<CinemachineVirtualCamera> cameras;
    public int startCameraIndex;
    public static CinemachineVirtualCamera activeCamera;
    public static CameraManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        InitializeCameras();
    }

    private void InitializeCameras()
    {
        if (cameras != null && cameras.Count > 0)
        {
            activeCamera = cameras[startCameraIndex];
            activeCamera.Priority = 10;
        }
    }

    public void SwitchCamera(int switchCameraIndex)
    {
        if (switchCameraIndex >= 0 && switchCameraIndex < cameras.Count)
        {
            activeCamera.Priority = 0;
            cameras[switchCameraIndex].Priority = 10;
            activeCamera = cameras[switchCameraIndex];
        }
    }
}
