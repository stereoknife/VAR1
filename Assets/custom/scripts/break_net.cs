using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class break_net : MonoBehaviour
{
    [SerializeField] float breakThreshold = 0.04f;        // Speed threshold for breaking
    [SerializeField] float maxSafeVelocity = 0.02f;       // Safe movement velocity
    [SerializeField] float dangerIncreaseRate = 1.0f;     // Rate at which danger level increases when moving too fast
    [SerializeField] float dangerDecreaseRate = 0.002f;     // Rate at which danger level decreases when moving safely
    [SerializeField] float maxDangerLevel = 100.0f;           // Danger level required to break the net
    [SerializeField] GameObject intactObject;             // The intact version of the object
    [SerializeField] GameObject brokenObject;             // The broken version of the object
    [SerializeField] Renderer objectRenderer;             // Renderer for color change based on danger

    private Rigidbody rb;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grab;
    private bool isBroken = false;
    private bool isGrabbed = false;
    private Vector3 prevPosition;
    private float dangerLevel = 0f;                       // Current danger level

    private Renderer intactObjectRenderer;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        grab = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        intactObject.SetActive(true);                      // Start with the intact object visible
        brokenObject.SetActive(false);                     // Start with the broken object hidden
        prevPosition = transform.position;

        // Cache the Renderer component of the intact object
        intactObjectRenderer = intactObject.GetComponent<Renderer>();
    }

    void Update()
    {
        if (isGrabbed && !isBroken)
        {
            // Calculate the speed
            Vector3 currentPosition = transform.position;
            float speed = (currentPosition - prevPosition).magnitude / Time.deltaTime*1000;
            prevPosition = currentPosition;

            // Adjust danger level based on speed
            UpdateDangerLevel(speed);

            // Update color based on the current danger level
            UpdateColor();
            
            // Check if the net should break
            if (dangerLevel >= maxDangerLevel)
            {
                BreakNet();
            }
        }
    }

    private void UpdateDangerLevel(float speed)
    {
        if (speed > maxSafeVelocity)
        {
            // Increase the danger level when moving too fast
            dangerLevel += dangerIncreaseRate;
        }
        else
        {
            // Decrease the danger level when moving slowly enough
            dangerLevel -= dangerDecreaseRate;
        }
        // Debug.Log("Danger level: " + dangerLevel);
        // Clamp danger level between 0 and maxDangerLevel
        dangerLevel = Mathf.Clamp(dangerLevel, 0f, maxDangerLevel);
    }

    private void UpdateColor()
    {
        if (intactObjectRenderer != null)
        {
            // Map the danger level to a color (green when safe, red when dangerous)
            Color color = Color.Lerp(Color.green, Color.red, dangerLevel / maxDangerLevel);
            intactObjectRenderer.material.color = color;
        }
    }

    private void BreakNet()
    {
        if (!isBroken)
        {
            // Debug.Log("The net has broken!");
            isBroken = true;
            
            // Disable the intact object and enable the broken object
            intactObject.SetActive(false);
            brokenObject.SetActive(true);

            // get the intactObject elemen that has the GoldfishScoop script and execute the CleanList method
            intactObject.GetComponent<GoldfishScoop>().CleanList();
        }
    }

    private void OnEnable()
    {
        grab.selectEntered.AddListener(GrabbedStart);
        grab.selectExited.AddListener(GrabbedEnd);
    }

    private void OnDisable()
    {
        grab.selectEntered.RemoveListener(GrabbedStart);
        grab.selectExited.RemoveListener(GrabbedEnd);
    }

    public void GrabbedStart(SelectEnterEventArgs args)
    {
        isGrabbed = true;
        prevPosition = transform.position;
    }

    public void GrabbedEnd(SelectExitEventArgs args)
    {
        isGrabbed = false;
        dangerLevel = 0f; // Reset danger level when released
        UpdateColor();    // Reset color to green when not held
    }
}
