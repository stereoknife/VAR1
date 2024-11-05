using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketGoldfish : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> scoopedGoldfish = new List<GameObject>();  // Holds the scooped goldfish

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PopulateScoopedGoldfish(List<GameObject> goldfish)
    {
        scoopedGoldfish.AddRange(goldfish);
        Debug.Log("Got"+goldfish.Count+" goldfish from the net");
        Debug.Log("Populated " + scoopedGoldfish.Count + " goldfish in the bucket");
    }
}
