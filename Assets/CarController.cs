using Unity.VRTemplate;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class CarController : MonoBehaviour
{
    // Input Actions specific to car control
    public InputActionProperty accelerateAction;
    public InputActionProperty brakeAction;
    public InputActionProperty steerAction;

    // Reference to the InputActionAsset containing all action maps
    public InputActionAsset inputActions;

    // Car-specific movement settings
    public float accelerationForce = 10f;
    public float brakingForce = 10f;
    public float maxSpeed = 20f;
    private Rigidbody rb;
    private bool isMounted = false; // Tracks if the user is mounted in the car
    private float speed = 0f;
    
    [SerializeField] private XRKnob steering;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true; // Disable movement until the user mounts the car
    }

    void Update()
    {
        if (!isMounted) return; // Ignore controls if not mounted

        // Handle car controls only if mounted
        float accelerationInput = accelerateAction.action.ReadValue<float>();
        float brakeInput = brakeAction.action.ReadValue<float>();
        // float steerInput = steerAction.action.ReadValue<Vector2>().x; // Example steer input from joystick X-axis

        if (accelerationInput > 0)
        {
            Accelerate(accelerationInput);
            // Some rolling resistance
            brakeInput = Mathf.Max(brakeInput, 0.1f);
        }

        if (brakeInput > 0)
        {
            Brake(brakeInput);
        }

        Steer(3 * (steering.value - 0.5f));
        
        transform.position += Time.deltaTime * speed * transform.forward;
    }

    private void Accelerate(float input)
    {
        if (speed < maxSpeed)
        {
            speed += accelerationForce * input * Time.deltaTime;
        }
    }

    private void Brake(float input)
    {
        if (speed > 0)
        {
            speed -= brakingForce * input * Time.deltaTime;
        }
        if (speed < 0) speed = 0f;
    }

    private void Steer(float input)
    {
        // Rotate the car based on steer input
        transform.Rotate(Vector3.up * input * Time.deltaTime * 50f); // Adjust rotation speed as needed
    }

    public void MountCar()
    {
        isMounted = true;
        //rb.isKinematic = false; // Enable physics

        // Enable CarControls and disable other action maps
        inputActions.FindActionMap("CarControls").Enable();
        inputActions.FindActionMap("XRI Left Locomotion").Disable();
        // inputActions.FindActionMap("XRI Left Interaction").Disable();
        // inputActions.FindActionMap("XRI Left").Disable();
        inputActions.FindActionMap("XRI Right Locomotion").Disable();
        // inputActions.FindActionMap("XRI Right Interaction").Disable();
        // inputActions.FindActionMap("XRI Right").Disable();

        accelerateAction.action.Enable();
        brakeAction.action.Enable();
        // steerAction.action.Enable();
    }

    public void UnmountCar()
    {
        isMounted = false;
        rb.isKinematic = true; // Disable physics when unmounted
        //rb.velocity = Vector3.zero;

        // Disable CarControls and re-enable other action maps
        inputActions.FindActionMap("CarControls").Disable();
        inputActions.FindActionMap("XRI Left Locomotion").Enable();
        inputActions.FindActionMap("XRI Right Locomotion").Enable();

        accelerateAction.action.Disable();
        brakeAction.action.Disable();
        // steerAction.action.Disable();
    }
}
