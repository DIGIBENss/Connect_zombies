
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour, IDamage
{
    [SerializeField] private EnemiesTypes _enemiesTypes;
    [SerializeField] private int _health;
    [SerializeField] private int _damage;
    public int Damage => _damage;
    [SerializeField] private float _movespeed;

    [SerializeField] private Transform target;  // Ссылка на цель (солдата)
    [SerializeField]  private NavMeshAgent _agent; // Компонент для навигации противника
    [SerializeField] private AudioManager _audioManager;

    [SerializeField] private Animator _animator;
    private bool isAttacking = false;
    private float attackCooldown = 2f; // Время задержки между атаками
    private float attackTimer = 0f;
    [SerializeField] private AudioSource _audioSource;
    void Start()
    {
        _audioManager = FindObjectOfType<AudioManager>();
        _agent = GetComponent<NavMeshAgent>();
        _health = _enemiesTypes.MaxHealth;
        _damage = _enemiesTypes.damage;
        _movespeed = _enemiesTypes.moveSpeed;
        _agent.speed = _movespeed;


    }
    void Update()
    {
        if (target != null && _agent != null)
        {
         _agent.SetDestination(target.position);
        }
        else
        {
         FindNewTarget();
        }
        Anim();
        AttackZombie();   
    }
    private void AttackZombie()
    {
        if (isAttacking)
        {          
            attackTimer += Time.deltaTime;
            if (attackTimer >= attackCooldown)
            {
                isAttacking = false;
                attackTimer = 0f;
                _animator.SetBool("Attack", false);
                _agent.speed = _movespeed; 
            }
        }
    }
    private void Anim()
    {
        if(_agent.speed == 0f)
        {
            _animator.SetBool("Run",false);
            _animator.SetBool("Idle", true);
        }
        else
        {
            _animator.SetBool("Idle", false);
            _animator.SetBool("Run", true);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("settler"))
        {
            if (!isAttacking)
            {
              isAttacking = true;
              _agent.speed = 0f;
              _animator.SetBool("Attack", true);
              StartCoroutine(ResetAttack());
              ally _ally = other.GetComponent<ally>();
              _ally.DamageAlly(_damage);
            }
        }
    }
    private IEnumerator ResetAttack()
    {
        float random = Random.Range(0.25f, 1f);
        yield return new WaitForSeconds(random);
        _audioSource.Play();
    }
    public void DamageEmeny(int damage)
    {
     _health -= damage;
        if (_health <= 0)
        {
            StartCoroutine(DieZombie());     
        }
    }
    private IEnumerator DieZombie()
    {
        _audioManager.PlaySound(1);
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
    private void FindNewTarget()
    {
        GameObject[] settlers = GameObject.FindGameObjectsWithTag("settler");
        float closestDistance = float.MaxValue;

        foreach (GameObject settler in settlers)
        {
            float distance = Vector3.Distance(transform.position, settler.transform.position);
            if (distance < closestDistance)
            {
              closestDistance = distance;
              target = settler.transform;
            }
        }
        if(target != null)
        {
            _agent.SetDestination(target.position);
        }
        else
        {
            if(FindObjectOfType<GameManager>().IsStopReload == false)
            {
                FindObjectOfType<GameManager>().PassedLvl();
            }
        }
    }
}
