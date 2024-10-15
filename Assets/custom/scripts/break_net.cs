using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class break_net : MonoBehaviour
{
    public float breakThreshold = 5f; // The speed threshold at which the object breaks
    public GameObject intactObject;   // The intact version of the object
    public GameObject brokenObject;   // The broken version of the object

    private Rigidbody rb;             // Rigidbody of the object
    private bool isBroken = false;    // Tracks if the object is already broken
    private OVRGrabbable grabbable;   // Reference to the OVRGrabbable component

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        grabbable = GetComponent<OVRGrabbable>();
        intactObject.SetActive(true); // Start with the intact object visible
        brokenObject.SetActive(false); // Start with the broken object hidden
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the object is being grabbed
        if (grabbable.isGrabbed)
        {
            // Get the hand that is grabbing the object
            OVRGrabber grabber = grabbable.grabbedBy;

            // Check the speed of the hand
            float handSpeed = grabber.GetComponent<Rigidbody>().velocity.magnitude;

            // If speed exceeds the threshold and the object is not yet broken
            if (handSpeed > breakThreshold && !isBroken)
            {
                BreakObject();
            }
        }
    }

    // Function to handle breaking the object
    void BreakObject()
    {
        isBroken = true;

        // Swap the intact object for the broken one
        intactObject.SetActive(false);
        brokenObject.SetActive(true);

        // Optionally, disable grabbing so the user can't pick it up anymore
        // if (grabbable != null)
        // {
        //     grabbable.enabled = false;
        // }

        // If you want physics to affect the broken pieces:
        foreach (Rigidbody piece in brokenObject.GetComponentsInChildren<Rigidbody>())
        {
            piece.isKinematic = false; // Allow broken pieces to fall and scatter
        }
    }
}
