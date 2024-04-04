
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemiesTypes", menuName = "EnemiesTypes")]
public class EnemiesTypes : ScriptableObject
{
    public int MaxHealth;
    public int damage;
    public float moveSpeed;
}
