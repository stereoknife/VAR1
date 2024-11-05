using System.Collections.Generic;
using UnityEngine;

public class GoldfishScoop : MonoBehaviour
{
    public List<GameObject> scoopedGoldfish = new List<GameObject>();  // Holds the scooped goldfish
    public Transform scoopContainer;                                   // Empty object to hold scooped fish

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object we collided with is a goldfish
        if (other.gameObject.CompareTag("Goldfish"))
        {
            // Scoop the goldfish
            Debug.Log("Scooping goldfish");
            ScoopGoldfish(other.gameObject);
        }
    }

    private void ScoopGoldfish(GameObject goldfish)
    {
        // Add to scooped list
        if (!scoopedGoldfish.Contains(goldfish))
        {
            scoopedGoldfish.Add(goldfish);

            // Parent the goldfish to the scoop container
            goldfish.transform.SetParent(scoopContainer);
            goldfish.transform.localPosition = Vector3.zero; // Center inside the scoop
            
            // Optionally disable its movement
            GoldfishMovement movementScript = goldfish.GetComponent<GoldfishMovement>();
            if (movementScript != null)
            {
                movementScript.enabled = false;  // Stop goldfish from moving
            }
            // Remove the Rigidbody and Collider components
            Rigidbody rb = goldfish.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Destroy(rb);
            }

            Collider col = goldfish.GetComponent<Collider>();
            if (col != null)
            {
                Destroy(col);
            }
        }
    }
}
