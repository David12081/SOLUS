using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    public Slider volumeSlider;

    private void Start()
    {
        volumeSlider = GetComponent<Slider>();
        volumeSlider.value = AudioManager.instance.music.volume;
    }

    private void Update()
    {
        volumeSlider.value = AudioManager.instance.music.volume;
    }

    public void SetVolume()
    {
        AudioManager.instance.music.volume = volumeSlider.value;
    }
}