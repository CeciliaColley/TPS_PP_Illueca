using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerMonitor : MonoBehaviour
{
    [SerializeField] private string HomeLevelName = "Home";

    private bool handlingNoMoreTargets = false;

    private void Update()
    {
        CheckForBulletTargets();
    }

    private void CheckForBulletTargets()
    {
        BulletTarget[] targets = GetComponentsInChildren<BulletTarget>();

        if (targets.Length == 0)
        {
            OnNoMoreTargets();
        }
    }

    private void OnNoMoreTargets()
    {
        if (Player.Instance != null)
        {
            Player.Instance.hasWatered = true;
        }
        if ( !handlingNoMoreTargets &&  LevelManager.Instance != null)
        {
            handlingNoMoreTargets = true;
            LevelManager.Instance.ChangeLevel(HomeLevelName);
        }
    }
}
