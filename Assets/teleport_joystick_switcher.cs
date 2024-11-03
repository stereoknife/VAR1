using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class teleport_joystick_switcher : MonoBehaviour
{
    public XRController controller; // Assign your XR controller in the inspector
    public float distanceFromUser = 2.0f; // Distance to place the canvas in front of the user

    // Update is called once per frame
    void Update()
    {
        MoveCanvasInFrontOfUser();

        // if (controller.inputDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool isPressed) && isPressed)
        // {
        //     MoveCanvasInFrontOfUser();
        // }
    }

    void MoveCanvasInFrontOfUser()
    {
        Transform userTransform = Camera.main.transform;
        Vector3 newCanvasPosition = userTransform.position + userTransform.forward * distanceFromUser;
        transform.position = newCanvasPosition;
        transform.rotation = Quaternion.LookRotation(userTransform.forward);
    }
}
