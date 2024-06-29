using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class BridgeCamera : MonoBehaviour
{
    [SerializeField] private float activeTime = 4.0f;
    [SerializeField] private int nextCameraIndex = 1;
    [SerializeField] private string dialogueLine = "This rickety bridge looks dangerous...";

    public void InspectBridgeSequence()
    {
        StartCoroutine(WaitThenSwitch());
    }

    private IEnumerator WaitThenSwitch()
    {
        Player.PlayerSpeech = dialogueLine;
        yield return new WaitForSeconds(activeTime);
        if (CameraManager.Instance != null)
        {
            CameraManager.Instance.SwitchCamera(nextCameraIndex);
        }
        else
        {
            Debug.LogError("The bridge camera didn't switch to the next camera because there is no instance of camera manager.");
        }
    }
}
