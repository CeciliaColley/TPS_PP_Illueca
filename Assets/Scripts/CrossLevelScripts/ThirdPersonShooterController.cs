using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;
using UnityEngine.InputSystem;
using System;

public class ThirdPersonShooterController : MonoBehaviour {

    [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField] private float normalSensitivity = 1.0f;
    [SerializeField] private float aimSensitivity = 0.5f;
    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();
    [SerializeField] private Transform debugTransform;
    [SerializeField] private Transform pfBulletProjectile;
    [SerializeField] private Transform spawnBulletPosition;
    [SerializeField] private Transform waterSplash;
    [SerializeField] private string enemyLayerName;

    [HideInInspector]
    public RaycastHit objectHit;
    private bool _enemyDetected;
    public static BulletTarget bulletTarget;

    private ThirdPersonController thirdPersonController;
    private StarterAssetsInputs starterAssetsInputs;
    private Animator animator;

    public bool EnemyDetected
    {
        get { return _enemyDetected; }
        set 
        { 
            _enemyDetected = value; 
        }
    }

    public static Action<float> EnemyWasDetected;

    private void Awake() 
    {

        thirdPersonController = GetComponent<ThirdPersonController>();
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        animator = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update() {
        Vector3 mouseWorldPosition = Vector3.zero;

        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        Transform hitTransform = null;
        if (Physics.Raycast(ray, out objectHit, 999f, aimColliderLayerMask))
        {
            int enemyLayerIndex = LayerMask.NameToLayer(enemyLayerName);
            if (objectHit.collider.gameObject.layer == enemyLayerIndex)
            {
                bulletTarget = objectHit.transform.GetComponent<BulletTarget>();
            }
            else
            {
                bulletTarget = null;
            }


            debugTransform.position = objectHit.point;
            mouseWorldPosition = objectHit.point;
            hitTransform = objectHit.transform;
        }

        if (starterAssetsInputs.aim)
        {
            if (Player.HasGun)
            {
                PerformAim(mouseWorldPosition);
            }
        }
        else
        {
            UseFollowCamera();
        }


        if (starterAssetsInputs.shoot)
        {
            if (Gun.Ammo > 0)
            {
                Gun.Ammo--;
                // Hit Scan Shoot
                if (hitTransform != null)
                {
                    bulletTarget = hitTransform.GetComponent<BulletTarget>();
                    // Decrease life
                    if (bulletTarget != null)
                    {
                        bulletTarget.Life--;
                    }
                    // Hit something
                    Instantiate(waterSplash, mouseWorldPosition, Quaternion.identity);
                }
            }


            //// Projectile Shoot: does not work well.
            //Vector3 aimDir = (mouseWorldPosition - spawnBulletPosition.position).normalized;
            //Instantiate(pfBulletProjectile, spawnBulletPosition.position, Quaternion.LookRotation(aimDir, Vector3.up));

            starterAssetsInputs.shoot = false;
        }
    }

    private void PerformAim(Vector3 mouseWorldPosition)
    {
        aimVirtualCamera.gameObject.SetActive(true);
        thirdPersonController.SetSensitivity(aimSensitivity);
        thirdPersonController.SetRotateOnMove(false);
        animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 1f, Time.deltaTime * 13f));

        Vector3 worldAimTarget = mouseWorldPosition;
        worldAimTarget.y = transform.position.y;
        Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

        transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
    }

    private void UseFollowCamera()
    {
        aimVirtualCamera.gameObject.SetActive(false);
        thirdPersonController.SetSensitivity(normalSensitivity);
        thirdPersonController.SetRotateOnMove(true);
        animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 0f, Time.deltaTime * 13f));
    }
}