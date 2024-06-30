using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShambaGunCamera : CameraBehaviour
{
    public void PerformSwitchCamera()
    {
        Debug.Log("GunCamera");
        SwitchCamera();
    }
}
