using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyLifeBar : MonoBehaviour
{
    [SerializeField] private ThirdPersonShooterController player;
    [SerializeField] private GameObject LifeBar;
    [SerializeField] private Image lifeBarImage;

    //private void OnEnable()
    //{
    //    BulletTarget.LifeChanged += OnEnemyLifeChange;
    //}

    //private void OnDisable()
    //{
    //    BulletTarget.LifeChanged -= OnEnemyLifeChange;
    //}

    private void Update()
    {
        if (ThirdPersonShooterController.bulletTarget != null)
        {
            ShowLifeBar(ThirdPersonShooterController.bulletTarget.Life);
        }
        else
        {
            LifeBar.SetActive(false);
        }
    }


    public void ShowLifeBar(float enemyLife)
    {
        lifeBarImage.fillAmount = (enemyLife / ThirdPersonShooterController.bulletTarget.MaxLife);
        LifeBar.SetActive(true);
    }
}
