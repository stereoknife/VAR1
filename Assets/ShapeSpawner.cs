using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class ShapeSpawner : MonoBehaviour
{
    [SerializeField] private Transform prefab;
    [SerializeField] private float distance;

    private float distsq;
    private Transform lastInstance;
    private Transform t;

    // Start is called before the first frame update
    void Start()
    {
        distsq = distance * distance;
        t = transform;
        Spawn();
    }

    void Spawn()
    {
        lastInstance = Instantiate(prefab, transform.position, Quaternion.identity);
    }

    private void Update()
    {
        if ((t.position - lastInstance.position).sqrMagnitude > distsq)
        {
            Spawn();
        }
    }
}
