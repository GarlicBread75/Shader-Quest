using UnityEngine;

public class CustomizationStation : MonoBehaviour
{
    [SerializeField] GameObject customizationMenu;
    [SerializeField] float hueModifier;
    Renderer rend;
    bool canColour = true;
    float hue;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void FixedUpdate()
    {
        if (!canColour)
        {
            return;
        }

        hue += hueModifier;

        if (hue > 1)
        {
            hue = 0;
        }

        rend.material.SetColor("_BaseColor", Color.HSVToRGB(hue, 1f, 1f));
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canColour = false;
            rend.material.SetColor("_BaseColor", Color.white);
            rend.material.SetColor("_OutlineColor", Color.black);
            customizationMenu.SetActive(true);
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canColour = true;
            rend.material.SetColor("_OutlineColor", Color.white);
            customizationMenu.SetActive(false);
        }
    }
}
