using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyLifeBar : MonoBehaviour
{
    [SerializeField] private GameObject LifeBar;
    [SerializeField] private Image lifeBarImage;
    [SerializeField] private float duration = 0.2f;
    [SerializeField] private float fadeOutDelay = 2.0f;
    [SerializeField] private float fadeOutDuration = 1.0f;

    private Coroutine fillCoroutine;
    private Coroutine fadeOutCoroutine;
    private float lastLifeChangeTime;

    private void OnEnable()
    {
        BulletTarget.LifeChanged += OnEnemyLifeChange;
    }

    private void OnDisable()
    {
        BulletTarget.LifeChanged -= OnEnemyLifeChange;
    }

    private void Update()
    {
        // Check if it's time to start the fade-out process
        if (Time.time - lastLifeChangeTime > fadeOutDelay && LifeBar.activeSelf)
        {
            if (fadeOutCoroutine == null)
            {
                fadeOutCoroutine = StartCoroutine(FadeOutLifeBar());
            }
        }
    }

    private void OnEnemyLifeChange(float life)
    {
        Debug.Log("On enemy life changes is invoked.");
        lastLifeChangeTime = Time.time; // Update the time of the last life change
        LifeBar.SetActive(true);

        if (lifeBarImage == null)
            return;

        SetAlpha(1);

        if (fillCoroutine != null)
            StopCoroutine(fillCoroutine);

        fillCoroutine = StartCoroutine(UpdateAmmoBarFill(life / BulletTarget.maxLife));

        // Reset any ongoing fade-out since life changed
        if (fadeOutCoroutine != null)
        {
            StopCoroutine(fadeOutCoroutine);
            fadeOutCoroutine = null;
            ResetAlpha(); // Reset the alpha to be fully visible
        }
    }

    private IEnumerator UpdateAmmoBarFill(float targetFillAmount)
    {
        float elapsedTime = 0f;
        float startFillAmount = lifeBarImage.fillAmount;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            lifeBarImage.fillAmount = Mathf.Lerp(startFillAmount, targetFillAmount, elapsedTime / duration);
            yield return null;
        }

        lifeBarImage.fillAmount = targetFillAmount;
    }

    private IEnumerator FadeOutLifeBar()
    {
        float startAlpha = lifeBarImage.color.a;

        float elapsedTime = 0f;
        while (elapsedTime < fadeOutDuration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, 0, elapsedTime / fadeOutDuration);
            SetAlpha(newAlpha);
            yield return null;
        }

        SetAlpha(0);
        LifeBar.SetActive(false);
        fadeOutCoroutine = null;
    }

    private void SetAlpha(float alpha)
    {
        Color newColor = new Color(lifeBarImage.color.r, lifeBarImage.color.g, lifeBarImage.color.b, alpha);
        lifeBarImage.color = newColor;

        Image lifeBarBackground = LifeBar.GetComponent<Image>();
        if (lifeBarBackground != null)
        {
            lifeBarBackground.color = newColor;
        }
    }

    private void ResetAlpha()
    {
        SetAlpha(1.0f);
    }
}
