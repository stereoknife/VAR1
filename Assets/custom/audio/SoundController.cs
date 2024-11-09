using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    [SerializeField] private List<List<AudioSource>> soundGroups = new List<List<AudioSource>>(); // List of sound groups
    [SerializeField] private Slider volumeSlider; // The UI slider to control volume

    private List<float> initialVolumes = new List<float>(); // List to store initial volumes

    void Start()
    {
        // Store the initial volume for each AudioSource in all sound groups
        foreach (var soundList in soundGroups)
        {
            foreach (var audioSource in soundList)
            {
                initialVolumes.Add(audioSource.volume);
            }
        }

        // Add a listener to the slider to call UpdateVolume when its value changes
        volumeSlider.onValueChanged.AddListener(UpdateVolume);

        // Initialize volume
        UpdateVolume(volumeSlider.value);
    }

    private void UpdateVolume(float sliderValue)
    {
        int index = 0;
        
        // Update each AudioSource volume based on the slider value and its initial volume
        foreach (var soundList in soundGroups)
        {
            foreach (var audioSource in soundList)
            {
                audioSource.volume = initialVolumes[index] * sliderValue;
                index++;
            }
        }
    }

    private void OnDestroy()
    {
        // Clean up the listener to prevent memory leaks
        volumeSlider.onValueChanged.RemoveListener(UpdateVolume);
    }
}
