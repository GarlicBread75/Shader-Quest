using UnityEngine;
using UnityEngine.Events;

public class LockedOption : MonoBehaviour
{
    [SerializeField] UnityEvent[] sounds;
    [SerializeField] int shaderCount;

    public void Unlock(GameObject thingy)
    {
        if (shaderCount > 0)
        {
            Destroy(thingy);
            sounds[0].Invoke();
            shaderCount--;
        }
        else
        {
            sounds[1].Invoke();
        }
    }

    public void AddShaderCount(int num)
    {
        shaderCount += num;
    }
}
