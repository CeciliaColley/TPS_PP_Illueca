using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class GardenInput : MonoBehaviour
{
    [SerializeField] Gun gun;

    public static GameObject pickUp;
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
                if (pickUp != null)
                {
                    Destroy(pickUp);
                }
                break;
            case PlayerPrompts.PUDDLERELOAD:
                gun.Refill();
                break;
            case PlayerPrompts.PICKUPHEALTH:
                if (pickUp != null)
                {
                    Destroy(pickUp);
                };
                break;
            default:
                break;
        }
    }
}
