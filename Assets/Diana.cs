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
        var mr = GetComponent<MeshRenderer>();
        size = Mathf.Max(mr.bounds.extents.x, Mathf.Max(mr.bounds.extents.y, mr.bounds.extents.z));
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
