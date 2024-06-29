using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraBehaviour : MonoBehaviour
{
    [Tooltip("The seconds b")]
    [SerializeField] private float spinStartDelay = 1.0f;
    [Tooltip("The seconds between the spin ending, and the invocation of the 'OnSpinEnd' events.")]
    [SerializeField] private float spinEndDelay = 2.0f;
    protected CinemachineVirtualCamera _camera;
    protected Action SpinComplete;


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

    protected void SwitchCamera(int switchCameraIndex)
    {
        if (CameraManager.Instance != null)
        {
            CameraManager.Instance.SwitchCamera(switchCameraIndex);
        }
    }
}





