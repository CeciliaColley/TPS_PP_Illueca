using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class HomeInput : MonoBehaviour
{
    [SerializeField] private string gardenLevelName = "Garden";
    [SerializeField] private string houseLevelName = "House";
    [SerializeField] private int bridgeCameraIndex = 2;
    
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
    }

    private void OnFollowPrompt(InputAction.CallbackContext context)
    {
        switch (Player.lastPrompt)
        {
            case PlayerPrompts.DEFAULT:
                break;
            case PlayerPrompts.GOTOGARDEN:
                if (LevelManager.Instance != null)
                {
                    LevelManager.Instance.ChangeLevel(gardenLevelName);
                }
                else Debug.LogError("No scene was loaded becuase there is no instance of a level manager.");
                
                break;
            case PlayerPrompts.GOTOHOUSE:
                if (LevelManager.Instance != null)
                {
                    LevelManager.Instance.ChangeLevel(houseLevelName);
                }
                else Debug.LogError("No scene was loaded becuase there is no instance of a level manager.");
                break;
            case PlayerPrompts.INSPECTBRIDGE:
                if (CameraManager.Instance != null)
                {
                    CameraManager.Instance.SwitchCamera(bridgeCameraIndex);
                }
                else Debug.LogError("The inspect bridge sequence didn't happen because there is no instance of camera manager.");
                break;
            default:
                break;
        }
    }
}
