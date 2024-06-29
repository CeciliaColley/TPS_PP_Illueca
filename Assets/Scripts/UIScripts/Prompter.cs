using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Prompter : MonoBehaviour
{
    [SerializeField] private Text promptText;
    [SerializeField] private float secondsToFadeIn = 1.0f;
    [SerializeField] private float secondsToDisplay = 4.0f;
    [SerializeField] private float secondsToFadeOut = 1.0f;

    private Coroutine promptCoroutine;

    private void Start()
    {
        if (promptText != null)
        {
            promptText.color = new Color(promptText.color.r, promptText.color.g, promptText.color.b, 0);
        }
    }

    private void OnEnable()
    {
        Player.OnPromptPlayer += DisplayPrompt;
    }

    private void OnDisable()
    {
        Player.OnPromptPlayer -= DisplayPrompt;
    }

    void DisplayPrompt(string playerPrompt)
    {
        if (promptCoroutine != null)
        {
            StopCoroutine(promptCoroutine);
        }
        promptCoroutine = StartCoroutine(ShowPrompt(playerPrompt));
    }

    IEnumerator ShowPrompt(string prompt)
    {
        yield return StartCoroutine(FadeTextToFullAlpha(secondsToFadeIn, prompt));
        yield return new WaitForSeconds(secondsToDisplay);
        yield return StartCoroutine(FadeTextToZeroAlpha(secondsToFadeOut));
    }

    public IEnumerator FadeTextToFullAlpha(float time, string prompt)
    {
        promptText.color = new Color(promptText.color.r, promptText.color.g, promptText.color.b, 0);
        promptText.text = prompt;
        while (promptText.color.a < 1.0f)
        {
            promptText.color = new Color(promptText.color.r, promptText.color.g, promptText.color.b, promptText.color.a + (Time.deltaTime / time));
            yield return null;
        }
    }

    public IEnumerator FadeTextToZeroAlpha(float time)
    {
        promptText.color = new Color(promptText.color.r, promptText.color.g, promptText.color.b, 1);
        while (promptText.color.a > 0.0f)
        {
            promptText.color = new Color(promptText.color.r, promptText.color.g, promptText.color.b, promptText.color.a - (Time.deltaTime / time));
            yield return null;
        }
    }
}
