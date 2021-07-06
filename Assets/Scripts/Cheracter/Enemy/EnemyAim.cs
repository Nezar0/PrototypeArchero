using UnityEngine;

public class EnemyAim : Aim
{
    [SerializeField] Transform player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    public bool Aim()
    {
        bool success = false;
        success = IsVisible(player);
        return success;
    }
}
