using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PromptCatalyst : MonoBehaviour
{
    [SerializeField] private string prompt;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player.PlayerPrompt = prompt;
        }
        else
        {
            Debug.LogError("The prompt didn't display because the trigger was not tagged as a Player.");
        }
    }
}
