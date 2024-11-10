using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMenuController : MonoBehaviour
{
    // Start is called before the first frame update
    public Canvas mainCanvas;
    public Canvas colorCanvas;
    void Start()
    {
    }
    void Awake()
    {
        mainCanvas.enabled = true;
        colorCanvas.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnCustomizeButtonClick()
    {
        mainCanvas.enabled = false;
        colorCanvas.enabled = true;
    }
    public void OnBackButtonClick()
    {
        mainCanvas.enabled = true;
        colorCanvas.enabled = false;
    }

    public void OnExitButtonClick()
    {
        mainCanvas.enabled = false;
        colorCanvas.enabled = false;
    }
}
