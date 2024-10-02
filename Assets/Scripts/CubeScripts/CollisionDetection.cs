using UnityEngine;
using UnityEngine.Events;

public class CollisionDetection : MonoBehaviour
{
    [SerializeField] private UnityEvent _collided;

    private CollisionDetection _collisionDetection;
    private bool wasCollision;

    private void Awake()
    {
        _collisionDetection = GetComponent<CollisionDetection>();
    }

    private void OnEnable()
    {
        wasCollision = false;
        Debug.Log(wasCollision);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.gameObject.GetComponent<Plane>() == true && wasCollision == false)
        {
            _collided.Invoke();
            wasCollision = true;
        }
    }
}
