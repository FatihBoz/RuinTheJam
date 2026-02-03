using System.Collections.Generic;
using UnityEngine;

public class LevelComponent : MonoBehaviour
{
    [SerializeField] private Transform enemySpawnPointsHolder;
    [SerializeField] private LevelComponentType componentType;

    public List<Transform> GetEnemySpawnPoints()
    {
        List<Transform> spawnPoints = new();
        for (int i = 0; i < enemySpawnPointsHolder.childCount; i++)
        {
            spawnPoints.Add(enemySpawnPointsHolder.GetChild(i));
        }
        return spawnPoints;
    }


    public int GetEnemySpawnPointsCount()
    {
        return enemySpawnPointsHolder.childCount;
    }

    public LevelComponentType GetComponentType()
    {
        return componentType;
    }

}
