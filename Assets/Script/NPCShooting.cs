using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;

public class NPCShooting : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint; 
    [SerializeField] [Range(1f, 100f)] private float bulletSpeed = 10f; 
    public LayerMask enemyLayer; 
    private float detectionRadius = 5f; 
    [SerializeField] [Range(0.1f, 10f)]  private float fireRate = 1f;
    [SerializeField] private AudioSource _audioSource;


    private float _kd;
    private float nextFireTime = 0f;
    private bool isShooting = false;
    [SerializeField] private ParticleSystem _fxattack;
    private void Start()
    {
        _fxattack = GetComponentInChildren<ParticleSystem>();
    }
    private void Update()
    {
        HandleShooting();
    }
    private void OnDrawGizmos()
    {
        // Убедитесь, что используете эту функцию в режиме редактирования и во время выполнения
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
    private void HandleShooting()
    {
        if (Time.time >= nextFireTime && !isShooting)
        {
            
            Collider[] enemies = Physics.OverlapSphere(transform.position, detectionRadius, enemyLayer);
            if (enemies.Length > 0)
            {
                
                StartCoroutine(Shoot());
            }
        }
    }

    private IEnumerator Shoot()
    {
        isShooting = true;
        _fxattack.Play();
        if(!_audioSource.isPlaying)
        {
           StartCoroutine(FireAudio());
        }
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        bulletRigidbody.AddForce(firePoint.forward * bulletSpeed, ForceMode.Impulse);
        bullet.transform.parent = firePoint;
        // Установите следующее время выстрела в соответствии с fireRate
        nextFireTime = Time.time + 1f / fireRate;

        // Подождите fireRate перед отключением флага isShooting
        yield return new WaitForSeconds(1f / fireRate);
        isShooting = false;
    }

    private IEnumerator FireAudio()
    {
        _kd = Random.Range(0.05f , 0.15f);
        yield return new WaitForSeconds(_kd);
        _audioSource.Play();
    }

}
