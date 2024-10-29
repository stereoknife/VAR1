using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class break_net : MonoBehaviour
{
    //0.2 or 0.3
    [SerializeField] float breakThreshold = 0.04f; // The speed threshold at which the object breaks
    [SerializeField] GameObject intactObject;   // The intact version of the object
    [SerializeField] GameObject brokenObject;   // The broken version of the object

    private Rigidbody rb;             // Rigidbody of the object
    private XRGrabInteractable grab;
    
    private bool isBroken = false;   // Tracks if the object is already broken
    private bool isWet = false;
    private bool isGrabbed = false;
    private bool isTouching = false;

    private Vector3 prevPosition;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        grab = GetComponent<XRGrabInteractable>();
        intactObject.SetActive(true); // Start with the intact object visible
        brokenObject.SetActive(false); // Start with the broken object hidden
        prevPosition = transform.position;
    }
    
    void Update()
    {
        if (!isGrabbed) return;
        if (!isWet) return;
        if (!isTouching) return;
        
        var speed = rb.velocity.sqrMagnitude;
        
        if (speed > breakThreshold && !isBroken)
        {
            isBroken = true;

            // Swap the intact object for the broken one
            intactObject.SetActive(false);
            brokenObject.SetActive(true);
        }  
    }

    private void OnEnable()
    {
        grab.selectEntered.AddListener(OnGrabbed);
    }

    private void OnDisable()
    {
        grab.selectEntered.RemoveListener(OnGrabbed);
    }

    private void OnGrabbed(SelectEnterEventArgs args)
    {
        isGrabbed = true;
    }
}
