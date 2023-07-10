using UnityEngine;

public class ShaderShards : MonoBehaviour
{
    [SerializeField] GameObject shaderCollectible;
    [SerializeField] Transform shaderSpawn;
    [SerializeField] int shardsCount;

    void FixedUpdate()
    {
        if (shardsCount == 0)
        {
            Instantiate(shaderCollectible, shaderSpawn.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void ChangeShardCount(int num)
    {
        shardsCount += num;
    }
}
