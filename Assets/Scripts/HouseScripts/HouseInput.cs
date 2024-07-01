using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HouseInput : MonoBehaviour
{
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
            case PlayerPrompts.EXITHOUSE:
                if (LevelManager.Instance != null)
                {
                    LevelManager.Instance.ChangeLevel("Home");
                }
                else Debug.LogError("No scene was loaded becuase there is no instance of a level manager.");
                break;
            default:
                break;
        }
    }
}
