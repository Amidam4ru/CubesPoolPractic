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
        if (collision.transform.gameObject.GetComponent<Plane>() == true && _wasCollision == false)
        {
            _collided.Invoke();
            _wasCollision = true;
        }
    }
}
