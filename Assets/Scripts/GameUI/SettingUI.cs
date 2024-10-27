using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour
{
    [SerializeField] private AudioMixer soundsMixer;
    [SerializeField] private AudioMixer musicMixer;
    [SerializeField] private Slider soundsSlider;
    [SerializeField] private Slider musicSlider;
    private bool SoundsOn = true;
    [SerializeField] private Button soundsButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Sprite SoundOn;
    [SerializeField] private Sprite SoundOff;


    

    void Start()
    {
        // Đặt giá trị của Slider theo giá trị hiện tại của AudioMixer
        SetSliderValue(soundsMixer, soundsSlider);
        SetSliderValue(musicMixer, musicSlider);
    }

    private void SetSliderValue(AudioMixer mixer, Slider slider)
    {
        float currentVolume;
        mixer.GetFloat("Volume", out currentVolume);
        slider.value = Mathf.InverseLerp(-80f, 0f, currentVolume); // Chuyển đổi dB thành giá trị từ 0 đến 1
    }

    public void OnSoundsSliderValueChanged(float value)
    {
        value = soundsSlider.value;
        // Chuyển đổi giá trị từ 0-1 thành dB và thiết lập âm lượng cho Sounds
        float volume = Mathf.Lerp(-80f, 0f, value);
        soundsMixer.SetFloat("Volume", volume);
    }

    public void OnMusicSliderValueChanged(float value)
    {
        value = musicSlider.value;
        // Chuyển đổi giá trị từ 0-1 thành dB và thiết lập âm lượng cho Music
        float volume = Mathf.Lerp(-80f, 0f, value);
        musicMixer.SetFloat("Volume", volume);
    }

    public void CloseSetting()
    {
        gameObject.SetActive(false);
    }

    private void volumeButton(AudioMixer mixer, Slider slider)
    {
        if (SoundsOn)
        {
            mixer.SetFloat("Volume", -80f);
            slider.value = 0;
            SoundsOn = false;
        }
        else
        {
            mixer.SetFloat("Volume", 0f);
            slider.value = 1;
            SoundsOn = true;
        }
    }
    
    public void SoundButton()
    {
        if (SoundsOn)
        {
            soundsButton.GetComponent<Image>().sprite = SoundOff;
        }
        else
        {
            soundsButton.GetComponent<Image>().sprite = SoundOn;
        }
        volumeButton(soundsMixer, soundsSlider);
    }
    public void MusicButton()
    {
        if (SoundsOn)
        {
            musicButton.GetComponent<Image>().sprite = SoundOff;
        }
        else
        {
            musicButton.GetComponent<Image>().sprite = SoundOn;
        }
        volumeButton(musicMixer, musicSlider);
    }

}
