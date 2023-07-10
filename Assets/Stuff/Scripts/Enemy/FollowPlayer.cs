using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    Vector3 player;
    bool canFollow;
    Renderer rend;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform.position;
        rend = GetComponent<Renderer>();
    }

    void FixedUpdate()
    {
        if (canFollow)
        {

        }
    }

    void OnTriggerEnter(Collider trigger)
    {
        if (trigger.gameObject.CompareTag("Player"))
        {
            canFollow = true;
            rend.material.SetColor("_BaseColor", Color.HSVToRGB(0f, 1f, 0.7f));
        }
    }

    void OnTriggerExit(Collider trigger)
    {
        if (trigger.gameObject.CompareTag("Player"))
        {
            canFollow = false;
            rend.material.SetColor("_BaseColor", Color.HSVToRGB(0.6f, 1f, 0.7f));
        }
    }
}
