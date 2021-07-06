using System.Collections;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] ParticleSystem portalParticle;
    [SerializeField] private float timeToSpawn = 2.5f;
    [SerializeField] private GameObject enemies;
    [SerializeField] private Transform[] spawnPos;
    private EnemyController enemyController;

    private void Awake()
    {
        enemyController = GetComponent<EnemyController>();
        EnemyController.OnAllEnemyKilled += StartSpawnEnemies;
    }
    public void StartSpawnEnemies()
    {
        for (int i = 0; i < spawnPos.Count(); i++)
        {
            var portal = Instantiate(portalParticle, spawnPos[i].position, portalParticle.transform.rotation, transform);
            Destroy(portal.gameObject, timeToSpawn);
            StartCoroutine(SpawnEnemies(i));
        }
    }
    private IEnumerator SpawnEnemies(int i)
    {
        yield return new WaitForSeconds(timeToSpawn);
        var newEnemy = Instantiate(enemies, transform);
        newEnemy.transform.position = spawnPos[i].position;
        enemyController.AddEnemy(newEnemy.GetComponent<Enemy>());
    }
    private void OnDestroy()
    {
        EnemyController.OnAllEnemyKilled -= StartSpawnEnemies;
    }
}
