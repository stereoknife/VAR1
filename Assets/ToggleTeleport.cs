using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class ToggleTeleport : MonoBehaviour
{
    [SerializeField]
    private ControllerInputActionManager leftControllerManager, rightControllerManager;

    public void Toggle()
    {
        Debug.Log("Toggling");
        leftControllerManager.smoothMotionEnabled = !leftControllerManager.smoothMotionEnabled;
        rightControllerManager.smoothMotionEnabled = !rightControllerManager.smoothMotionEnabled;
    }
}
