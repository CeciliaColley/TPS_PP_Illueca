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
        lastLifeChangeTime = Time.time;

        if (lifeBarImage == null || LifeBar == null)
            return;

        LifeBar.SetActive(true);
        if (fadeOutCoroutine != null)
        {
            StopCoroutine(fadeOutCoroutine);
            fadeOutCoroutine = null;
        }
        ResetAlpha();

        if (fillCoroutine != null)
        {
            StopCoroutine(fillCoroutine);
        }

        Debug.Log("Fill amount: " + (life / BulletTarget.maxLife));
        fillCoroutine = StartCoroutine(UpdateEnemyLifeFillBar(life / BulletTarget.maxLife));
        
    }

    private IEnumerator UpdateEnemyLifeFillBar(float targetFillAmount)
    {
        float elapsedTime = 0f;
        float startFillAmount = lifeBarImage.fillAmount;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            lifeBarImage.fillAmount = Mathf.Lerp(startFillAmount, (startFillAmount - targetFillAmount), elapsedTime / duration);
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
            newColor = new Color(lifeBarBackground.color.r, lifeBarBackground.color.g, lifeBarBackground.color.b, alpha);
            lifeBarBackground.color = newColor;
        }
    }

    private void ResetAlpha()
    {
        SetAlpha(1.0f);
    }
}
