using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuddlePromptBehaviour : MonoBehaviour
{
    [SerializeField] GameObject puddlePrompt;

    private void OnEnable()
    {
        Player.OnAcquireGun += ActivatePuddlePrompt;
    }

    private void OnDisable()
    {
        Player.OnAcquireGun -= ActivatePuddlePrompt;
    }

    private void ActivatePuddlePrompt(bool hasGun)
    {
        if (puddlePrompt != null)
        {
            puddlePrompt.SetActive(hasGun);
        }
    }
}
