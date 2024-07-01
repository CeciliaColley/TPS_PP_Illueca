using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelCollider : MonoBehaviour
{
    [SerializeField] private string nextLevel = "Home";

    private void OnTriggerEnter(Collider other)
    {
        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.ChangeLevel(nextLevel);
        }
    }
}
