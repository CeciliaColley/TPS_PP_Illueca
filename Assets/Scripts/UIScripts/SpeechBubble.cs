using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeechBubble : MonoBehaviour
{
    [SerializeField] private GameObject speechBubble;
    [SerializeField] private Text speechBubbleText;
    [SerializeField] private Image speechBubbleBG;
    [SerializeField] private float secondsToFadeIn = 1.0f;
    [SerializeField] private float secondsToDisplay = 4.0f;
    [SerializeField] private float secondsToFadeOut = 1.0f;

    private void OnEnable()
    {
        Player.OnPlayerSpeech += DisplaySpeech;
    }

    private void OnDisable()
    {
        Player.OnPlayerSpeech -= DisplaySpeech;
    }
    private void DisplaySpeech(string dialogueLine)
    {
        speechBubble.SetActive(true);
        // Start the speech bubble display coroutine with specific timing values
        StartCoroutine(ShowSpeechBubble(dialogueLine));
    }

    // Coroutine to show the speech bubble, wait, and then hide it
    IEnumerator ShowSpeechBubble(string dialogueLine)
    {
        yield return StartCoroutine(FadeSpeechBubbleToFullAlpha(dialogueLine));
        yield return new WaitForSeconds(secondsToDisplay);
        yield return StartCoroutine(FadeSpeechBubbleToZeroAlpha());
    }

    // Coroutine to fade the speech bubble to full alpha (opaque)
    public IEnumerator FadeSpeechBubbleToFullAlpha(string dialogueLine)
    {
        speechBubbleText.text = dialogueLine; // Set the text of the speech bubble
        float startTime = Time.time;
        float startAlphaText = speechBubbleText.color.a;
        float startAlphaBG = speechBubbleBG.color.a;
        Color originalColorText = speechBubbleText.color;
        Color originalColorBG = speechBubbleBG.color;

        // Fade in both text and background until fully opaque
        while (speechBubbleText.color.a < 1.0f)
        {
            float t = (Time.time - startTime) / secondsToFadeIn;
            float alphaText = Mathf.Lerp(startAlphaText, 1, t);
            float alphaBG = Mathf.Lerp(startAlphaBG, 1, t);
            speechBubbleText.color = new Color(originalColorText.r, originalColorText.g, originalColorText.b, alphaText);
            speechBubbleBG.color = new Color(originalColorBG.r, originalColorBG.g, originalColorBG.b, alphaBG);
            yield return null;
        }
    }

    // Coroutine to fade the speech bubble to zero alpha (transparent)
    public IEnumerator FadeSpeechBubbleToZeroAlpha()
    {
        float startTime = Time.time;
        float startAlphaText = speechBubbleText.color.a;
        float startAlphaBG = speechBubbleBG.color.a;
        Color originalColorText = speechBubbleText.color;
        Color originalColorBG = speechBubbleBG.color;

        // Fade out both text and background until fully transparent
        while (speechBubbleText.color.a > 0.0f)
        {
            float t = (Time.time - startTime) / secondsToFadeOut;
            float alphaText = Mathf.Lerp(startAlphaText, 0, t);
            float alphaBG = Mathf.Lerp(startAlphaBG, 0, t);
            speechBubbleText.color = new Color(originalColorText.r, originalColorText.g, originalColorText.b, alphaText);
            speechBubbleBG.color = new Color(originalColorBG.r, originalColorBG.g, originalColorBG.b, alphaBG);
            yield return null;
        }
    }
}
