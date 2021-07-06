using UnityEngine;

public class Lightning : Ammo
{
    ParticleSystem particle;
    [SerializeField] LightningAim aim;

    private int count = 0;
    [SerializeField] private bool hit = false;
    [SerializeField] private Transform MyTarget;

    private float _damage;

    private void Start()
    {
        particle = GetComponent<ParticleSystem>();
        particle.Play();
    }
    public void Shoot(Character character, Transform target)
    {
        if (aim.target.Count > 1)
        {
            _damage = character.Damage;
            aim.target.Remove(target);
            NextTarget();
            _character = character;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void NextTarget()
    {
        foreach (var enemies in aim.target)
        {
            if (aim.IsVisible())
            {
                count++;
                MyTarget = enemies;
                break;
            }
        }
    }

    private void PickTarget(Collider other)
    {
        Character otherCharacter = other.GetComponent<Character>();
        otherCharacter.TakeDamage(_character, _damage);

        if (Level.curLevel > 2)
        {
            (_character as Player).Lifesteal(_character.Damage);
        }

        NextTarget();
        _damage = _character.Damage - _damage * 0.35f;
        hit = false;
        if (count == 3)
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        if (MyTarget != null)
            rigidbody.MovePosition(Vector3.MoveTowards(transform.position, MyTarget.position, speed * Time.fixedDeltaTime));
        else
            Destroy(gameObject);
    }
    private new void OnTriggerEnter(Collider other)
    {
        if (!hit && other.transform == MyTarget)
        {
            aim.target.Remove(other.transform);
            if (aim.target.Count == 0)
                Destroy(gameObject);
            hit = true;
            PickTarget(other);
        }
    }
}
