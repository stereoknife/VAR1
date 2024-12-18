using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CarInteractionUI : MonoBehaviour
{
    public Canvas carUICanvas;
    public Canvas carUIColorCanvas;

    private void Awake()
    {
        carUICanvas.enabled = false; // Hide UI initially
    }

    public void OnHoverEnter(HoverEnterEventArgs args)
    {
        if (carUICanvas.enabled || carUIColorCanvas.enabled)
        {
            return;
        }
        carUICanvas.enabled = true;  // Show UI when user hovers
    }

    public void OnHoverExit(HoverExitEventArgs args)
    {
        carUICanvas.enabled = false; // Hide UI when user leaves
    }
}
