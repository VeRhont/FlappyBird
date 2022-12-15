using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private Slider _sliderVolumeMusic;
    [SerializeField] private Toggle _toggleMusic;

    private AudioSource[] _audioSources;
    private float _volume = 0.3f;

    private void Start()
    {
        _audioSources = GameObject.FindObjectsOfType<AudioSource>();

        Load();
        ValueMusic();
    }

    public void SliderMusic()
    {
        _volume = _sliderVolumeMusic.value;

        Save();
        ValueMusic();
    }

    public void ToggleMusic()
    {
        if (_toggleMusic.isOn)
        {
            _volume = 1;
        }
        else
        {
            _volume = 0;
        }

        Save();
        ValueMusic();
    }

    private void ValueMusic()
    {
        foreach (var source in _audioSources)
        {
            source.volume = _volume;         
        }

        if (_sliderVolumeMusic != null) _sliderVolumeMusic.value = _volume;
        if (_toggleMusic != null) _toggleMusic.isOn = _volume != 0f;
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("volume", _volume);
    }

    private void Load()
    {
        _volume = PlayerPrefs.GetFloat("volume");
    }
}