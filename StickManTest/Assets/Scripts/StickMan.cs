using UnityEngine;

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
    
    
    void Awake()
    {
        _ragdolRigidBodies = new[] { GetComponentInChildren<Rigidbody>() };
        
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

    private void DisableRagdoll()
    {
        foreach (var rigidbody in _ragdolRigidBodies)
        {
            rigidbody.isKinematic = true;
        }
    }

    private void EnableRagdoll()
    {
        foreach (var rigidbody in _ragdolRigidBodies)
        {
            rigidbody.isKinematic = false;
        }
    }

    private void IdleBehaviour()
    {
        Vector3 direction = _camera.transform.position - transform.position;
        direction.y = 0;
        direction.Normalize();

        Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 20 * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            EnableRagdoll();
            _currentState = StickManState.Ragdoll;
        }
    }

    private void RagdollBehaviour()
    {
        
    }
}