using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private GameObject ammoPrefab;
    public void Shoot(Character character)
    {
        var newAmmo = Instantiate(ammoPrefab);
        newAmmo.transform.position = transform.position;
        newAmmo.transform.rotation = transform.rotation;
        newAmmo.GetComponent<Ammo>().Shoot(character);
    }
}
