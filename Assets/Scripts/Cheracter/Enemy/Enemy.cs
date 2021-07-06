using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    [SerializeField] private Transform player;
    [SerializeField] private EnemyController enemyController;
    [SerializeField] private float movingTime;
    [SerializeField] private float waitingTime;
    [SerializeField] private float randomTime;
    [SerializeField] private int expToDrop = 100;

    [SerializeField] public Shooter shooter;
    [SerializeField] EnemyAim aimer;
    private float lastShootTime;

    private Animator anim;
    private float movingStateTimer = 0;
    NavMeshAgent agent;

    private new void Awake()
    {
        base.Awake();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (agent == null)
            agent = GetComponent<NavMeshAgent>();
        agent.speed = Speed;
        enemyController = GetComponentInParent<EnemyController>();
        shooter = GetComponentInChildren<Shooter>();
        aimer = GetComponentInChildren<EnemyAim>();
        anim = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        switch (_state)
        {
            case MovingState.moving:
                if (Time.time - movingStateTimer >= movingTime)
                {
                    _state = MovingState.staying;
                    anim.SetBool("isMoving", false);
                    movingStateTimer = Time.time;
                }
                else
                {
                    agent.destination = player.position;
                }
                break;
            case MovingState.staying:
                if (Time.time - movingStateTimer >= waitingTime)
                {
                    _state = MovingState.moving;
                    anim.SetBool("isMoving", true);
                    movingStateTimer = Time.time + Random.Range(0, randomTime);
                }
                else
                {
                    agent.destination = transform.position;
                }
                break;
            default:
                break;
        }
        if (_state == MovingState.staying)
        {
            if (aimer.Target == null)
                aimer.Aim();
            else if (!aimer.IsVisible())
                aimer.ResetTarget();
        }
    }
    private void Update()
    {
        if (aimer.Target != null)
        {
            anim.SetBool("isMoving", false);
            _state = MovingState.staying;
            aimer.FollowTarget(transform);
            if (Time.time - lastShootTime >= (1 / attackSpeed))
            {
                anim.SetBool("isAttacking", true);
                lastShootTime = Time.time;
                shooter.Shoot(this);
            }
        }
        else
            anim.SetBool("isAttacking", false);
    }

    protected override void Death(Character killer)
    {
        tag = "Untagged";
        anim.SetBool("isDied", false);
        Player player = killer as Player;
        enemyController.RemoveEnemy(this);
        if (player != null)
        {
            player.aimer.ResetTarget();
            player.AddExp(expToDrop);
        }
        GetComponent<Animator>().enabled = false;
        setRigidbodyState(false);
        setColliderState(true);
        MonoBehaviour[] comps = GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour c in comps)
        {
            c.enabled = false;
        }
        Destroy(gameObject, 5f);
    }
}
