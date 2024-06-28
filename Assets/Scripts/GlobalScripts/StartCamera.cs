using UnityEngine;
using Cinemachine;
using System.Collections;
using Unity.VisualScripting;

public class StartCamera : Camera
{
    [SerializeField] private int switchCameraIndex;
    [SerializeField] private float spinSpeed = 1.0f;

    private void Awake()
    {
        if (TryGetComponent(out CinemachineVirtualCamera camera))
        {
            _camera = camera;
        }
        else
        {
            Debug.LogWarning("CinemachineVirtualCamera component not found on this GameObject.");
        }
    }

    private void OnEnable()
    {
        SpinComplete += OnSpinCompleteHandler;
    }

    private void OnDisable()
    {
        SpinComplete -= OnSpinCompleteHandler;
    }

    private void Start()
    {
        StartCoroutine(FullSpin(spinSpeed));
    }

    private void OnSpinCompleteHandler()
    {
        SwitchCamera(switchCameraIndex);
    }
}
