using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;



public class SFXManager : MonoBehaviour
{
    [SerializeField] List<string> sfx_name_list;
    Dictionary<string, AudioSource> sfx_dictionary = new Dictionary<string, AudioSource>();
    Dictionary<string, float> sfx_volume_dictionary = new Dictionary<string, float>();

    public UnityEvent<float> volumeChanged = new UnityEvent<float>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < sfx_name_list.Count; i++)
        {
            sfx_dictionary[sfx_name_list[i]] = transform.GetChild(i).GetComponentInChildren<AudioSource>();
        }
        for (int i = 0; i < sfx_name_list.Count; i++)
        {
            sfx_volume_dictionary[sfx_name_list[i]] = sfx_dictionary[sfx_name_list[i]].volume;
        }
        if (PlayerPrefs.HasKey("sfx_volume")) {
            SetVolume(PlayerPrefs.GetFloat("sfx_volume"));
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Update SFX volume based on player preferences
        
    }

    public void SetVolume(float value)
    {
        for (int i = 0; i < sfx_name_list.Count; i++)
        {
            sfx_dictionary[sfx_name_list[i]].volume = value * sfx_volume_dictionary[sfx_name_list[i]];
        }
        volumeChanged.Invoke(value);
    }

    public void Play(string sfx_name)
    {
        if (sfx_dictionary.ContainsKey(sfx_name))
        {
            sfx_dictionary[sfx_name].Play();
        }
    }

    public void Pause(string sfx_name)
    {
        if (sfx_dictionary.ContainsKey(sfx_name))
        {
            sfx_dictionary[sfx_name].Pause();
        }
    }

    public void Stop(string sfx_name)
    {
        if (sfx_dictionary.ContainsKey(sfx_name))
        {
            sfx_dictionary[sfx_name].Stop();
        }
    }

    public void PausePlaying()
    {
        for (int i = 0; i < sfx_name_list.Count; i++)
        {
            if (sfx_dictionary[sfx_name_list[i]].isPlaying == true)
            {
                sfx_dictionary[sfx_name_list[i]].Pause();
            }
        }
    }

    public void PlayPaused()
    {
        for (int i = 0; i < sfx_name_list.Count; i++)
        {
            sfx_dictionary[sfx_name_list[i]].UnPause();
        }
    }
}
