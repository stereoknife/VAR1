using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MasterVolumeController : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer; // Reference to the Audio Mixer
    [SerializeField] private Slider sfxVolumeSlider; // Slider to control SFX volume

    private void Start()
    {
        // Set initial volume based on the slider's current value
        UpdateSFXVolume(sfxVolumeSlider.value);

        // Add a listener to the slider to adjust the volume when changed
        sfxVolumeSlider.onValueChanged.AddListener(UpdateSFXVolume);
    }

    private void UpdateSFXVolume(float sliderValue)
    {
        // Convert the slider value (0-1) to a logarithmic scale (-80 dB to 0 dB)
        float volumeInDecibels = Mathf.Log10(Mathf.Clamp(sliderValue, 0.0001f, 1)) * 20;
        audioMixer.SetFloat("Master", volumeInDecibels);
    }

    private void OnDestroy()
    {
        sfxVolumeSlider.onValueChanged.RemoveListener(UpdateSFXVolume);
    }
}
