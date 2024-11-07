using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ArcheryPoints : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    private int points = 0;

    public void Start()
    {
        text.text = "0";
    }

    public void Increase(int p = 1)
    {
        points += points;
        text.text =  points.ToString();
    }
}
