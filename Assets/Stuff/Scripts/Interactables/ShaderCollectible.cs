using UnityEngine;
using UnityEngine.Events;

public class ShaderCollectible : MonoBehaviour
{
    [SerializeField] GameObject shaderModel;
    [SerializeField] float hueModifier;
    [SerializeField] bool canColour;
    Renderer rend;
    float hue;
    
    [Space]

    [SerializeField] float rotationSpeed;
    float rotY;

    [Space]
    [Space]
    [Space]

    [SerializeField] UnityEvent pickup;

    void Start()
    {
        rend = shaderModel.GetComponent<Renderer>();
    }

    private void Awake()
    {
        pickup = GameObject.Find("Shader Events Copy").GetComponent<EventsThingy>().events;
    }

    void FixedUpdate()
    {
        rotY += Time.deltaTime * rotationSpeed;
        transform.rotation = Quaternion.Euler(0, rotY, 0);

        if (canColour)
        {
            hue += hueModifier;

            if (hue > 1)
            {
                hue = 0;
            }

            rend.material.SetColor("_RimColor", Color.HSVToRGB(hue, 1f, 1f));
        }
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
