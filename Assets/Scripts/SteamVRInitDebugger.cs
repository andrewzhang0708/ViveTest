using UnityEngine;
using Valve.VR;

public class SteamVRInitDebugger : MonoBehaviour
{
    void Start()
    {
        EVRInitError error = EVRInitError.None;
        OpenVR.Init(ref error, EVRApplicationType.VRApplication_Scene);

        if (error != EVRInitError.None)
        {
            Debug.LogError("OpenVR.Init failed: " + error);
            return;
        }

        Debug.Log("OpenVR.Init succeeded.");
    }

    void Update()
    {
        var system = OpenVR.System;

        if (system == null)
        {
            Debug.LogWarning("OpenVR.System is still null.");
            return;
        }

        var poses = new TrackedDevicePose_t[OpenVR.k_unMaxTrackedDeviceCount];

        if (OpenVR.Compositor != null)
        {
            OpenVR.Compositor.GetLastPoses(poses, null);
        }
        else
        {
            Debug.LogWarning("OpenVR.Compositor is null.");
        }

        for (uint i = 0; i < OpenVR.k_unMaxTrackedDeviceCount; i++)
        {
            if (!system.IsTrackedDeviceConnected(i)) continue;

            var deviceClass = system.GetTrackedDeviceClass(i);
            bool valid = poses[i].bPoseIsValid;

            var m = poses[i].mDeviceToAbsoluteTracking;
            Vector3 pos = new Vector3(m.m3, m.m7, -m.m11);

            Debug.Log($"Device {i}: class={deviceClass}, valid={valid}, pos={pos}");
        }
    }

    void OnDestroy()
    {
        OpenVR.Shutdown();
    }
}