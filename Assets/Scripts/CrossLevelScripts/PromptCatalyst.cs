using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PromptCatalyst : MonoBehaviour
{
    [SerializeField] private string promptText;
    [SerializeField] private PlayerPrompts prompt;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player.PlayerPrompt = promptText;
            Player.lastPrompt = prompt;
        }
        else
        {
            Debug.LogError("The prompt didn't display because the trigger was not tagged as a Player.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player.lastPrompt = PlayerPrompts.DEFAULT;
        }
        else
        {
            Debug.LogError("The prompt wasn't reset because the trigger was not tagged as a Player.");
        }
    }
}
