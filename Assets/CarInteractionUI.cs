using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CarInteractionUI : MonoBehaviour
{
    public Canvas carUICanvas;

    private void Awake()
    {
        carUICanvas.enabled = false; // Hide UI initially
    }

    public void OnHoverEnter(HoverEnterEventArgs args)
    {
        carUICanvas.enabled = true;  // Show UI when user hovers
    }

    public void OnHoverExit(HoverExitEventArgs args)
    {
        carUICanvas.enabled = false; // Hide UI when user leaves
    }
}
