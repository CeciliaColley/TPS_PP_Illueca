using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerMonitor : MonoBehaviour
{
    [SerializeField] private string endLevelPrompt = "Jump off when you're ready to go home.";
    [SerializeField] private GameObject endLevelCollider;

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
        Player.Instance.hasWatered = true;
        Player.PlayerPrompt = endLevelPrompt;
        endLevelCollider.SetActive(true);
    }
}
