using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AmmoBarBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject AmmoBar;
    [SerializeField ]private Image ammoBarImage;
    [SerializeField] private float duration = 0.5f;

    private Coroutine fillCoroutine;

    private void OnEnable()
    {
        Player.OnAcquireGun += ActivateAmmoBar;
        Gun.AmmoChange += ModifyAmmoFillBar;
    }

    private void OnDisable()
    {
        Player.OnAcquireGun -= ActivateAmmoBar;
        Gun.AmmoChange += ModifyAmmoFillBar;
    }

    private void ActivateAmmoBar(bool hasGun)
    {
        AmmoBar.SetActive(hasGun);
    }

    private void ModifyAmmoFillBar(float ammo)
    {
        if (ammoBarImage == null)
            return;

        if (fillCoroutine != null)
            StopCoroutine(fillCoroutine);

        fillCoroutine = StartCoroutine(UpdateAmmoBarFill(ammo / Gun.maxAmmo));
    }

    private IEnumerator UpdateAmmoBarFill(float targetFillAmount)
    {
        float elapsedTime = 0f;
        float startFillAmount = ammoBarImage.fillAmount;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            ammoBarImage.fillAmount = Mathf.Lerp(startFillAmount, targetFillAmount, elapsedTime / duration);
            yield return null;
        }

        ammoBarImage.fillAmount = targetFillAmount;
    }
}
