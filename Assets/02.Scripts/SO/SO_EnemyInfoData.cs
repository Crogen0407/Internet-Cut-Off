using UnityEngine;

[CreateAssetMenu(menuName = "SO/EnemyInfoData", fileName = "New EnemyInfoData")]
public class SO_EnemyInfoData : ScriptableObject
{
    public EnemyType enemyType = EnemyType.Yellow;
    public HealthSystem healthSystem;
    public float moveSpeed = 1;
    public float attackDamage = 1;
    public float maxAttackRangeRadius = 0.5f;
}