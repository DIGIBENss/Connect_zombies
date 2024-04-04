
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private ally _ally;
    private void Start()
    {
        _ally = GetComponentInParent<ally>();
        Invoke("DieBullet", 2f);
    }
    private void OnCollisionEnter(Collision collision)
    {

        EnemyAi enemy = collision.gameObject.GetComponentInChildren<EnemyAi>();
        
            if(enemy != null)
            {
                enemy.DamageEmeny(_ally.Damage);
                DieBullet();
            }
            


    }
    private void DieBullet()
    {
        Destroy(gameObject);
    }
}
