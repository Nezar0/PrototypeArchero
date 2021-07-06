using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private EnemySpawner enemySpawner;
    private void Start()
    {
        enemySpawner.StartSpawnEnemies();
    }
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
