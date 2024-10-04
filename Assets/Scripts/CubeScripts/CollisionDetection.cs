using UnityEngine;
using UnityEngine.Events;

public class CollisionDetection : MonoBehaviour
{
    [SerializeField] private UnityEvent _collided;

    private bool _wasCollision;

    private void OnEnable()
    {
        _wasCollision = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent<Plane>(out Plane plane) && _wasCollision == false)
        {
            _collided?.Invoke();
            _wasCollision = true;
        }
    }
}
