using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paint : MonoBehaviour
{
    [SerializeField] private Color color;
    
    void Awake()
    {
        var renderer = GetComponent<MeshRenderer>();
        var displayColor = color;
        displayColor.a = 0.5f;
        renderer.material.color = displayColor;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BuildBlock"))
        {
            var renderer = other.GetComponent<MeshRenderer>();
            renderer.material.color *= color;
        }
    }
}
