using UnityEngine;

public class FakeHeadFromWaist : MonoBehaviour
{
    public Transform waistTracker;
    public Transform leftHandTracker;
    public Transform rightHandTracker;

    public float heightAboveWaist = 0.85f;
    public float forwardOffset = 0.15f;
    public float smooth = 10f;

    void Update()
    {
        if (waistTracker == null) return;

        Vector3 targetPos = waistTracker.position + Vector3.up * heightAboveWaist + waistTracker.forward * forwardOffset;

        transform.position = Vector3.Lerp(transform.position, targetPos, smooth * Time.deltaTime);

        Vector3 forward = waistTracker.forward;

        if (leftHandTracker != null && rightHandTracker != null)
        {
            Vector3 handCenter = (leftHandTracker.position + rightHandTracker.position) * 0.5f;
            Vector3 dir = handCenter - waistTracker.position;
            dir.y = 0f;

            if (dir.sqrMagnitude > 0.01f)
                forward = dir.normalized;
        }

        Quaternion targetRot = Quaternion.LookRotation(forward, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, smooth * Time.deltaTime);
    }
}