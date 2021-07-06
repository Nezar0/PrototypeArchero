using System.Collections.Generic;
using UnityEngine;

public class LightningAim : Aim
{
    private GameObject[] enemy;

    public List<Transform> target = new List<Transform>();

    private void Awake()
    {
        enemy = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemies in enemy)
        {
            if (IsVisible(enemies.transform))
            {
                target.Add(enemies.transform);
            }
        }
    }
}
