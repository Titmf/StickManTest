using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private float _maximumForce;
    [SerializeField] private float _maximumForceTime;

    private float _timeMouseButtonDown;

    private Camera _camera;
    
    void Awake()
    {
        _camera = GetComponent<Camera>();
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _timeMouseButtonDown = Time.time;
        }

        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                StickMan stickMan = hitInfo.collider.GetComponentInParent<StickMan>();

                if (stickMan != null)
                {
                    float mouseButtonDownDuration = Time.time - _timeMouseButtonDown;
                    float forcePercentage = mouseButtonDownDuration / _maximumForceTime;
                    float forceMagnitude = Mathf.Lerp(1, _maximumForce, forcePercentage);

                    Vector3 forceDirection = stickMan.transform.position - _camera.transform.position;
                    forceDirection.y = 1;
                    forceDirection.Normalize();

                    Vector3 force = forceMagnitude * forceDirection;
                    
                    stickMan.TriggerRagdoll(force, hitInfo.point);
                }
            }
        }
    }
}