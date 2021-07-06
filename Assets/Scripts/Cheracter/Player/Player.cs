using System;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private Shooter shooter;
    [SerializeField] private GameObject Lightning;
    public PlayerAim aimer;
    [SerializeField] private long exp = 0;
    private Animator anim;
    private float lastShootTime;

    public event Action<float> OnExpChanged = delegate { };
    public static event Action OnPlayerDied = delegate { };
    protected new void Awake()
    {
        base.Awake();
        shooter = GetComponentInChildren<Shooter>();
        aimer = GetComponentInChildren<PlayerAim>();
        anim = GetComponent<Animator>();
    }

    public void CheckMovementState(Vector2 direction)
    {
        if (_state == MovingState.staying && direction != Vector2.zero)
        {
            anim.SetBool("isMoving", true);
            _state = MovingState.moving;
        }
        else
        {
            if (_state == MovingState.moving && direction == Vector2.zero)
            {
                anim.SetBool("isMoving", false);
                _state = MovingState.staying;
            }
        }
    }
    private void FixedUpdate()
    {
        if (aimer.Target == null)
            aimer.Aim();
        else if (!aimer.IsVisible())
            aimer.ResetTarget();
    }
    private void Update()
    {
        if (_state == MovingState.staying)
        {
            if (aimer.Target != null)
            {
                if (Time.time - lastShootTime >= (1 / attackSpeed))
                {
                    aimer.FollowTarget(transform);
                    anim.SetBool("isAttacking", true);
                    lastShootTime = Time.time;
                    shooter.Shoot(this);
                }
            }
            else
                anim.SetBool("isAttacking", false);
        }
    }

    public void Lifesteal(float damage)
    {
        hp += damage * 0.23f;
        if (hp > maxHp)
            hp = maxHp;
        GetComponentInChildren<HealthBar>().ChangedHealthBar(hp / maxHp);
    }

    public void LightningAttack(Character startPos)
    {
        var newLightning = Instantiate(Lightning, startPos.transform.position, Quaternion.identity);
        newLightning.GetComponent<Lightning>().Shoot(this, startPos.transform);
    }

    public void AddExp(int count)
    {
        exp += count;
        OnExpChanged(exp);
    }
    protected override void Death(Character killer)
    {
        OnPlayerDied();
        anim.SetBool("isDied", false);
        GetComponent<Animator>().enabled = false;
        setRigidbodyState(false);
        setColliderState(true);
        MonoBehaviour[] comps = GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour c in comps)
        {
            c.enabled = false;
        }
        Debug.Log("Player is DEAD");
    }
}
