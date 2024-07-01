using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieFlower : BulletTarget
{
    [SerializeField] private float _maxLife = 3.0f;
    [SerializeField] private GameObject happyFlower;
    [SerializeField] private GameObject health;
    [SerializeField] private float damage = 2.0f;
    [SerializeField] private float damageInterval = 2.0f;
    [SerializeField] private float healthDropPercentage = 2;
    [SerializeField] private Vector3 healthDropOffset;

    private NavMeshAgent agent;
    private ThirdPersonShooterController player;
    private Vector3 initialPosition;
    private bool isTouchingPlayer = false;
    private float timer = 0f;
    private bool spawnFinished = false;

    private void Start()
    {
        maxLife = _maxLife;
        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }
        if (player == null)
        {
            player = FindAnyObjectByType<ThirdPersonShooterController>();
        }
        initialPosition = transform.position;
    }

    private void Update()
    {
        if (spawnFinished && Life > 0)
        {
            agent.SetDestination(player.transform.position);
        }
        else
        {
            isTouchingPlayer = false;
            if (agent.isActiveAndEnabled)
            {
                agent.SetDestination(initialPosition);
            }
        }

        if (isTouchingPlayer)
        {
            timer += Time.deltaTime;

            if (timer >= damageInterval)
            {
                Player.PlayerLife -= damage;
                timer = 0f;
            }
        }
    }

    private void OnEnable()
    {
        FlowerSpawner.Spawned += FollowPlayer;

    }

    private void OnDisable()
    {
        FlowerSpawner.Spawned -= FollowPlayer;
    }

    protected override void Die()
    {
        DropLife();
        SpawnWateredFlower();
    }


    private void SpawnWateredFlower()
    {
        if (happyFlower != null)
        {
            Instantiate(happyFlower, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    private void DropLife()
    {
        int randomNumber = Random.Range(1, 10);
        if (randomNumber <= healthDropPercentage)
        {
            Vector3 healthPos = new Vector3(transform.position.x + healthDropOffset.x, transform.position.y + healthDropOffset.y, transform.position.z + healthDropOffset.z);
            Instantiate(health, healthPos, transform.rotation);
        }

    }

    private void FollowPlayer()
    {
        if (agent != null)
        {
            agent.enabled = true;
        }
        spawnFinished = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        ThirdPersonShooterController player = other.GetComponent<ThirdPersonShooterController>();

        if (player != null)
        {
            isTouchingPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        ThirdPersonShooterController player = other.GetComponent<ThirdPersonShooterController>();

        if (player != null)
        {
            isTouchingPlayer = false; ;
        }
    }
}
