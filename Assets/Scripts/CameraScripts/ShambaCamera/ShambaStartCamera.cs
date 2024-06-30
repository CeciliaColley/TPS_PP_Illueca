using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShambaStartCamera : CameraBehaviour
{
    private void Start()
    {
        SwitchCamera();
    }

    private void OnLevelLoaded()
    {
        SwitchCamera();
    }
}
