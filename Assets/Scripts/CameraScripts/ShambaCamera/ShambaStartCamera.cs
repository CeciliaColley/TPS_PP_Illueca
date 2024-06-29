using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShambaStartCamera : CameraBehaviour
{
    private void OnEnable()
    {
        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.LevelLoadedEvent += OnLevelLoaded;
        }
        else
        {
            FocusGun();
        }
    }

    private void OnLevelLoaded()
    {
        FocusGun();
        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.LevelLoadedEvent -= OnLevelLoaded;
        }
    }

    private void FocusGun()
    {
        LookComplete += OnLookCompleteHandler;
        StartCoroutine(LookAtForSeconds());
    }

    private void OnLookCompleteHandler()
    {
        StartCoroutine(WaitThenSwitch());
        LookComplete -= OnLookCompleteHandler;
    }
}
