using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GardenInput : MonoBehaviour
{
    [SerializeField] Gun gun;

    private FollowPromptInput actionAsset;

    private void Awake()
    {
        actionAsset = new FollowPromptInput();
    }

    void OnEnable()
    {
        actionAsset.Enable();
        actionAsset.Home.FollowPrompt.performed += OnFollowPrompt;
    }

    void OnDisable()
    {
        actionAsset.Disable();
        actionAsset.Home.FollowPrompt.performed -= OnFollowPrompt;
        Player.HasGun = false;
    }

    private void OnFollowPrompt(InputAction.CallbackContext context)
    {
        switch (Player.lastPrompt)
        {
            case PlayerPrompts.PICKUPGUN:
                Player.HasGun = true;
                break;
            case PlayerPrompts.PUDDLERELOAD:
                gun.Refill();
                break;
            default:
                break;
        }
    }
}
