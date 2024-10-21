using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The object is like a net that breaks when it moves too fast.
// The prefab is composed of three parts: the net, the broken net, and the net holder.
// The net holder is the parent object that contains the net and the broken net which swaps visibility when the net breaks.
// The code takes the speed of the net when it's moved by the user and breaks the net if the speed exceeds a certain threshold.
// The net has to only take into account speed under the water represented as external bounding boxes.

public class break_net : MonoBehaviour
{
    public float breakSpeedThreshold = 5.0f;
    public GameObject net;
    public GameObject brokenNet;
    public Collider waterBounds; // Collider representing the underwater bounds
    private Vector3 lastPosition;
    private bool isBroken = false;

    void Start()
    {
        lastPosition = transform.position;
        net.SetActive(true);
        brokenNet.SetActive(false);
    }

    void Update()
    {
        if (isBroken) return;

        Vector3 currentPosition = transform.position;

        // Check if the net is within the water bounds
        if (waterBounds.bounds.Contains(currentPosition))
        {
            float speed = (currentPosition - lastPosition).magnitude / Time.deltaTime;

            if (speed > breakSpeedThreshold)
            {
                Break();
            }
        }

        lastPosition = currentPosition;
    }

    void Break()
    {
        isBroken = true;
        net.SetActive(false);
        brokenNet.SetActive(true);
        // Add additional logic to handle the object breaking, e.g., play animation, disable object, etc.
        Debug.Log("Object has broken due to high speed!");
    }
}
