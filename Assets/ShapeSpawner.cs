using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class ShapeSpawner : MonoBehaviour
{
    [SerializeField] private Transform prefab;

    private XRGrabInteractable interactable;

    // Start is called before the first frame update
    void Start()
    {
        Spawn();
    }

    void Spawn()
    {
        var instance = Instantiate(prefab, transform.position, Quaternion.identity);
        interactable = instance.gameObject.GetComponent<XRGrabInteractable>();
        interactable.selectEntered.AddListener(OnGrab);
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        interactable.selectEntered.RemoveListener(OnGrab);
        Spawn();
    }
}
