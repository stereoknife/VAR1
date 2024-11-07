using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paint : MonoBehaviour
{
    [SerializeField] private Color color;
    [SerializeField] private float tintTime = 3f;

    private float tintMult;
    
    void Awake()
    {
        var renderer = GetComponent<MeshRenderer>();
        var displayColor = color;
        displayColor.a = 0.5f;
        renderer.material.color = displayColor;
        color = InvertColor(color);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("BuildBlock"))
        {
            var renderer = other.GetComponent<MeshRenderer>();
            var tcolor = renderer.material.color;
            tcolor -= color * Time.deltaTime / tintTime;
            tcolor.r = Mathf.Max(0, tcolor.r);
            tcolor.g = Mathf.Max(0, tcolor.g);
            tcolor.b = Mathf.Max(0, tcolor.b);
            renderer.material.color = tcolor;
        }
    }

    private Color InvertColor(Color color)
    {
        var o = color;
        o.r = 1f - o.r;
        o.g = 1f - o.g;
        o.b = 1f - o.b;
        return o;
    }
}
