using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private List<Enemy> _enemies;
    public List<Enemy> Enemies { get => _enemies; private set => _enemies = value; }

    public static event Action OnAllEnemyKilled = delegate { };
    public void AddEnemy(Enemy enemy)
    {
        _enemies.Add(enemy);
    }
    public void RemoveEnemy(Enemy enemy)
    {
        _enemies.Remove(enemy);
        if (_enemies.Count == 0)
            OnAllEnemyKilled();
    }
}
