using UnityEngine;

public class PlayerAim : Aim
{
    [SerializeField] EnemyController EnemyController;
    public bool Aim()
    {
        bool success = false;
        foreach (var enemies in EnemyController.Enemies)
        {
            success = IsVisible(enemies.transform);
        }
        return success;
    }
}
