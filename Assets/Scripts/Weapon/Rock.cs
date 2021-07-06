using UnityEngine;

public class Rock : Ammo
{
    private Transform shooterPoint;

    private float gravity = Physics.gravity.y;
    private Vector3 targetPoint;
    void Update()
    {
        shooterPoint.localEulerAngles = new Vector3(-45, 0, 0);
    }
    public override void Shoot(Character character)
    {
        SpeedCalculate();
        shooterPoint = (character as Enemy).shooter.transform;
        _character = character;
        rigidbody.velocity = shooterPoint.forward * speed;
    }

    private void SpeedCalculate()
    {
        Vector3 fromTo = targetPoint - transform.position;
        Vector3 fromToXZ = new Vector3(fromTo.x, 0f, fromTo.z);
        float x = fromToXZ.magnitude;
        float y = fromTo.y;

        float angleInRadians = 45 * Mathf.PI / 180;

        float speedSquare = (gravity * x * x) / (2 * (y - Mathf.Tan(angleInRadians) * x) * Mathf.Pow(Mathf.Cos(angleInRadians), 2));
        speed = Mathf.Sqrt(Mathf.Abs(speedSquare)) + 10.0f;
    }
}
