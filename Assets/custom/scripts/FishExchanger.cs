using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class FishExchanger : MonoBehaviour
{
    private BucketGoldfish CurrentBucket = null;
    public TMP_Text inputField;
    // Start is called before the first frame update
    void Start()
    {
        inputField.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BucketGoldfish"))
        {
            CurrentBucket = other.GetComponent<BucketGoldfish>();
            if (CurrentBucket != null)
            {
                Debug.Log("Number of goldfish in the bucket: " + CurrentBucket.scoopedGoldfish.Count);
                inputField.text = CurrentBucket.scoopedGoldfish.Count.ToString();

            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("BucketGoldfish"))
        {
            CurrentBucket = null;
            inputField.text = "0";

        }
    }

    public void ExchangeFish()
    {
        if (CurrentBucket != null)
        {
            Debug.Log("Exchanged " + CurrentBucket.scoopedGoldfish.Count + " goldfish with the bucket");
            CurrentBucket.CleanList();
            inputField.text = "0";
        }
    }
}
