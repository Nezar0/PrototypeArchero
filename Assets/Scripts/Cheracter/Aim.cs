using UnityEngine;

public abstract class Aim : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] LayerMask obstacleMask = 6;
    public Transform Target { get => _target; private set => _target = value; }
    protected bool IsVisible(Transform target)
    {
        Vector3 dirToTarget = (target.position - transform.position).normalized;
        float dstToTarget = Vector3.Distance(transform.position, target.position);
        if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
        {
            _target = target;
            return true;
        }
        return false;
    }
    public bool IsVisible()
    {
        Vector3 dirToTarget = (_target.position - transform.position).normalized;
        float dstToTarget = Vector3.Distance(transform.position, _target.position);
        return !Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask);
    }
    public void ResetTarget()
    {
        _target = null;
    }
    public void FollowTarget(Transform root)
    {
        transform.rotation = root.rotation;
        root.LookAt(_target);
    }
}
