using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShambaStartCamera : CameraBehaviour
{
    private void Start()
    {
        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.LevelLoadedEvent += OnLevelLoaded;
        }
        else
        {
            SwitchCamera();
        }
    }

    private void OnLevelLoaded()
    {
        SwitchCamera();
    }
}
