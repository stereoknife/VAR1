using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class ToggleRotation : MonoBehaviour
{
    [SerializeField]
    private ControllerInputActionManager leftControllerManager, rightControllerManager;

    public void Toggle()
    {
        Debug.Log("Toggling");
        leftControllerManager.smoothTurnEnabled = !leftControllerManager.smoothTurnEnabled;
        rightControllerManager.smoothTurnEnabled = !rightControllerManager.smoothTurnEnabled;
    }
}
