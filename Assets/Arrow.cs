using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float attachSpeed = 5f;
    [SerializeField] private float bowStrength = 5f;
    
    private XRGrabInteractable _interactable;

    private bool isGrabbed, isOnString;

    private Transform _bow;
    private SkinnedMeshRenderer _string;
    
    private float drawStrength;
    private const float stringDrawMultiplier = 0.5f / 0.00102128f;

    private Rigidbody rb;
    
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        _interactable = GetComponent<XRGrabInteractable>();
        _interactable.trackRotation = true;
    }
    
    private void OnEnable()
    {
        _interactable.selectEntered.AddListener(OnGrab);
        _interactable.selectExited.AddListener(OnDrop);
    }

    private void OnDisable()
    {
        _interactable.selectEntered.RemoveListener(OnGrab);
        _interactable.selectExited.RemoveListener(OnDrop);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isGrabbed || isOnString) return;
        
        if (other.CompareTag("Arc"))
        {
            _bow = other.transform;
            _interactable.trackRotation = false;
            _string = _bow.parent.GetComponentInChildren<SkinnedMeshRenderer>();
            isOnString = true;
        }
    }
    
    void Update()
    {
        if (isOnString)
        {
            transform.rotation = _bow.rotation;
            _interactable.throwOnDetach = false;
            drawStrength = stringDrawMultiplier * Vector3.Distance(_bow.position, transform.position);
            _string.SetBlendShapeWeight(0, drawStrength);
        }
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        isGrabbed = true;
        transform.parent = null;
    }

    void OnDrop(SelectExitEventArgs args)
    {
        rb.isKinematic = false;
        
        if (isOnString)
        {
            rb.AddForce(transform.forward * bowStrength * drawStrength);
            _string.SetBlendShapeWeight(0, 0);
        }
        
        isGrabbed = false;
        isOnString = false;
        _bow = null;
        _string = null;
        _interactable.trackRotation = true;
        _interactable.throwOnDetach = true;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Arc") || other.gameObject.CompareTag("Fletxa")) return;
        if (rb.velocity.sqrMagnitude > attachSpeed * attachSpeed)
        {
            rb.isKinematic = true;
            transform.parent = other.transform;
        }
    }
}
