using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CarMount : MonoBehaviour
{
    public Transform mountPoint;
    public Canvas canvas1;
    public GameObject xrRig;
    private bool isMounted = false;

    public void OnEnterPressed()
    {
        if (!isMounted)
        {
            MountCar();
        }
    }

    public void OnExitPressed()
    {
        if (isMounted)
        {
            UnmountCar();
        }
    }

    private void MountCar()
    {
        // Move the XR Rig to the mount point position
        xrRig.transform.position = mountPoint.position;
        xrRig.transform.rotation = mountPoint.rotation;

        // Parent the XR Rig to the car to follow its movement
        xrRig.transform.parent = transform;

        // Align the XR Rig's Y rotation with the car's Y rotation
        Vector3 eulerAngles = xrRig.transform.eulerAngles;
        eulerAngles.y = transform.eulerAngles.y;
        xrRig.transform.eulerAngles = eulerAngles;
        
        // Deactivate the character controller
        var characterController = xrRig.GetComponent<CharacterController>();
        if (characterController != null)
        {
            characterController.enabled = false;
        }

        // Hide the canvas
        canvas1.enabled = false;
        isMounted = true;
    }

    private void UnmountCar()
    {
        // Unparent the XR Rig
        xrRig.transform.parent = null;

        // Optionally, move the XR Rig back to a default position
        // xrRig.transform.position = originalPosition;

        isMounted = false;
    }
}
