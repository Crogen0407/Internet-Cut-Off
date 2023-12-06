using UnityEngine;

[CreateAssetMenu(menuName = "SO/EnemyInfoData", fileName = "New EnemyInfoData")]
public class SO_EnemyInfoData : ScriptableObject
{
    public EnemyType enemyType = EnemyType.Yellow;
    public float moveSpeed = 1;
    public float attackDamage = 1;
    public float attackDelay = 1;
    public float maxAttackRangeRadius = 0.5f;
    public float viewingRadius = 5;
}