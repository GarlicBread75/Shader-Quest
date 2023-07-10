using UnityEngine;
using UnityEngine.Events;

public class ShardCollectible : MonoBehaviour
{
    [SerializeField] float rotationSpeed;
    float rotY;

    [Space]
    [Space]
    [Space]

    [SerializeField] UnityEvent pickup;

    void Awake()
    {
        pickup = GameObject.Find("Shard Events Copy").GetComponent<EventsThingy>().events;
    }

    void FixedUpdate()
    {
        rotY += Time.deltaTime * rotationSpeed;
        transform.rotation = Quaternion.Euler(0, rotY, 0);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            pickup.Invoke();
            Destroy(gameObject);
        }
    }
}
