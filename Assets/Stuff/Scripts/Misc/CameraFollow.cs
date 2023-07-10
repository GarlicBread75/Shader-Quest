using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Vector3 followOffset;
    [SerializeField] Vector3 levelFinishOffset;
    [SerializeField] float smoothSpeed;
    Vector3 offset;
    float smoothSpd;

    void Start()
    {
        smoothSpd = smoothSpeed;
        offset = followOffset;
    }

    void Update()
    {
        Vector3 desiredPos = target.position + followOffset;
        Vector3 smoothPos = Vector3.Lerp(transform.position, desiredPos, smoothSpd * Time.deltaTime);
        transform.position = smoothPos;
    }

    public void StopFollowing()
    {
        smoothSpd = 1.5f;
    }

    public void ResumeFollowing()
    {
        smoothSpd = smoothSpeed;
        offset = followOffset;
    }

    public void LevelFinish()
    {
        offset = levelFinishOffset;
    }
}
