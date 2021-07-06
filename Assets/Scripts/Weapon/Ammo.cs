using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] protected float speed = 5;
    [SerializeField] protected new Rigidbody rigidbody;

    protected Character _character;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    public virtual void Shoot(Character character)
    {
        _character = character;
        rigidbody.velocity = transform.forward * speed;
    }
    protected void OnTriggerEnter(Collider other)
    {
        string tag = other.tag;
        Character otherCharacter = other.GetComponent<Character>();
        if (_character != otherCharacter)
        {
            switch (tag)
            {
                case "Obstacles":
                    Destroy(gameObject);
                    break;
                case "Enemy":
                    if (_character.CompareTag("Player"))
                    {
                        if (Level.curLevel >= 1)
                        {
                            (_character as Player).LightningAttack(otherCharacter);
                            if (Level.curLevel >= 2)
                            {
                                (_character as Player).Lifesteal(_character.Damage);
                            }
                        }
                    }
                    otherCharacter.TakeDamage(_character, _character.Damage);
                    Destroy(gameObject);
                    break;
                case "Player":
                    otherCharacter.TakeDamage(_character, _character.Damage);
                    Destroy(gameObject);
                    break;
            }
        }
    }
}
