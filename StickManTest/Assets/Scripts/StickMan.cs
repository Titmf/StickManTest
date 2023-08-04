using UnityEngine;
using System.Linq;

public class StickMan : MonoBehaviour
{
    private enum StickManState
    {
        Ragdoll,
        Idle
    }

    [SerializeField] private Camera _camera;
    
    private Rigidbody[] _ragdolRigidBodies;
    private StickManState _currentState = StickManState.Idle;
    private Animator _animator;
    private CharacterController _characterController;
    
    void Awake()
    {
        _ragdolRigidBodies = new[] { GetComponentInChildren<Rigidbody>() };
        _animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();

        DisableRagdoll();
    }

    void Update()
    {
        switch (_currentState)
        {
            case StickManState.Idle:
                IdleBehaviour();
                break;
            case StickManState.Ragdoll:
                RagdollBehaviour();
                break;
        }
    }

    public void TriggerRagdoll(Vector3 force, Vector3 hitPoint)
    {
        EnableRagdoll();

        Rigidbody hitRigidbody = _ragdolRigidBodies.
            OrderBy(rigidbody => Vector3.Distance(rigidbody.position, hitPoint)).First();
        
        hitRigidbody.AddForceAtPosition(force,hitPoint,ForceMode.Impulse);

        _currentState = StickManState.Ragdoll;
    }

    private void DisableRagdoll()
    {
        foreach (var rigidbody in _ragdolRigidBodies)
        {
            rigidbody.isKinematic = true;
        }
        
        _animator.enabled = true;
        _characterController.enabled = true;
    }

    private void EnableRagdoll()
    {
        foreach (var rigidbody in _ragdolRigidBodies)
        {
            rigidbody.isKinematic = false;
        }

        _animator.enabled = false;
        _characterController.enabled = false;
    }

    private void IdleBehaviour()
    {
        Vector3 direction = _camera.transform.position - transform.position;
        direction.y = 0;
        direction.Normalize();

        Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 20 * Time.deltaTime);
    }

    private void RagdollBehaviour()
    {
        
    }
}