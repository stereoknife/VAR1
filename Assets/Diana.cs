using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana : MonoBehaviour
{
    [SerializeField] private ArcheryPoints points;
    [SerializeField] private int maxPoints;
    private float size;
    
    void Awake()
    {
        var bounds = GetComponent<MeshCollider>().bounds;
        size = Mathf.Max(bounds.extents.x, Mathf.Max(bounds.extents.y, bounds.extents.z));
        size /= 2;
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Fletxa"))
        {
            var point = other.GetContact(0).point;
            var dist = Vector3.Distance(point, transform.position);
            var ndist = dist / size;
            int score = ((int)(1 - ndist) * maxPoints) + 1;
            points.Increase(score);
        }
    }
}
