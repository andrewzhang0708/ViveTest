using UnityEngine;

public class FollowTrackerTarget : MonoBehaviour
{
    public Transform tracker;
    public Vector3 positionOffset;
    public Vector3 rotationOffsetEuler;
    public float positionSmooth = 12f;
    public float rotationSmooth = 12f;

    private Quaternion rotationOffset;

    void Start()
    {
        rotationOffset = Quaternion.Euler(rotationOffsetEuler);
    }

    void Update()
    {
        if (tracker == null) return;

        Vector3 targetPos = tracker.position + tracker.rotation * positionOffset;
        Quaternion targetRot = tracker.rotation * rotationOffset;

        transform.position = Vector3.Lerp(transform.position, targetPos, positionSmooth * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotationSmooth * Time.deltaTime);
    }
}