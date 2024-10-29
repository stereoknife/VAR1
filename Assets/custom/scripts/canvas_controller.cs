using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CanvasController : MonoBehaviour
{
    public GameObject canvas; // Assign the Canvas in the Inspector
    public XRController rightHandController; // Assign the right hand controller in the Inspector
    private bool isVisible = false;

    void Start()
    {
        // Initially hide the canvas
        canvas.SetActive(isVisible);
    }

    void Update()
    {
        // Attach the canvas to the right hand
        if (rightHandController)
        {
            canvas.transform.position = rightHandController.transform.position;
            canvas.transform.rotation = rightHandController.transform.rotation;
        }
    }

    public void ToggleCanvasVisibility()
    {
        isVisible = !isVisible;
        canvas.SetActive(isVisible);
    }
}