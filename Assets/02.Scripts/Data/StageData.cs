using System;
using UnityEngine;

[Serializable]
public class StageData
{
    public GameObject stagePrefab;
    [Space] [Header("EnemyCount")] 
    public int yellowEnemyCount;
    public int blueEnemyCount;
    public int redEnemyCount;

}
