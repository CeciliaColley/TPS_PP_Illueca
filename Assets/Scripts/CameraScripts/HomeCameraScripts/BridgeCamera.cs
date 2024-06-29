using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class BridgeCamera : CameraBehaviour
{
    [SerializeField] private string dialogueLine = "This rickety bridge looks dangerous...";

    public void InspectBridgeSequence()
    {
        Player.PlayerSpeech = dialogueLine;
        StartCoroutine(WaitThenSwitch());
    }
}
