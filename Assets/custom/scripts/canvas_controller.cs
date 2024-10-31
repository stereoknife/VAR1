using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem; // Add this line for the new Input System

public class CanvasController : MonoBehaviour
{
    public InputActionProperty triggerPressAction;  // Drag and drop your Action here in the Inspector
    public float distanceFromUser = 2.0f; // Distance to place the canvas in front of the user
    private bool isVisible = false;
    public GameObject canvas; // Assign the Canvas in the Inspector

    void Start()
    {
        // Enable the input action
        triggerPressAction.action.Enable();
    }

    void Update()
    {
        // Check if the action is triggered
        float OpenMenu = triggerPressAction.action.ReadValue<float>();
        Debug.Log(OpenMenu);
        if ( OpenMenu >=0.1f)
        {
            Debug.Log("Trigger button pressed, opening canvas.");
            OpenCanvas();
        }
    }

    void MoveCanvasInFrontOfUser()
    {
        Transform userTransform = Camera.main.transform;
        Vector3 newCanvasPosition = userTransform.position + userTransform.forward * distanceFromUser;
        transform.position = newCanvasPosition;
        transform.rotation = Quaternion.LookRotation(userTransform.forward);
    }

    public void ToggleCanvasVisibility()
    {
        isVisible = !isVisible;
        canvas.SetActive(isVisible);
    }

    public void OpenCanvas()
    {
        isVisible = true;
        canvas.SetActive(true);
        MoveCanvasInFrontOfUser();
    }
}