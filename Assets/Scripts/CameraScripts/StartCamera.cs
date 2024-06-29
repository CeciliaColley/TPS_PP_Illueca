using UnityEngine;
using Cinemachine;
using System.Collections;
using Unity.VisualScripting;

public class StartCamera : CameraBehaviour
{
    [Tooltip("The index of the camera in the camera manager 'cameras' list that you want to switch to.")]
    [SerializeField] private int switchCameraIndex;
    [Tooltip("The minimum speed for the spin.")]
    [SerializeField] private float SpinSpeed = 1.0f;

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
        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.LevelLoadedEvent += OnLevelLoaded;
        }
        else
        {
            Spin();
        }
    }

    private void OnDisable()
    {
        SpinComplete -= OnSpinCompleteHandler;
    }

    private void OnLevelLoaded()
    {
        Spin();
        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.LevelLoadedEvent -= OnLevelLoaded;
        }
    }

    private void Spin()
    {
        StartCoroutine(FullSpin(SpinSpeed));
    }

    private void OnSpinCompleteHandler()
    {
        SwitchCamera(switchCameraIndex);
    }
}
