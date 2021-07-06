using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Joystick joystick;
    private Rigidbody rb;
    private Character player;
    private Vector3 moveDir;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        player = GetComponent<Character>();
    }
    private void Update()
    {
        ProcessInputs();
    }
    private void FixedUpdate()
    {
        Move();
    }
    private void ProcessInputs()
    {
        float moveX = joystick.Horizontal;
        float moveY = joystick.Vertical;
        moveDir = new Vector3(moveX, 0, moveY).normalized;
    }
    private void Move()
    {
        GetComponent<Player>().CheckMovementState(moveDir);
        transform.LookAt(transform.position + moveDir);
        rb.velocity = moveDir * player.Speed;
        if (moveDir == Vector3.zero)
            rb.angularVelocity = moveDir;
    }
}
