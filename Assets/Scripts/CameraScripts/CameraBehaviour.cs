using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraBehaviour : MonoBehaviour
{
    [Header("Variables for a camera spin")]
    [Tooltip("The seconds befor the sping begins")]
    [SerializeField] private float spinStartDelay = 1.0f;
    [Tooltip("The seconds between the spin ending, and the invocation of the 'OnSpinEnd' events.")]
    [SerializeField] private float spinEndDelay = 2.0f;
    protected Action SpinComplete;

    [Header("Variables for a wait then switch camera")]
    [Tooltip("The second the camera should wait before switching")]
    [SerializeField] private float activeTime = 4.0f;
    [Tooltip("The index of the camera to switch to in the camera manager camera's list.")]
    [SerializeField] protected int nextCameraIndex = 1;

    [Header("Variables for a look at for seconds")]
    [Tooltip("The game object to look at")]
    [SerializeField] private GameObject newLookAt;
    [Tooltip("The amount of time to look at the game object")]
    [SerializeField] private float lookTime;
    [Tooltip("The amount of time to look at the game object")]
    [SerializeField] private float lookDelay;
    protected Action LookComplete;

    protected Camera _mainCam;
    protected Cinemachine.CinemachineBrain cineBrain;

    protected CinemachineVirtualCamera _camera;
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
        _mainCam = Camera.main;
        if (_mainCam != null)
        {
            cineBrain = _mainCam.GetComponent<Cinemachine.CinemachineBrain>();
        }
    }

    protected IEnumerator LookAtForSeconds()
    {
        yield return new WaitForSeconds(lookDelay);
        Transform originaLookAt = _camera.LookAt;
        _camera.LookAt = newLookAt.transform;
        yield return new WaitForSeconds(lookTime);
        _camera.LookAt = originaLookAt;
        OnLookComplete();
    }
    private void OnLookComplete()
    {
        LookComplete?.Invoke();
    }

    protected IEnumerator WaitThenSwitch()
    {
        yield return new WaitForSeconds(activeTime);
        if (CameraManager.Instance != null)
        {
            CameraManager.Instance.SwitchCamera(nextCameraIndex);
        }
        else
        {
            Debug.LogError("The camera didn't switch to the next camera because there is no instance of camera manager.");
        }
    }

    protected IEnumerator FullSpin(float speed)
    {
        yield return new WaitForSeconds(spinStartDelay);
        // Get orbital transposer
        var orbitalTransposer = _camera.GetCinemachineComponent<CinemachineOrbitalTransposer>();
        if (orbitalTransposer == null)
        {
            Debug.LogWarning("CinemachineOrbitalTransposer component not found on the virtual camera.");
            yield break;
        }
        // Disable Input
        float originalSpeed = orbitalTransposer.m_XAxis.m_MaxSpeed;
        orbitalTransposer.m_XAxis.m_MaxSpeed = 0;
        // Start at a value of 0.
        orbitalTransposer.m_XAxis.Value = 0;
        // Gradually increase m_XAxis.Value until it reaches 180
        while (orbitalTransposer.m_XAxis.Value >= 0)
        {
            orbitalTransposer.m_XAxis.Value += speed * Time.deltaTime;
            yield return null;
        }

        // Set m_XAxis.Value to -180
        orbitalTransposer.m_XAxis.Value = -180f;
        yield return null;

        // Gradually decrease m_XAxis.Value until it reaches 0
        while (orbitalTransposer.m_XAxis.Value < 0f)
        {
            orbitalTransposer.m_XAxis.Value += speed * Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(spinEndDelay);
        // Ensure m_XAxis.Value is exactly 0
        orbitalTransposer.m_XAxis.Value = 0f;
        // Invoke events that are calles when the spin is over
        OnSpinComplete();
        //Enable input
        orbitalTransposer.m_XAxis.m_MaxSpeed = originalSpeed;
    }

    private void OnSpinComplete()
    {
        SpinComplete?.Invoke();
    }

    protected void SwitchCamera()
    {
        StartCoroutine(SwitchCameraCoroutine());
    }

    private IEnumerator SwitchCameraCoroutine()
    {
        yield return new WaitForSeconds(activeTime);
        if (CameraManager.Instance != null)
        {
            CameraManager.Instance.SwitchCamera(nextCameraIndex);
        }
    }
}





