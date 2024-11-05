using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

[RequireComponent(typeof(SphereCollider))]
public class ShapeSpawner : MonoBehaviour
{
    [SerializeField] private Transform prefab;

    private XRGrabInteractable interactable;
    private Transform lastInstance;

    // Start is called before the first frame update
    void Start()
    {
        Spawn();
    }

    void Spawn()
    {
        lastInstance = Instantiate(prefab, transform.position, Quaternion.identity);
        interactable = lastInstance.gameObject.GetComponent<XRGrabInteractable>();
        interactable.selectEntered.AddListener(OnGrab);
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        interactable.selectEntered.RemoveListener(OnGrab);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform == lastInstance)
        {
            Spawn();
        }
    }
}
