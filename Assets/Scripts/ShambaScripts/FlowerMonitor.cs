using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerMonitor : MonoBehaviour
{
    [SerializeField] private string HomeLevelName = "Home";

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
        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.ChangeLevel(HomeLevelName);
        }
    }
}
