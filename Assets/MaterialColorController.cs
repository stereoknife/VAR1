using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaterialColorController : MonoBehaviour
{
    [SerializeField] private Slider redSlider;
    [SerializeField] private Slider greenSlider;
    [SerializeField] private Slider blueSlider;
    [SerializeField] private List<Renderer> targetRenderers = new List<Renderer>(); // List of renderers to change

    private List<Material> targetMaterials = new List<Material>(); // List of materials to update

    void Start()
    {
        // Get a reference to the materials of each renderer in the list
        foreach (Renderer renderer in targetRenderers)
        {
            if (renderer != null)
            {
                // Create a copy of the material for each renderer to prevent modifying the original material asset
                if (renderer.materials.Length > 1)
                {
                    targetMaterials.Add(renderer.materials[1]);
                }
            }
        }

        // Add listeners to each slider to call UpdateColor when their values change
        redSlider.onValueChanged.AddListener(UpdateColor);
        greenSlider.onValueChanged.AddListener(UpdateColor);
        blueSlider.onValueChanged.AddListener(UpdateColor);

        // Initialize color
        UpdateColor(0);
    }

    // Update the material color based on slider values
    private void UpdateColor(float value)
    {
        float r = redSlider.value;
        float g = greenSlider.value;
        float b = blueSlider.value;
        
        Color newColor = new Color(r, g, b, 1); // 1 for full opacity

        // Apply the color to each material in the list
        foreach (Material material in targetMaterials)
        {
            material.color = newColor;
        }
    }

    private void OnDestroy()
    {
        // Clean up listeners to prevent memory leaks
        redSlider.onValueChanged.RemoveListener(UpdateColor);
        greenSlider.onValueChanged.RemoveListener(UpdateColor);
        blueSlider.onValueChanged.RemoveListener(UpdateColor);
    }
}
