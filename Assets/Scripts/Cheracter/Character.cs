using System;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    protected enum MovingState
    {
        moving,
        staying
    }

    [SerializeField] protected float speed;
    [SerializeField] protected float maxHp;
    [SerializeField] protected float attackSpeed;
    [SerializeField] protected float damage;

    [SerializeField] protected float hp;

    public event Action<float> OnHealthChanged = delegate { };

    protected MovingState _state = MovingState.staying;
    public float Speed => speed;
    public float MaxHp => maxHp;
    public float Hp => hp;
    public float AttackSpeed => attackSpeed;
    public float Damage => damage;
    protected void Awake()
    {
        hp = maxHp;
    }

    private void Start()
    {
        setRigidbodyState(true);
        setColliderState(false);
    }

    public void TakeDamage(Character attacker, float damage)
    {
        hp -= damage;
        OnHealthChanged(hp / maxHp);
        if (hp <= 0)
        {
            Death(attacker);
        }
    }

    protected void setRigidbodyState(bool state)
    {
        Rigidbody[] Rigidbodies = GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody rb in Rigidbodies)
        {
            rb.isKinematic = state;
        }

        GetComponent<Rigidbody>().isKinematic = !state;

    }
    protected void setColliderState(bool state)
    {
        Collider[] Colliders = GetComponentsInChildren<Collider>();

        foreach (Collider col in Colliders)
        {
            col.enabled = state;
        }

        GetComponent<Collider>().enabled = !state;
    }
    protected abstract void Death(Character killer);
}
