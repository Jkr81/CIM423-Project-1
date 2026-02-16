using UnityEngine;
using UnityEngine.XR;
using Unity.XR.CoreUtils;

public class XROriginSnapToStart : MonoBehaviour
{
    [Header("References")]
    public XROrigin xrOrigin;
    public Transform playerStart;

    [Header("Options")]
    public bool alsoRecenter = false; // try this if you want a stronger reset

    void Start()
    {
        if (xrOrigin == null) xrOrigin = GetComponent<XROrigin>();
        SnapToStart();
    }

    public void SnapToStart()
    {
        if (xrOrigin == null || playerStart == null) return;

        // Current camera position in world space (HMD)
        Transform cam = xrOrigin.Camera.transform;

        // How far the XR Origin is from the camera right now
        Vector3 originToCam = cam.position - xrOrigin.transform.position;

        // Move XR Origin so the CAMERA lands exactly on playerStart
        xrOrigin.transform.position = playerStart.position - originToCam;

        // Optional: face the same direction as the start
        Vector3 flatForward = playerStart.forward;
        flatForward.y = 0f;
        if (flatForward.sqrMagnitude > 0.001f)
            xrOrigin.transform.rotation = Quaternion.LookRotation(flatForward);

        if (alsoRecenter)
        {
            var subsystems = new System.Collections.Generic.List<XRInputSubsystem>();
            SubsystemManager.GetSubsystems(subsystems);
            foreach (var s in subsystems) s.TryRecenter();
        }
    }
}
