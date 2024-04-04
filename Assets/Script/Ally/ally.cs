
using System.Collections;
using UnityEngine;
using UnityEngine.AI;


public class ally : MonoBehaviour, IDamage
{
    [Header("Статы Settler")]
    [SerializeField] private EnemiesTypes _enemiesTypes;
    [SerializeField] private int _health;
    [SerializeField] private int _damage;
    [SerializeField] private float _movespeed;
    [SerializeField] private Wallet _wallet;
    public int Damage => _damage;
    [SerializeField] private NavMeshAgent _agent;
     private Animator _animator;
    [Header("Звуки")]
    [SerializeField] private AudioManager _audioManager;
    [Header("Дистанция реагирования")]
    [SerializeField]private float startingRotationY = 45f;
    private Transform closestEnemy;
    [SerializeField] [Range(5f, 100f)] private float _detectionDistance = 10f;
    [SerializeField][Range(1f, 20f)] private float turnSpeed = 5f;

    [Header("Задание")] private Task _task; 

    private GameManager _manager;
    private void Start()
    {
        _health = _enemiesTypes.MaxHealth;
        _damage = _enemiesTypes.damage;
        _wallet = FindObjectOfType<Wallet>();
        _animator = GetComponent<Animator>();
        _manager = FindObjectOfType<GameManager>();
        _audioManager = FindObjectOfType<AudioManager>();
        _task = FindObjectOfType<Task>();
    }
    void Update()
    {
       SearchEnemy();
    }
    private void SearchEnemy()
    {

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");


        float closestDistanceSquared = _detectionDistance * _detectionDistance;

        foreach (GameObject enemyObj in enemies)
        {
            float distanceSquared = (enemyObj.transform.position - transform.position).sqrMagnitude;

            if (distanceSquared < closestDistanceSquared)
            {
                closestEnemy = enemyObj.transform;
                closestDistanceSquared = distanceSquared;
                _animator.SetBool("Attack", true);
            }
        }
        if (enemies.Length <= 0)
        {
            _animator.SetBool("Attack", false);
            if (_manager.IsStartReload)
            {
                if (!_manager.IsReload)
                {
                    _manager.GameOver();
                    _manager.IsReload = true;
                }
                _manager.IsStartReload = false;
            }
        }
        
        if (closestEnemy != null)
        {
            Vector3 directionToEnemy = closestEnemy.position - transform.position;
            float targetAngle = Mathf.Atan2(directionToEnemy.x, directionToEnemy.z) * Mathf.Rad2Deg + startingRotationY;
            Quaternion targetRotation = Quaternion.Euler(0f, targetAngle, 0f);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }
    }
    public void DamageAlly(int damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            _task.UpdateKillSoldier(3);
            _audioManager.PlaySound(5);
            _wallet.AddMoney(10);
            Destroy(gameObject);
        }
    }
    private void GetAudio()
    {
        _audioManager.PlaySound(5);
    }
    private void GetMoney()
    {
        _audioManager.PlaySound(6);
    }
}